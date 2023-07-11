using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private WaveSpawner _waveSpawnerState;
    [SerializeField] private Text _enemyCountText;
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _survivedTimeText;
    [SerializeField] private Text _playerHealthText;
    [SerializeField] private Text _enemyInText;
    [SerializeField] private Button _restartButton;
    
    private int enemyKillCount = 0;
    private float timeCount = 0f;
    private float _playerHealth;
    private float initial_time;

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
        initial_time = timeCount;
        _restartButton.interactable = false;
        _restartButton.gameObject.SetActive(false);
        _enemyCountText.text = "Enemy Killed: " + enemyKillCount;
        _timerText.text = "Time: 0";
        _survivedTimeText.enabled = false;
        _playerHealth = 100f;
        _enemyInText.text ="State: " + _waveSpawnerState.state;


    }
    private void Update()
    {
        _enemyCountText.text = "Enemy Killed: " + enemyKillCount;
        timeCount = Time.time;
        _timerText.text = "Time: " + timeCount;
        survivedTime = Time.time - initial_time;
        HealthUpdate(_playerHealth);
        _enemyInText.text = "State: " +  _waveSpawnerState.state;
    }
    void HealthUpdate(float Health)
    {
        _playerHealth = Health;
        _playerHealthText.text = "Health: " + _playerHealth;
        if (_playerHealth <= 0)
        {
            _playerHealthText.text = "Dead";
            _restartButton.interactable = true;
            _restartButton.gameObject.SetActive(true);
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
        _restartButton.interactable = true;
        _restartButton.gameObject.SetActive(true);
    }


    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
