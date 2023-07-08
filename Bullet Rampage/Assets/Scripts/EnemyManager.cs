using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]private WaveSpawner _waveSpawner;
    private List<Transform> enemyList = new List<Transform>();
    
    private void OnEnable()
    {
        _waveSpawner.EnemyGenerateCountAction += EnemyGenerated;
    }

    private void OnDisable()
    {
        _waveSpawner.EnemyGenerateCountAction -= EnemyGenerated;
    }

    void EnemyGenerated(Transform _enemy)
    {
        enemyList.Add(_enemy);
    }
   public void EnemyKilled()
    {
        enemyList.RemoveAt(enemyList.Count -1);
    }
}
