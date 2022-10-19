using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatSystem : MonoBehaviour
{
	[SerializeField] private BrowserManager browser;

	[Header("Dialogue")]
	[SerializeField] private GameObject dialogueBloc;
	[SerializeField] private TextMeshProUGUI dialogueText;
	[SerializeField] private GameObject commandeBloc;

	[Header("Enemy Status")]
	[SerializeField] private Image imageEnemy;
	[SerializeField] private Slider sliderEnemy;
	[SerializeField] private TextMeshProUGUI nameEnemy;
	[SerializeField] private TextMeshProUGUI hpEnemy;

	[Header("Player Status")]
	[SerializeField] private Image imagePlayer;
	[SerializeField] private Slider sliderPlayer;
	[SerializeField] private TextMeshProUGUI namePlayer;
	[SerializeField] private TextMeshProUGUI hpPlayer;
	
	[Header ("Intro")]
	[SerializeField] private float durationINTRO_START;
	
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
	}

	void Update()
	{
		if (state == lastState)
			return;

		lastState = state;

		switch (state)
		{
			case BATTLE_STATE.INIT:
				// ---------------- Enemy ---------------- //
				sliderEnemy.maxValue = enemy.maxHealth;
				sliderEnemy.value = enemy.healthPoint;
				hpEnemy.text = enemy.healthPoint + "/" + enemy.maxHealth;
				nameEnemy.text = enemy.name;
				imageEnemy.sprite = enemy.battleSprite;

				// ---------------- Player ---------------- //
				sliderPlayer.maxValue = player.maxHealth;
				sliderPlayer.value = player.healthPoint;
				hpPlayer.text = player.healthPoint + "/" + player.maxHealth;
				namePlayer.text = player.name;
				imagePlayer.sprite = player.battleSprite;

				 state = BATTLE_STATE.INTRO;
				break;

			case BATTLE_STATE.INTRO:
				transparence.alpha = 1;
				transparence.interactable = true;
				transparence.blocksRaycasts = true;
				state = BATTLE_STATE.START;
				break;

			case BATTLE_STATE.START:
				Debug.Log("DIalogue");
				dialogueBloc.SetActive(true);
				state = BATTLE_STATE.CHOICE;
				break;

			case BATTLE_STATE.CHOICE:
				commandeBloc.SetActive(true);
				dialogueText.text = "WHAT NAT SHOULD DO ?";
				Debug.Log("CHOICE + affichage choix");
				break;

			case BATTLE_STATE.FIGHT:
				dialogueText.text = "";
				commandeBloc.SetActive(false);

				enemy.healthPoint -= player.weapons[0].damage;
				SetSlider(sliderEnemy, hpEnemy, enemy.healthPoint, enemy.maxHealth);
				if (enemy.healthPoint <= 0)
                {
					Debug.Log("VICTOIRE");
					state = BATTLE_STATE.END;
					break;
                }

				player.healthPoint -= enemy.attacks[Random.Range(0, enemy.attacks.Count - 1)].damage;
				SetSlider(sliderPlayer, hpPlayer, player.healthPoint, player.maxHealth);
				if (enemy.healthPoint <= 0)
                {
					Debug.Log("DEFAITE");
					state = BATTLE_STATE.END;
					break;
                }

				state = BATTLE_STATE.CHOICE;
				break;

			case BATTLE_STATE.END:
				dialogueBloc.SetActive(false);
				commandeBloc.SetActive(false);
				transparence.alpha = 0;
				transparence.interactable = false;
				transparence.blocksRaycasts = false;
				break;
		}
	}

	private IEnumerator NextStateWithDelay(BATTLE_STATE _nextState, float duration)
	{
		yield return new WaitForSeconds(duration);
		state = _nextState;
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
			browser.ShowBrowser<Weapon>(GameManager.instance.GetPlayerData.weapons);
		else if (what == 2)
			browser.ShowBrowser<Item>(GameManager.instance.GetPlayerData.inventory);
	}
}
