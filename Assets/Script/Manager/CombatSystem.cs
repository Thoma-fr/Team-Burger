using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.InputSystem;
public class CombatSystem : MonoBehaviour
{
	[Header("Dialogue")]
	[SerializeField] private GameObject dialogueBloc;
	[SerializeField] private TextMeshProUGUI dialogueText;
	[SerializeField] private GameObject commandeBloc;

	[Header("Entity")]
	[SerializeField] private GameObject playerGameObject;
	[SerializeField] private GameObject enemyGameObject;

	/*[Header ("Intro")]
	[SerializeField] private float durationINTRO_START;*/

	private RectMask2D rectMask2D;
	
	private GameObject enemyStat, playerStat;
	private Image imageEnemy, imagePlayer, background;
	private Slider sliderEnemy, sliderPlayer;
	private TextMeshProUGUI nameEnemy, hpEnemy, namePlayer, hpPlayer;

	private CanvasGroup transparence;

	public BATTLE_STATE state = BATTLE_STATE.NONE;
	private BATTLE_STATE lastState = BATTLE_STATE.NONE;

	private GameObject enemiGameObj;
	private EnemyData enemy;
	private PlayerData player;
	private Animator enemyAnimator;

	bool SetSliderValueEnemy = false, SetSliderValuePlayer = false, useConsomable = false;

	public enum BATTLE_STATE
	{
		NONE,
		INIT,
		INTRO,
		START,
		CHOICE,
		FIGHT,
		END,
	}


	void Start()
	{
        
        player = GameManager.instance.GetPlayerData;
		
        // --------------------------------------------------------------------------- DEBUG --------------------------------------------------------------------------- //
        player.weaponInHand = player.weapons[0];
		// ------------------------------------------------------------------------- FIN DEBUG ------------------------------------------------------------------------- //

		transparence = transform.GetComponent<CanvasGroup>();
		transparence.alpha = 0;

		rectMask2D = gameObject.GetComponent<RectMask2D>();
		rectMask2D.padding = new Vector4(0, 347, 0, 347);
		//                                  ^ Bottom  ^ Top

		background = gameObject.transform.GetChild(0).GetComponent<Image>();

		imagePlayer = playerGameObject.transform.GetChild(1).GetComponent<Image>();
		playerStat = playerGameObject.transform.GetChild(0).gameObject;
		sliderPlayer = playerStat.transform.GetChild(0).GetComponent<Slider>();
		namePlayer = playerStat.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
		hpPlayer = playerStat.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();

		imageEnemy = enemyGameObject.transform.GetChild(1).GetComponent<Image>();
		enemyStat = enemyGameObject.transform.GetChild(0).gameObject;
		sliderEnemy = enemyStat.transform.GetChild(0).GetComponent<Slider>();
		nameEnemy = enemyStat.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
		hpEnemy = enemyStat.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
		enemyAnimator = imageEnemy.GetComponent<Animator>();
	}

