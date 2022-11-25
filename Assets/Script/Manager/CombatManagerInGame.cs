using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CombatManagerInGame : MonoBehaviour
{
	private BATTLE_STATE state = BATTLE_STATE.NONE;
	private BATTLE_STATE lastState = BATTLE_STATE.NONE;

	private EnemyController enemyController;
	private EnemyData enemyData;
	private PlayerData playerData;

	[SerializeField] private Transform dialogueBloc;
	private TextMeshProUGUI dialogueText;

	[SerializeField] private Transform commandBloc;
	[SerializeField] private Transform playerStat;

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
				if(enemyController)
                {
                    //PlayerController.playerInstance.playerMode = PlayerController.PLAYER_MODE.COMBAT_MODE;
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
				StartCoroutine(TypeSentence("Un ennemi apparait ! Il n'est pas content.", () => waitPlayerInput = true));

				break;

			case BATTLE_STATE.CHOICE:
				commandBloc.gameObject.SetActive(true);
				commandBloc.localPosition = new Vector2(1160, commandBloc.localPosition.y);
				StartCoroutine(TypeSentence("Que voulez-vous faire ?", () => commandBloc.DOLocalMoveX(commandeBoxStartX, 0.2f).SetEase(Ease.Linear)));

				break;

			case BATTLE_STATE.PLAYER_ACTION:
				if (useConsomable)
                {
					StartCoroutine(TypeSentence("Vous utilisez un item extraordinaire ! Wow, bien jouer ?", () => state = BATTLE_STATE.ENEMY_ACTION));
					break;
                }

				StartCoroutine(CheckLife("Vous utilisez votre fusil !!!", () => enemyController.TakeDamage(playerData.weaponInHand.damage), BATTLE_STATE.ENEMY_ACTION));
				GameManager.instance.UpdateAllUI();
				break;

			case BATTLE_STATE.ENEMY_ACTION:
				playerData.healthPoint -= enemyController.EnemyAttacking(out string attName);

				StartCoroutine(CheckLife("Votre enemy vous attack avec " + attName + " !!!", () => GameManager.instance.UpdateAllUI(), BATTLE_STATE.CHOICE));
				break;

			case BATTLE_STATE.END:
				enemyController.gameObject.GetComponent<NAVAI>().die();
				PlayerController.playerInstance.playerMode = PlayerController.PLAYER_MODE.ADVENTURE_MODE;

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
		state = BATTLE_STATE.PLAYER_ACTION;
	}

	private IEnumerator CheckLife(string message, Callback action, BATTLE_STATE nextState)
    {
		yield return StartCoroutine(TypeSentence(message, action));
		yield return new WaitForSeconds(1.8f);

		if (enemyController.m_data.healthPoint <= 0)
        {
			yield return StartCoroutine(TypeSentence("Félicitation vous avez vaincu l'ennemi. ........... Vous récupérer une récompense !"));
			state = BATTLE_STATE.END;
        }
		else if (enemyController.m_data.healthPoint <= 0)
        {
			yield return StartCoroutine(TypeSentence("L'ennemi a été plus fort que vous. Vous êtes désormait mort. Pour rejouer veuillez aller sur https//ecodestructor.shop et acheter le jeu pour 19.99€."));
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

	/*
	public void Run()
	{
		if (Random.Range(0, 100) >= enemyData.menace)
		{
			dialogueText.text = "Vous réusiser à partir";
			state = BATTLE_STATE.END;
		}
		else
		{
			dialogueText.text = "Vous réusiser échouer à fuire";
			commandeBloc.SetActive(false);
			useConsomable = true;
			state = BATTLE_STATE.FIGHT;
		}
	}*/
}
