using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("Datas")]
    [SerializeField] public PlayerData playerData;
    [SerializeField] public ListEnemyData listEnemyData;

    [Header("Debug")]
    [SerializeField] private EnemyController enemyPrefab;

    public void OnBattleActivation()
    {
            CombatSystem.instance.StartNewBattle(enemyPrefab.m_data);
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.transform);
    }
}