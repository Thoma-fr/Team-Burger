using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.Progress;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
   
    [SerializeField] public CombatManagerInGame combatSystem;
    public CombatManagerInGame GetCombatSystem { get { return combatSystem; } }

    [Header("Default Data")]
    [SerializeField] private ScrPlayerData DeafaultPlayerData;
    [SerializeField] private ScrListEnemy DefaultEnemiesData;
    [SerializeField] private ScrItemsData DefaultItemsData;
    [SerializeField] private ScrWeaponsData DefaultWeaponsData;


    public ScrPlayerData GetDefaultPlayerData { get { return DeafaultPlayerData; } }
    public ScrListEnemy GetDefaultEnemiesData { get { return DefaultEnemiesData; } }
    public ScrItemsData GetDefaultItemsData { get { return DefaultItemsData; } }
    public ScrWeaponsData GetDefaultWeaponsData { get { return DefaultWeaponsData; } }


    [SerializeField]private PlayerData playerData;
    public PlayerData GetPlayerData { get { return playerData; } }

   

    [Header("Debug")]
    [SerializeField] private EnemyController enemyPrefab;
    
    [Header("FaceCamera")]
    public List<GameObject> faceTheCam = new List<GameObject>();
    public Transform mainCam;
    public GameObject test;

    public bool isShooting;
    public bool hasRotate;

    public bool neeInstaRotate;

    public delegate void PlayerStatChangedDelegate();
    public PlayerStatChangedDelegate onPlayerStatChanged;

    private void Awake()
    {
        playerData = new PlayerData(DeafaultPlayerData.playerData);

        if (instance == null)
            instance = this;
    }

    public void OnBattleActivation(EnemyController ec)
    {
        combatSystem.StartBattlePhase(ec);
    }

    public void UpdateAllUI()
    {
        onPlayerStatChanged();
    }

    public void RotateWorld(Transform rot)
    {
        //foreach (GameObject item in faceTheCam)
        //{

        //    if (isShooting && !item.transform.GetComponent<facingCamera>().hasRotate)
        //    {
        //        Debug.Log("1");
        //        item.transform.GetComponent<facingCamera>().rotateTowardPlayer(rot);
        //    }
        //    else if (!isShooting && item.transform.GetComponent<facingCamera>().hasRotate)
        //    {
        //        Debug.Log("2");
        //        item.transform.GetComponent<facingCamera>().RotateUp();
        //    }

        //}
    }
}