	void Update()
	{
        if (SetSliderValueEnemy)
			hpEnemy.text = (int)sliderEnemy.value + "/" + enemy.maxHealth;
        else if(SetSliderValuePlayer)
			hpPlayer.text = (int)sliderPlayer.value + "/" + player.maxHealth;


		if (state == lastState)
			return;

		lastState = state;

		switch (state)
		{
			case BATTLE_STATE.INIT:
				
                Cursor.visible = true;
                PlayerController.playerInstance.playerMode = PlayerController.PLAYER_MODE.COMBAT_MODE;
                background.color = new Color32(40, 40, 40, 255);
				// dialogueBloc.transform.localPosition = new Vector2(dialogueBloc.transform.localPosition.x, -40);
				dialogueBloc.SetActive(false);
				// commandeBloc.transform.localPosition = new Vector2(commandeBloc.transform.localPosition.x, -40);
				commandeBloc.SetActive(false);

				rectMask2D.padding = new Vector4(0, 347, 0, 347);

				// ---------------- Enemy ---------------- //
				sliderEnemy.maxValue = enemy.maxHealth;
				sliderEnemy.value = enemy.healthPoint;
				hpEnemy.text = enemy.healthPoint + "/" + enemy.maxHealth;
				nameEnemy.text = enemy.name;
				imageEnemy.sprite = enemy.battleSprite;
				imageEnemy.color = new Color32(40, 40, 40, 0);
				enemyStat.transform.localPosition = new Vector2(-400, enemyStat.transform.localPosition.y);
				imageEnemy.gameObject.transform.localPosition = new Vector2(-200, 0);

				// ---------------- Player ---------------- //
				sliderPlayer.maxValue = player.maxHealth;
				sliderPlayer.value = player.healthPoint;
				hpPlayer.text = player.healthPoint + "/" + player.maxHealth;
				namePlayer.text = player.name;
				imagePlayer.sprite = player.battleSprite;
				imagePlayer.color = new Color32(40, 40, 40, 0);
				playerStat.transform.localPosition = new Vector2(400, playerStat.transform.localPosition.y);
				imagePlayer.gameObject.transform.localPosition = new Vector2(200, imagePlayer.gameObject.transform.localPosition.y);

				state = BATTLE_STATE.INTRO;
				break;

			case BATTLE_STATE.INTRO:
				transparence.alpha = 1;

				Sequence myStartSequence = DOTween.Sequence();
				// Background
				myStartSequence.Append(DOTween.To(() => rectMask2D.padding, window => rectMask2D.padding = window, new Vector4(0, 0, 0, 0), 1));
				myStartSequence.Insert(0.5f, background.DOColor(new Color(1, 1, 1), 0.5f));
				myStartSequence.PrependInterval(0.7f);
				// Sprite Position
				myStartSequence.Append(imageEnemy.gameObject.transform.DOLocalMoveX(0, 2f).SetEase(Ease.Linear));
				myStartSequence.Join(imagePlayer.gameObject.transform.DOLocalMoveX(0, 2f).SetEase(Ease.Linear));
				myStartSequence.Join(imageEnemy.DOFade(1, 0.5f));
				myStartSequence.Join(imagePlayer.DOFade(1, 0.5f));
				// Sprite Color
				myStartSequence.Insert(2.5f, imageEnemy.DOColor(new Color(1, 1, 1), 0.7f));
				myStartSequence.Join(imagePlayer.DOColor(new Color(1, 1, 1), 0.7f));
				// Statistics
				myStartSequence.Append(enemyStat.transform.DOLocalMoveX(-180, 0.5f).SetEase(Ease.OutBack));
				myStartSequence.Insert(4f, playerStat.transform.DOLocalMoveX(180, 0.5f).SetEase(Ease.OutBack));
				// Callback
				myStartSequence.AppendCallback(EndIntroduction);

				/*if (enemy._animatorCtrl != null)
					myStartSequence.AppendCallback(() => enemyAnimator.runtimeAnimatorController = enemy._animatorCtrl);*/

				break;

			case BATTLE_STATE.START:
				dialogueBloc.SetActive(true);
				// dialogueBloc.transform.DOLocalMoveY(25, 0.5f);
				dialogueText.text = "Un jeune beer est apparue. Vous sortez un gros fusil";
				StartCoroutine(NextStateWithDelay(BATTLE_STATE.CHOICE, 3));
				break;

			case BATTLE_STATE.CHOICE:
				commandeBloc.SetActive(true);
				// commandeBloc.transform.DOLocalMoveY(25, 0.5f);
				dialogueText.text = "WHAT SHOULD YOU DO ?";
				break;

			case BATTLE_STATE.FIGHT:
				dialogueText.text = "";
				commandeBloc.SetActive(false);
				StartCoroutine(FightPhase());
				break;

			case BATTLE_STATE.END:				
				transparence.interactable = false;
				transparence.blocksRaycasts = false;
                Sequence myEndSequence = DOTween.Sequence();
				myEndSequence.AppendInterval(1f);
				myEndSequence.Append(transparence.DOFade(0, 1));
                myEndSequence.AppendCallback(() => GameManager.instance.faceTheCam.Remove(enemiGameObj));
                myEndSequence.AppendCallback(() => Destroy(enemiGameObj));
                Cursor.visible = false;
                PlayerController.playerInstance.playerMode = PlayerController.PLAYER_MODE.ADVENTURE_MODE;
				PlayerController.playerInstance.isVise = false;
                break;
		}
	}

