using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private CombatSystem combatSystem;

    [Header("Default Data")]
    [SerializeField] private ScrPlayerData DeafaultPlayerData;
    [SerializeField] private ScrListEnemy DefaultEnemiesData;
    [SerializeField] private ScrItemsData DefaultItemsData;
    [SerializeField] private ScrWeaponsData DefaultWeaponsData;


    public CombatSystem GetDefaultCombatSystem { get { return combatSystem; } }
    public ScrPlayerData GetDefaultPlayerData { get { return DeafaultPlayerData; } }
    public ScrListEnemy GetDefaultEnemiesData { get { return DefaultEnemiesData; } }
    public ScrItemsData GetDefaultItemsData { get { return DefaultItemsData; } }
    public ScrWeaponsData GetDefaultWeaponsData { get { return DefaultWeaponsData; } }


    private PlayerData playerData;
    public PlayerData GetPlayerData { get { return playerData; } }


    [Header("Debug")]
    public GameObject test;

    public void OnBattleActivation()
    {
        if (test.TryGetComponent<EnemyController>(out EnemyController ec))
            combatSystem.StartBattlePhase(ec);
    }

    private void Awake()
    {

        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        playerData = new PlayerData(DeafaultPlayerData.playerData);
        
    }

}