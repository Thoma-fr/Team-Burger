using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CombatManagerInGame : MonoBehaviour
{
	public BATTLE_STATE state = BATTLE_STATE.NONE;
	private BATTLE_STATE lastState = BATTLE_STATE.NONE;

	public EnemyController enemyController { get; private set; }
	private EnemyData enemyData;
	private PlayerData playerData;

	[SerializeField] private Transform dialogueBloc;
	private TextMeshProUGUI dialogueText;

	[SerializeField] private TextMeshProUGUI count;
	private int nbAnimalCount = 0;

	[SerializeField] private Transform commandBloc;
	[SerializeField] private Transform playerStat;

	[Header("Audio")]
	[SerializeField] private List<AudioClip> attaquePlayerClip;
	[SerializeField] private List<AudioClip> attaqueEnnemyClip;
	private AudioSource sourceAudio;

	public delegate void Callback();

	/*[Header("Dialogue")]
	[SerializeField, TextArea(2,5), Tooltip("")] private string introDialogue;*/


	private bool useConsomable = false, waitPlayerInput = false;
	private float dialogueBoxStartY, commandeBoxStartX;

	public enum BATTLE_STATE
	{
		NONE,
		INIT,
		INTRO,
		START,
		CHOICE,
		PLAYER_ACTION,
		ENEMY_ACTION,
		END,
	}


	void Start()
	{
		playerData = GameManager.instance.GetPlayerData;
		dialogueText = dialogueBloc.GetChild(0).GetComponent<TextMeshProUGUI>();

		dialogueBoxStartY = dialogueBloc.localPosition.y;
		commandeBoxStartX = commandBloc.localPosition.x;

		// Désactivation
		commandBloc.gameObject.SetActive(false);
		dialogueBloc.gameObject.SetActive(false);
		sourceAudio = gameObject.GetComponent<AudioSource>();

		count.text = "0";
	}

	void Update()
	{
		if (waitPlayerInput && Input.anyKeyDown)
        {
			state += 1;
			waitPlayerInput = false;
        }

		if (state == lastState)
			return;

		lastState = state;

		switch (state)
		{
			case BATTLE_STATE.INIT:
				Cursor.visible = true;
				if(enemyController)
                {
                    PlayerController.playerInstance.playerMode = PlayerController.PLAYER_MODE.COMBAT_MODE;
                    dialogueText.text = "Que ............ !? ............ !? ............ ???";

					enemyData = enemyController.m_data;
					enemyController.InitCombat();

					dialogueBloc.localPosition = new Vector2(dialogueBloc.localPosition.x, -700);
					commandBloc.localPosition = new Vector2(1160, commandBloc.localPosition.y);
				}
				else
					state = BATTLE_STATE.NONE;

				state = BATTLE_STATE.INTRO;
				break;

			case BATTLE_STATE.INTRO:
				// ANIMATIONS
				dialogueBloc.gameObject.SetActive(true);
				Sequence intro = DOTween.Sequence();
				intro.Append(dialogueBloc.DOLocalMoveY(dialogueBoxStartY, 0.7f).SetEase(Ease.OutElastic));
				intro.AppendCallback(() => state = BATTLE_STATE.START);

				break;

			case BATTLE_STATE.START:
				StartCoroutine(TypeSentence("An enemy appears! He is not happy.", () => waitPlayerInput = true));

				break;

			case BATTLE_STATE.CHOICE:
				commandBloc.gameObject.SetActive(true);
				commandBloc.localPosition = new Vector2(1160, commandBloc.localPosition.y);
				StartCoroutine(TypeSentence("What do you want to do?", () => commandBloc.DOLocalMoveX(commandeBoxStartX, 0.2f).SetEase(Ease.Linear)));

				break;

			case BATTLE_STATE.PLAYER_ACTION:
				if (useConsomable)
                {
					StartCoroutine(TypeSentence("You use an extraordinary item! Wow, well done?", () => state = BATTLE_STATE.ENEMY_ACTION));
					GameManager.instance.UpdateAllUI();
					useConsomable = false;
					break;
                }

				StartCoroutine(CheckLife("You use your weapon!!!", PlayAudio(attaquePlayerClip[Random.Range(0, attaquePlayerClip.Count)], () => enemyController.TakeDamage(playerData.weaponInHand.damage)), BATTLE_STATE.ENEMY_ACTION));
				GameManager.instance.UpdateAllUI();
				break;

			case BATTLE_STATE.ENEMY_ACTION:
				playerData.healthPoint -= enemyController.EnemyAttacking(out string attName);

				StartCoroutine(CheckLife("Your enemy attacks you with  " + attName + " !!!", PlayAudio(attaqueEnnemyClip[Random.Range(0, attaqueEnnemyClip.Count)], () => GameManager.instance.UpdateAllUI()), BATTLE_STATE.CHOICE));
				break;

			case BATTLE_STATE.END:
				PlayerController.playerInstance.Unvise();
				enemyController.gameObject.GetComponent<NAVAI>().die();
				PlayerController.playerInstance.playerMode = PlayerController.PLAYER_MODE.ADVENTURE_MODE;
				Debug.Log("fight end");
				Sequence outro = DOTween.Sequence();
				outro.Append(dialogueBloc.DOLocalMoveY(-700, 0.7f).SetEase(Ease.Linear));
				outro.AppendCallback(() => dialogueBloc.gameObject.SetActive(false));
				break;
		}
	}

	private IEnumerator NextStateWithDelay(BATTLE_STATE _nextState, float duration)
	{
		yield return new WaitForSeconds(duration);
		state = _nextState;
	}

	public void StartBattlePhase(EnemyController other)
	{
		enemyController = other;
		state = BATTLE_STATE.INIT;
	}
	
	public void OnAttack()
	{
		state = BATTLE_STATE.PLAYER_ACTION;
	}

	public void Browser(int what)
	{
		if (what == 1)
			BrowserManager.instance.ShowBrowser<Weapon>(GameManager.instance.GetPlayerData.weapons);
		else if (what == 2)
			BrowserManager.instance.ShowBrowser<Item>(GameManager.instance.GetPlayerData.inventory);
	}

	public void UseWeapon(Weapon other)
	{
		playerData.weaponInHand = other;
		state = BATTLE_STATE.PLAYER_ACTION;
	}

	public void UseConsomable(Item other)
	{
		useConsomable = true;
		playerData.inventory.Remove(other);
		playerData.healthPoint = Mathf.Clamp(playerData.healthPoint + 60, 0, playerData.maxHealth);
		state = BATTLE_STATE.PLAYER_ACTION;
	}

	private IEnumerator CheckLife(string message, Callback action, BATTLE_STATE nextState)
    {
		yield return StartCoroutine(TypeSentence(message, action));
		yield return new WaitForSeconds(1.8f);

		if (enemyController.m_data.healthPoint <= 0)
        {
			yield return StartCoroutine(TypeSentence("Congratulations you have defeated the enemy ........... You get a reward !"));
			nbAnimalCount++;
			count.text = nbAnimalCount.ToString();
			state = BATTLE_STATE.END;
        }
		else if (enemyController.m_data.healthPoint <= 0)
        {
			yield return StartCoroutine(TypeSentence("The enemy was stronger than you. You are now dead."));
			state = BATTLE_STATE.END;
        }
		else
			state = nextState;
    }

	IEnumerator TypeSentence(string sentence, Callback callback = null)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.02f);
		}

		yield return new WaitForSeconds(0.3f);

		if (callback != null)
			callback();
	}

	public void Run()
	{
		if (Random.Range(0, 100) >= enemyData.menace)
		{
			StartCoroutine(TypeSentence("You can succeed in leaving", () => state = BATTLE_STATE.END));
		}
		else
		{
			StartCoroutine(TypeSentence("You succeed fail to flee.", () => state = BATTLE_STATE.ENEMY_ACTION));
		}
	}

	private Callback PlayAudio(AudioClip clip, Callback callback)
    {
		sourceAudio.PlayOneShot(clip);
		return callback;
    }
}
