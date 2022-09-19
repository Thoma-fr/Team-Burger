using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private Player player;
    private List<Enemy> listEnemies

    private GameManager()
    {

    }

    public static GameManager GetInstance()
    {
        if(_Instance)
        return _instance;
    }
}
