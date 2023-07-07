using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _EnemyCountText;
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _survivedTimeText;
    [SerializeField] private Text _scoreText;
    
    private int enemyKillCount = 0;
    private float timeCount = 0f;
    
    [SerializeField]private float survivedTime = 0f;
    private void OnEnable()
    {
        Enemy.EnemyKillCountAction += EnemyKillCount;
        _player.PlayerDeadAction += result;
    }
    
    private void OnDisable()
    {
        Enemy.EnemyKillCountAction -= EnemyKillCount;
        _player.PlayerDeadAction -= result;
    }
    private void Start()
    {
        _EnemyCountText.text = "Enemy Killed: " + enemyKillCount;
        _timerText.text = "Time: 0";
        _survivedTimeText.enabled = false;
    }

    private void Update()
    {
        _EnemyCountText.text = "Enemy Killed: " + enemyKillCount;
        timeCount = Time.time;
        _timerText.text = "Time:" + timeCount;
        survivedTime = timeCount;

    }

    private void EnemyKillCount()
    {
        enemyKillCount++;
        Debug.Log(enemyKillCount);
    }

    void result()
    {
        _EnemyCountText.enabled = false;
        _timerText.enabled = false;
        _survivedTimeText.enabled = true;
        _survivedTimeText.text = "Survived Time: " + survivedTime;
    }
    
}
