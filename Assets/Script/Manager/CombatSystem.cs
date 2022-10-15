using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatSystem : MonoBehaviour
{
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
		//player = GameManager.instance.playerData;
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

				// ANCIEN
				/*// <TODO> Type -> override Power -> actuel
				// ---------------- Enemy ---------------- //
				sliderEnemy.maxValue = enemy.maxHealth;
				sliderEnemy.value = enemy.healthPoint;
				hpEnemy.text = enemy.healthPoint + "/" + enemy.maxHealth;
				nameEnemy.text = enemy.m_name;
				imageEnemy.sprite = enemy.m_battleSprite;

				// ---------------- Player ---------------- //
				sliderPlayer.maxValue = player.m_baseData.maxHealth;
				sliderPlayer.value = player.m_baseData.healthPoint;
				hpPlayer.text = player.m_baseData.healthPoint + "/" + player.m_baseData.maxHealth;
				namePlayer.text = player.m_baseData.m_name;
				imagePlayer.sprite = player.m_baseData.m_battleSprite;*/

				state = BATTLE_STATE.INTRO;
				break;

			case BATTLE_STATE.INTRO:
				transparence.alpha = 1;
				transparence.interactable = true;
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

				// ANCIEN
				/*player.m_baseData.healthPoint -= enemy.m_Attacks[Random.Range(0, enemy.m_Attacks.Length)].attForce * enemy.attack - player.m_baseData.defense;
				enemy.healthPoint -= player.m_baseData.attack - enemy.defense;

				if (enemy.healthPoint < 0)
				{
					state = BATTLE_STATE.END;
				}
				else if (player.m_baseData.healthPoint < 0)
				{
					state = BATTLE_STATE.END;
				}
				else
					state = BATTLE_STATE.CHOICE;*/
				break;

			case BATTLE_STATE.END:
				dialogueBloc.SetActive(false);
				commandeBloc.SetActive(false);
				transparence.alpha = 0;
				transparence.interactable = false;
				break;
		}
	}

	private IEnumerator NextStateWithDelay(BATTLE_STATE _nextState, float duration)
	{
		yield return new WaitForSeconds(duration);
		state = _nextState;
	}

	public void StartNewBattle(EnemyData other)
	{
		enemy = other;
		state = BATTLE_STATE.INIT;
	}

	public void PlayerAttack()
    {
		state = BATTLE_STATE.FIGHT;
    }
}
