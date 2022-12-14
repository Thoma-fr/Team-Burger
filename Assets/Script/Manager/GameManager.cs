using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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

    [Header("Player Death")]
    [SerializeField] private AudioClip deathClip;
    private AudioSource sourceAudio;
    
    [Header("FaceCamera")]
    public List<GameObject> faceTheCam = new List<GameObject>();
    public Transform mainCam;

    // public bool isShooting;
    // public bool hasRotate;
    // public bool neeInstaRotate;

    public delegate void PlayerStatChangedDelegate();
    public PlayerStatChangedDelegate onPlayerStatChanged;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        playerData = new PlayerData(DeafaultPlayerData.playerData);
        playerData.weaponInHand = DefaultWeaponsData.weapons[4];
        for (int i = 0; i < 5; i++)
        {
            playerData.weapons.Add(DefaultWeaponsData.weapons[Random.Range(0, DefaultWeaponsData.weapons.Count)]);
        }

        for (int i = 0; i < 15; i++)
        {
            playerData.inventory.Add(DefaultItemsData.itemsData[Random.Range(0, DefaultItemsData.itemsData.Count)]);
        }
    }

    private void Start()
    {
        sourceAudio = gameObject.GetComponent<AudioSource>();
    }

    public void OnBattleActivation(EnemyController ec)
    {
        combatSystem.StartBattlePhase(ec);
    }

    public void UpdateAllUI()
    {
        onPlayerStatChanged();

        if (playerData.healthPoint <= 0)
            StartCoroutine(PlayerDeath());
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

    private IEnumerator PlayerDeath()
    {
        sourceAudio.PlayOneShot(deathClip);
        yield return new WaitForSeconds(1.0f);
        LevelLoader.instance.LoadSceneAnIndex(1);
    }
}