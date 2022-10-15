using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController , IShootable<PlayerData>
{
    public EnemyData m_data { get; private set; }
    private bool isOverrided = true;

    private void Start()
    {
        //if(isOverrided)
            //m_data = GameManager.instance.listEnemyData.allEnemiesData[0];
            //m_data = GameManager.GetInstance().listEnemyData.allEnemiesData[Random.Range(0, GameManager.GetInstance().listEnemyData.allEnemiesData.Count)];
    }

    public void OverrideData(EnemyData overrideData)
    {
        m_data = overrideData;
        isOverrided = true;
    }

    protected override void Move()
    {

    }

    public void OnInteraction(PlayerData actuator)
    {
        
    }
}
