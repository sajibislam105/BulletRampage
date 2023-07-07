using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour
{
    public Action<Transform> EnemyGenerateCountAction;
    public enum SpawnState
    {
        SPAWNING,
        WATING,
        COUNTING
    };
    
    [SerializeField] private WaveData _waveData;
    private int nextWave = 0;
    
    [SerializeField] private float timeBetweenWaves = 2f;
    public float waveCountDown;
    private float searchCountDown = 1f;

    [SerializeField] private SpawnState state = SpawnState.COUNTING;
    [SerializeField]private Transform[] spawnPoints;

    //enemy navmesh related
    [SerializeField] private GameObject groundObject;
    [SerializeField] private NavMeshSurface _groundNavMeshSurface;
    [SerializeField] private Player _player;
    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _groundNavMeshSurface = groundObject.GetComponent<NavMeshSurface>();
        // Check if the NavMeshSurface component is attached to the groundObject
        if (_groundNavMeshSurface == null)
        {
            Debug.LogError("NavMeshSurface component not found on the Ground Object.");
        }
        
        waveCountDown = timeBetweenWaves;
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No Spawn point referenced");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.WATING)
        {
            if (!EnemyIsAlive())
            {
                //Begin a new round
               WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if (waveCountDown <= 0)
        {
            if (state!= SpawnState.SPAWNING)
            {
                // start spawning wave
                StartCoroutine(SpawnWave(_waveData.Waves[nextWave])); //accessed the array class of S.O. using the reference
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        //Debug.Log("Wave Completed");
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;
        if (nextWave + 1 > _waveData.Waves.Length - 1 )
        {
            //nextWave = 0;
            _player.PlayerDeadAction?.Invoke();
            Debug.Log("All Waves complete! Lopping...");
        }
        else
        {
            nextWave++;   
        }
    }
    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator SpawnWave(WaveData.Wave _waves)
    {
        Debug.Log("Spawning Wave: "+ _waves.WaveName);
        state = SpawnState.SPAWNING;
        //spawn
        for (int i = 0; i < _waves.count; i++)
        {
            SpawnEnemy(_waves.enemy);
            yield return new WaitForSeconds(1f/_waves.SpawnRate);
        }
        state = SpawnState.WATING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Debug.Log("Spawn Enemy: " + _enemy.name);
        Transform _sp = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        Transform enemyClone = Instantiate(_enemy, _sp.position, _sp.rotation);
        enemyClone.gameObject.name = "EnemyClone";

        _navMeshAgent = enemyClone.GetComponent<NavMeshAgent>();
        _groundNavMeshSurface.AddData();
        _navMeshAgent.SetDestination(_player.transform.position);
        EnemyGenerateCountAction?.Invoke(enemyClone); // adding to the list
    }
}
