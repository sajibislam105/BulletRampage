using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Action<float,Vector3> BulletGenerateAction;

    [SerializeField] private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //fire
            BulletGenerateAction?.Invoke(1f,_player.bulletForceDirection);
            
        }
    }
}
