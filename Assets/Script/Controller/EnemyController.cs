using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController , IShootable<PlayerData>
{
    public EnemyData m_data { get; private set; }

    private void Start()
    {
        m_data = new EnemyData(GameManager.instance.GetDefaultEnemiesData.allEnemiesData[0]);
    }

    protected override void Move()
    {

    }

    public void OnInteraction(PlayerData actuator)
    {
        
    }
}
