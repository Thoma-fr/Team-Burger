using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : BaseController , IShootable<PlayerData>
{
    public int iDEnemy;
    public EnemyData m_data { get; private set; }

    private void Start()
    {
        if(iDEnemy < GameManager.instance.GetDefaultEnemiesData.allEnemiesData.Count)
        m_data = new EnemyData(GameManager.instance.GetDefaultEnemiesData.allEnemiesData[iDEnemy]);
    }

    protected override void Move()
    {
    }

    public void OnInteraction(PlayerData actuator)
    { 
    }
}
