using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _EnemyCount;
    [SerializeField] private Text _Timer;
    [SerializeField] private Text _SurvivedTime;
    [SerializeField] private Text _Score;
    [SerializeField] private int enemyKillCount = 0;

    
    private void OnEnable()
    {
        Enemy.EnemyKillCountAction += EnemyKillCount;
    }
    
    private void OnDisable()
    {
        Enemy.EnemyKillCountAction -= EnemyKillCount;
    }
    private void Start()
    {
        _EnemyCount.text = "Enemy Killed: " + enemyKillCount;
        _Timer.text = "Time: 0";
    }

    private void Update()
    {
        _EnemyCount.text = "Enemy Killed: " + enemyKillCount;
        _Timer.text = "Time:" + Time.time.ToString();
    }

    private void EnemyKillCount()
    {
        enemyKillCount++;
        Debug.Log(enemyKillCount);
    }
}
