using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    [SerializeField] private ANIMAL m_animal;

    private void Awake()
    {
        m_animal = GameManager.GetInstance().listEnemyData.allEnemiesData[Random.Range(0, GameManager.GetInstance().listEnemyData.allEnemiesData.Count)].animal;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    protected override void Move()
    {

    }
}