	private void EndIntroduction()
    {
		state = BATTLE_STATE.START;
		transparence.interactable = true;
		transparence.blocksRaycasts = true;
	}

	private IEnumerator NextStateWithDelay(BATTLE_STATE _nextState, float duration)
	{
		yield return new WaitForSeconds(duration);
		state = _nextState;
	}

	private IEnumerator FightPhase()
    {
		yield return new WaitForSeconds(0.5f);
		dialogueText.text = "Le chasseur utilise " + player.weaponInHand.name;
		yield return new WaitForSeconds(0.5f);

		// ATTACK JOUEUR
		if (!useConsomable)
		{
			enemy.healthPoint -= player.weaponInHand.damage;
			/*if (enemy._animatorCtrl != null)
				enemyAnimator.SetTrigger("Hit");*/

			imageEnemy.transform.DOShakePosition(0.7f, enemy.maxHealth / player.weaponInHand.damage * 2, 10, 90);
			SetSliderValueEnemy = true;
			sliderEnemy.DOValue(enemy.healthPoint, 1.5f);
			yield return new WaitForSeconds(3f);
			SetSliderValueEnemy = false;
		}
		else
			useConsomable = false;

		// L'enemie n'a plus de vie
		if (enemy.healthPoint <= 0)
        {
			dialogueText.text = "L'ennemie est KO !";
			yield return new WaitForSeconds(0.5f);
			Sequence mySequence = DOTween.Sequence();
			mySequence.Append(imageEnemy.transform.DOLocalMoveY(-80, 0.5f));
			mySequence.Join(enemyStat.transform.DOLocalMoveX(-400, 0.5f));
			mySequence.Insert(0.2f, imageEnemy.DOFade(0, 0.3f));

			yield return new WaitForSeconds(1f);
			dialogueText.text = "Félicitaion, tu as gagné 2512 de tune !";
			state = BATTLE_STATE.END;
		}
        else
        {
			Attack enemyAtt = enemy.attacks[Random.Range(0, enemy.attacks.Count - 1)];
			dialogueText.text = "L'ennemie utilise " + enemyAtt.attName;
			yield return new WaitForSeconds(0.5f);

			// ATTACK ENNEMIE
			player.healthPoint -= enemyAtt.damage;
			// SetSlider(sliderPlayer, hpPlayer, player.healthPoint, player.maxHealth);

			/*if (enemy._animatorCtrl != null)
				enemyAnimator.SetTrigger("Attack");*/

			yield return new WaitForSeconds(0.2f);

			this.transform.DOShakePosition(0.7f, player.maxHealth / enemyAtt.damage * 2, 10, 90);
			SetSliderValuePlayer = true;
			sliderPlayer.DOValue(player.healthPoint, 1.5f);
			yield return new WaitForSeconds(3f);
			SetSliderValuePlayer = false;

			// Le joueur n'a plus de vie
			if (enemy.healthPoint <= 0)
            {
				dialogueText.text = "Le chasseur est KO !";
				yield return new WaitForSeconds(0.5f);
				Sequence mySequence = DOTween.Sequence();
				mySequence.Append(imagePlayer.transform.DOLocalMoveY(-80, 0.5f));
				mySequence.Join(playerStat.transform.DOLocalMoveX(400, 0.5f));
				mySequence.Insert(0.2f, imagePlayer.DOFade(0, 0.3f));

				yield return new WaitForSeconds(1f);
				state = BATTLE_STATE.END;
            }
			else
				state = BATTLE_STATE.CHOICE;
        }
	}

	public void StartBattlePhase(EnemyController other)
	{
		enemy = other.m_data;
		enemiGameObj = other.gameObject;
        state = BATTLE_STATE.INIT;
	}

	public void Attack()
    {
		state = BATTLE_STATE.FIGHT;
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
		player.weaponInHand = other;
		state = BATTLE_STATE.FIGHT;
	}

	public void UseConsomable(Item other)
    {
		useConsomable = true;
		state = BATTLE_STATE.FIGHT;
	}

	public void Run()
    {
		if(Random.Range(0,100) >= enemy.menace)
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
	}
}
