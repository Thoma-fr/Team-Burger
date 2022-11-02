using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
	private Image background;
	
	private GameObject enemyStat;
	private Image imageEnemy;
	private Slider sliderEnemy;
	private TextMeshProUGUI nameEnemy;
	private TextMeshProUGUI hpEnemy;

	private GameObject playerStat;
	private Image imagePlayer;
	private Slider sliderPlayer;
	private TextMeshProUGUI namePlayer;
	private TextMeshProUGUI hpPlayer;

	private CanvasGroup transparence;

	private BATTLE_STATE state = BATTLE_STATE.NONE;
	private BATTLE_STATE lastState = BATTLE_STATE.NONE;

	private EnemyData enemy;
	private PlayerData player;

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
	}

	void Update()
	{
		if (state == lastState)
			return;

		lastState = state;

		switch (state)
		{
			case BATTLE_STATE.INIT:
				background.color = new Color32(40, 40, 40, 255);
				dialogueBloc.SetActive(false);
				commandeBloc.SetActive(false);

				// ---------------- Enemy ---------------- //
				sliderEnemy.maxValue = enemy.maxHealth;
				sliderEnemy.value = enemy.healthPoint;
				hpEnemy.text = enemy.healthPoint + "/" + enemy.maxHealth;
				nameEnemy.text = enemy.name;
				imageEnemy.sprite = enemy.battleSprite;
				imageEnemy.color = new Color32(40, 40, 40, 0);
				enemyStat.transform.localPosition = new Vector2(-400, enemyStat.transform.localPosition.y);
				imageEnemy.gameObject.transform.localPosition = new Vector2(-200, imageEnemy.gameObject.transform.localPosition.y);

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

				Sequence mySequence = DOTween.Sequence();
				// Background
				mySequence.Append(DOTween.To(() => rectMask2D.padding, window => rectMask2D.padding = window, new Vector4(0, 0, 0, 0), 1));
				mySequence.Insert(0.5f, background.DOColor(new Color(1, 1, 1), 0.5f));
				mySequence.PrependInterval(0.7f);
				// Sprite Position
				mySequence.Append(imageEnemy.gameObject.transform.DOLocalMoveX(0, 2f).SetEase(Ease.Linear));
				mySequence.Join(imagePlayer.gameObject.transform.DOLocalMoveX(0, 2f).SetEase(Ease.Linear));
				mySequence.Join(imageEnemy.DOFade(1, 0.5f));
				mySequence.Join(imagePlayer.DOFade(1, 0.5f));
				// Sprite Color
				mySequence.Insert(2.5f, imageEnemy.DOColor(new Color(1, 1, 1), 0.7f));
				mySequence.Join(imagePlayer.DOColor(new Color(1, 1, 1), 0.7f));
				// Statistics
				mySequence.Append(enemyStat.transform.DOLocalMoveX(-180, 0.5f).SetEase(Ease.OutBack));
				mySequence.Insert(4f, playerStat.transform.DOLocalMoveX(180, 0.5f).SetEase(Ease.OutBack));
				// Callback
				mySequence.AppendCallback(EndIntroduction);
				break;

			case BATTLE_STATE.START:
				dialogueBloc.SetActive(true);
				dialogueText.text = "Un jeune beer est apparue. Vous sortez un gros fusil";
				StartCoroutine(NextStateWithDelay(BATTLE_STATE.CHOICE, 2));
				break;

			case BATTLE_STATE.CHOICE:
				commandeBloc.SetActive(true);
				dialogueText.text = "WHAT NAT SHOULD DO ?";
				break;

			case BATTLE_STATE.FIGHT:
				dialogueText.text = "";
				commandeBloc.SetActive(false);

				StartCoroutine(FightPhase());
				break;

			case BATTLE_STATE.END:
				dialogueBloc.SetActive(false);
				commandeBloc.SetActive(false);
				rectMask2D.padding = new Vector4(0, 347, 0, 347);
				transparence.alpha = 0;
				transparence.interactable = false;
				transparence.blocksRaycasts = false;
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
		yield return null;

		// Attack du joueur
		enemy.healthPoint -= player.weaponInHand.damage;
		SetSlider(sliderEnemy, hpEnemy, enemy.healthPoint, enemy.maxHealth);

		sliderEnemy.DOValue(enemy.healthPoint, 1.5f);
		yield return new WaitForSeconds(1.8f);

		if (enemy.healthPoint <= 0)
			state = BATTLE_STATE.END;

		// Attack de l'ennemie
		player.healthPoint -= enemy.attacks[Random.Range(0, enemy.attacks.Count - 1)].damage;
		SetSlider(sliderPlayer, hpPlayer, player.healthPoint, player.maxHealth);
		if (enemy.healthPoint <= 0)
		{
			state = BATTLE_STATE.END;
		}

		state = BATTLE_STATE.CHOICE;
	}

	private void SetSlider(Slider _slider, TextMeshProUGUI _text, int _current, int _max)
    {
		_slider.value = _current;
		_text.text = _current + "/" + _max;
    }

	public void StartBattlePhase(EnemyController other)
	{
		enemy = other.m_data;
		state = BATTLE_STATE.INIT;
	}

	public void PlayerAttack()
    {
		state = BATTLE_STATE.FIGHT;
		Debug.Log("Attack");
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
		Debug.Log("Utiliser arm");
    }

	public void UseConsomable(Item other)
    {
		Debug.Log("Utiliser item");
	}
}
