using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header ("Datas")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private ListEnemyData listEnemyData;

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