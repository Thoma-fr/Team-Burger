using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
	public enum BATTLE_STATE
	{
		NONE,
		INTRO,
		START,
		CHOICE,
		FIGHT,
		END,
	}

	private static CombatSystem _instance;

	private BATTLE_STATE state = BATTLE_STATE.NONE;
	private BATTLE_STATE lastState = BATTLE_STATE.NONE;

	private EnemyData enemy = null;
	private Player player;

	public float durationINTRO_START;

	void Start()
	{
		///Init battle System
		state = BATTLE_STATE.INTRO;
	}

	void Update()
	{
		if (state == lastState)
			return;

		lastState = state;

		switch (state)
		{
			case BATTLE_STATE.INTRO:

				if (enemy)
				{
					state = BATTLE_STATE.START;
					Debug.Log("Play animation fondu");
					StartCoroutine(NextStateWithDelay(BATTLE_STATE.START, durationINTRO_START));
				}
				else
				{
					state = BATTLE_STATE.NONE;
				}

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

	public void StartBattle(EnemyData value, Player playerVal)
	{
		state = BATTLE_STATE.INTRO;
		player = playerVal;
		enemy = value;
	}

	private CombatSystem() { }

	public static CombatSystem GetInstance()
	{
		if(_instance == null)
		{
			_instance = new CombatSystem();
		}
		return _instance;
	}
}
