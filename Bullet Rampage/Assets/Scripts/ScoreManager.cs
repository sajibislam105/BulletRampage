using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _enemyCountText;
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _survivedTimeText;
    [SerializeField] private Text _playerHealthText;
    
    private int enemyKillCount = 0;
    private float timeCount = 0f;
    private float _playerHealth;
    
    [SerializeField]private float survivedTime = 0f;
    private void OnEnable()
    {
        Enemy.EnemyKillCountAction += EnemyKillCount;
        _player.PlayerDeadAction += result;
        _player.PlayerHealthUpdateAction += HealthUpdate;
    }
    private void OnDisable()
    {
        Enemy.EnemyKillCountAction -= EnemyKillCount;
        _player.PlayerDeadAction -= result;
        _player.PlayerHealthUpdateAction -= HealthUpdate;
    }
    private void Start()
    {
        _enemyCountText.text = "Enemy Killed: " + enemyKillCount;
        _timerText.text = "Time: 0";
        _survivedTimeText.enabled = false;
        _playerHealth = 100f;
    }
    private void Update()
    {
        _enemyCountText.text = "Enemy Killed: " + enemyKillCount;
        timeCount = Time.time;
        _timerText.text = "Time:" + timeCount;
        survivedTime = timeCount;
        HealthUpdate(_playerHealth);
    }
    void HealthUpdate(float Health)
    {
        _playerHealth = Health;
        _playerHealthText.text = "Health: " + _playerHealth;
        if (_playerHealth <= 0)
        {
            _playerHealthText.text = "Dead";
        }
    }
    private void EnemyKillCount()
    {
        enemyKillCount++;
    }
    void result()
    {
        _enemyCountText.enabled = false;
        _timerText.enabled = false;
        _survivedTimeText.enabled = true;
        _survivedTimeText.text = "Survived Time: " + survivedTime;
    }
}
