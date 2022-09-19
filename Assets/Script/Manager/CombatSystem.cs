using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
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

    private BATTLE_STATE state = BATTLE_STATE.NONE;
    private BATTLE_STATE lastState = BATTLE_STATE.NONE;

    public float durationINTRO_START;

    void Start()
    {
        ///Init battle System
        state = BATTLE_STATE.INIT;
    }

    void Update()
    {
        if (state == lastState)
            return;

        lastState = state;

        switch (state)
        {
            case BATTLE_STATE.INIT:

                Debug.Log("INIT");
                state = BATTLE_STATE.INTRO;
                break;

            case BATTLE_STATE.INTRO:

                Debug.Log("INTRO");
                StartCoroutine(NextStateWithDelay(BATTLE_STATE.START, durationINTRO_START));
                break;

            case BATTLE_STATE.START:

                Debug.Log("START");
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
}
