using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] public CombatSystem combatSystem;
    public CombatSystem GetCombatSystem { get { return combatSystem; } }

    [Header("Default Data")]
    [SerializeField] private ScrPlayerData DeafaultPlayerData;
    [SerializeField] private ScrListEnemy DefaultEnemiesData;
    [SerializeField] private ScrItemsData DefaultItemsData;
    [SerializeField] private ScrWeaponsData DefaultWeaponsData;


    public ScrPlayerData GetDefaultPlayerData { get { return DeafaultPlayerData; } }
    public ScrListEnemy GetDefaultEnemiesData { get { return DefaultEnemiesData; } }
    public ScrItemsData GetDefaultItemsData { get { return DefaultItemsData; } }
    public ScrWeaponsData GetDefaultWeaponsData { get { return DefaultWeaponsData; } }


    private PlayerData playerData;
    public PlayerData GetPlayerData { get { return playerData; } }



    [Header("Debug")]
    [SerializeField] private EnemyController enemyPrefab;
    
    [Header("FaceCamera")]
    public List<GameObject> faceTheCam = new List<GameObject>();
    public Transform mainCam;
    public GameObject test;

    public bool isShooting;
    public bool hasRotate;
    public void OnBattleActivation()
    {
        if (test.TryGetComponent<EnemyController>(out EnemyController ec))
            combatSystem.StartBattlePhase(ec);
    }
    void Update()
    {
        foreach (GameObject item in faceTheCam)
        {

            if (isShooting && !item.transform.GetComponent<facingCamera>().hasRotate)
            {
                Debug.Log("1");
                item.transform.GetComponent<facingCamera>().rotateTowardPlayer(mainCam);
            }
            else if(!isShooting && item.transform.GetComponent<facingCamera>().hasRotate)
            {
                Debug.Log("2");
                item.transform.GetComponent<facingCamera>().rotateTowardPlayer(mainCam);
            }
            
        }
    }
    private void Awake()
    {
        playerData = new PlayerData(DeafaultPlayerData.playerData);

        if (instance == null)
            instance = this;
    }
}