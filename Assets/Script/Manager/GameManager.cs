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
    
    [Header("FaceCamera")]
    public List<GameObject> faceTheCam = new List<GameObject>();
    public Camera mainCam;

    public bool isShooting;
    public void OnBattleActivation()
    {
            CombatSystem.instance.StartNewBattle(enemyPrefab.m_data);
    }
    void Update()
    {
        foreach (GameObject item in faceTheCam)
        {
            item.transform.rotation = mainCam.transform.rotation;
        }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.transform);
    }
    enum PLAYER_MODE
    {
        ADVENTURE_MODE,
        SHOOTING_MODE,
    }
}