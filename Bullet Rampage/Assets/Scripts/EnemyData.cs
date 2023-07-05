using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData",menuName = "ScriptableObject/EnemyData")]
public class EnemyData :ScriptableObject
{
    public string enemyName;
    public string enemyType;
    public float enemyHealth;
    public float enemyDamagePower;
    public float enemyMovementSpeed;

}
