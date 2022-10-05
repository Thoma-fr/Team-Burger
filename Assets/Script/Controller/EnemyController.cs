using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController , IShootable<PlayerData>
{
    private EnemyData m_data;
    private bool isOverrided;

    private void Awake()
    {
        if(!isOverrided)
            m_data = GameManager.GetInstance().listEnemyData.allEnemiesData[Random.Range(0, GameManager.GetInstance().listEnemyData.allEnemiesData.Count)];
    }

    public void OverrideData(EnemyData overrideData)
    {
        m_data = overrideData;
        isOverrided = true;
    }

    void Update()
    {
        switch (m_data.animal)
        {
            case ANIMAL.DEER:
                break;

            case ANIMAL.RABBIT:
                break;

            case ANIMAL.BOAR:
                break;

            case ANIMAL.PARIS_PIGEON:
                break;

            case ANIMAL.RAKON:
                break;

            case ANIMAL.BEEVER:
                break;

            case ANIMAL.OURS:
                break;

            case ANIMAL.FRANK_O:
                break;
            case ANIMAL.SHINY_NICO:
                break;

            case ANIMAL.SHINY_ALEX:
                break;

            case ANIMAL.SHINY_THOMAS:
                break;

            case ANIMAL.SUPER_JESUS:
                break;

            case ANIMAL.JEROM:
                break;

            default:
                break;
        }
    }

    protected override void Move()
    {

    }

    public void OnInteraction(PlayerData actuator)
    {
        
    }
}
