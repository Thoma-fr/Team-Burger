using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatSystem : MonoBehaviour
{
	[Header ("Enemy Status")]
	[SerializeField] private Slider sliderEnemy;
	[SerializeField] private TextMeshPro nameEnemy;
	[SerializeField] private TextMeshPro hpEnemy;

	[Header("Player Status")]
	[SerializeField] private Slider sliderPlayer;
	[SerializeField] private TextMeshPro namePlayer;
	[SerializeField] private TextMeshPro hpPlayer;
	
	[Header ("Intro")]
	[SerializeField] private float durationINTRO_START;


	private static CombatSystem _instance;
	public static CombatSystem GetInstance()
	{
		if(_instance == null)
		{
			_instance = new CombatSystem();
		}
		return _instance;
	}

	private BATTLE_STATE state = BATTLE_STATE.NONE;
	private BATTLE_STATE lastState = BATTLE_STATE.NONE;

	private EnemyData enemy;
	private PlayerData player;


	void Start()
	{
		///Init battle System
		state = BATTLE_STATE.INTRO;
		player = GameManager.GetInstance().playerData;
	}

	void Update()
	{
		if (state == lastState)
			return;

		lastState = state;

		switch (state)
		{
			case BATTLE_STATE.INTRO:
				StartCoroutine(NextStateWithDelay(BATTLE_STATE.START, durationINTRO_START));
				break;

			case BATTLE_STATE.START:
				Debug.Log("Play animation start");
				break;

			case BATTLE_STATE.CHOICE:

				Debug.Log("CHOICE");
				break;

			case BATTLE_STATE.FIGHT:
				
				Debug.Log("FIGHT");
				break;

			case BATTLE_STATE.END:

				Debug.Log("END");
				break;

		}
	}

	private IEnumerator NextStateWithDelay(BATTLE_STATE _nextState, float duration)
	{
		yield return new WaitForSeconds(duration);
		state = _nextState;
	}

	public void StartNewBattle(EnemyController enemy)
	{
		// Full health -> max + actuel
		// Name -> override
		// Type -> override
		// Power -> actuel
	}

	public enum BATTLE_STATE
	{
		NONE,
		INTRO,
		START,
		CHOICE,
		FIGHT,
		END,
	}
}
