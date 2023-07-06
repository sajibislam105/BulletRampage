using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Action EnemyKillCountAction;

    [SerializeField]private EnemyData _enemyData;
    private string EnemyName;
    private string EnemyType;
    [SerializeField] private float enemyHealth;
    private float enemyDamagePower;
    private float enemyMovementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        EnemyName = _enemyData.enemyName;
        EnemyType = _enemyData.enemyType;
        enemyHealth = _enemyData.enemyHealth;
        enemyDamagePower = _enemyData.enemyDamagePower;
        enemyMovementSpeed = _enemyData.enemyMovementSpeed;
    }
    // Update is called once per frame
    void Update()
    {
       
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
}
