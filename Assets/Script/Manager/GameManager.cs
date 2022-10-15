using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private CombatSystem combatSystem;

    [Header("Default Data")]
    [SerializeField] private ScrPlayerData PlayerData;
    [SerializeField] private ScrListEnemy EnemiesData;
    [SerializeField] private ScrItemsData ItemsData;
    [SerializeField] private ScrWeaponsData WeaponsData;


    public CombatSystem GetCombatSystem { get { return combatSystem; } }
    public ScrPlayerData GetPlayerData { get { return PlayerData; } }
    public ScrListEnemy GetEnemiesData { get { return EnemiesData; } }
    public ScrItemsData GetItemsData { get { return ItemsData; } }
    public ScrWeaponsData GetWeaponsData { get { return WeaponsData; } }

    [Header("Debug")]
    public GameObject test;

    public void OnBattleActivation()
    {
        
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.transform);
    }
}