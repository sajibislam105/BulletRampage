using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData",menuName = "ScriptableObject/WaveData")]
public class WaveData : ScriptableObject
{
    [System.Serializable]
    public class Wave
    {
        public string WaveName;
        public Transform enemy;
        public int count;
        public float SpawnRate;        
    }
    public Wave[] Waves;
}
