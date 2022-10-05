using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Datas")]
    [SerializeField] public PlayerData playerData;
    [SerializeField] public ListEnemyData listEnemyData;

    [Header("Debug")]
    [SerializeField] private EnemyController enemyPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            CombatSystem.GetInstance().StartNewBattle(enemyPrefab.m_data);
    }

    private static GameManager _instance;
    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameManager();
        }
        return _instance;
    }
}