using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static Action EnemyKillCountAction;

    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private GameObject enemyBulletContainer;
    [SerializeField] private GameObject Bullet;
    
    [SerializeField]private EnemyData _enemyData;
    private string EnemyName;
    private string EnemyType;
    [SerializeField] private float enemyHealth;
    private float enemyDamagePower;
    private float enemyMovementSpeed;

    private float timeBtwShots;
    private float startTimeBtwShots = 1f;
    private Vector3 _playerDirection;

    private Vector3 _playerDestination;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        EnemyName = _enemyData.enemyName;
        EnemyType = _enemyData.enemyType;
        enemyHealth = _enemyData.enemyHealth;
        enemyDamagePower = _enemyData.enemyDamagePower;
        enemyMovementSpeed = _enemyData.enemyMovementSpeed;
        
        timeBtwShots = startTimeBtwShots;
        _playerDestination = GameObject.FindGameObjectWithTag("Player").transform.position;
        _playerDirection = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
    }

    private void Update()
    {
        EnemyFire();
        _playerDestination = GameObject.FindGameObjectWithTag("Player").transform.position;
        _navMeshAgent.SetDestination(_playerDestination);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bullet")
        {
            Debug.Log("Hit by bullet");
            enemyHealth -= 50f;
            Destroy(other.gameObject);  //bullet
            if (enemyHealth <= 0)
            {
                EnemyKillCountAction?.Invoke();
                Destroy(gameObject); // Enemy
            }
        }
    }
    
    void EnemyFire()
    {
        
        if (timeBtwShots<- 0)
        {
            GameObject BulletClone = Instantiate(Bullet, enemyBulletContainer.transform.position, enemyBulletContainer.transform.rotation);
            BulletClone.transform.parent = enemyBulletContainer.transform;
            _playerDirection = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
            BulletClone.GetComponent<Rigidbody>().AddForce(_playerDirection * 5f, ForceMode.Impulse);
            BulletClone.name = "EnemyBullet ";
            BulletClone.tag = "EnemyBullet";
            Destroy(BulletClone,3f);
            timeBtwShots = startTimeBtwShots; 
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
