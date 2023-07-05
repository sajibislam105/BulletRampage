using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]private EnemyData _enemyData;

   // [SerializeField]private NavMeshAgent _navMeshAgent;
   // [SerializeField]public NavMeshSurface _navMeshSurface;
   // [SerializeField]private GameObject _destinationPosition;
   // private Vector3 _destination;

    private string EnemyName;
    private string EnemyType;
    private float enemyHealth;
    private float enemyDamagePower;
    private float enemyMovementSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
       // _navMeshSurface = GetComponent<NavMeshSurface>();
        
        EnemyName = _enemyData.enemyName;
        EnemyType = _enemyData.enemyType;
        enemyHealth = _enemyData.enemyHealth;
        enemyDamagePower = _enemyData.enemyDamagePower;
        enemyMovementSpeed = _enemyData.enemyMovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
       // _destination = _destinationPosition.transform.position;
       // _navMeshAgent.SetDestination(_destination);
    }
}
