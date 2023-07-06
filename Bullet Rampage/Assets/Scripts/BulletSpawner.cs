using System;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject OrignalBullet;
    [SerializeField] private GameObject BulletContainer;
    [SerializeField ]private Player _player;
    
    private float forceSize = 5f;

    private void OnEnable()
    {
        _player.BulletGenerateAction += SpawnBullet;
    }
    private void OnDisable()
    {
        _player.BulletGenerateAction -= SpawnBullet;
    }
    void SpawnBullet(float number, Vector3 forceDirection)
    {
        for (int i = 0; i < number; i++)
        {
            //GameObject BulletClone = Instantiate(OrignalBullet, OrignalBullet.transform.position, OrignalBullet.transform.rotation);
            GameObject BulletClone = Instantiate(OrignalBullet, BulletContainer.transform.position, OrignalBullet.transform.rotation);
            BulletClone.transform.parent = BulletContainer.transform;
            BulletClone.name = "Bullet";

            BulletClone.GetComponent<Rigidbody>().AddForce(forceDirection * forceSize,ForceMode.Impulse);
            //BulletClone.GetComponent<Rigidbody>().velocity = forceDirection*forceSize;
            //Destroy(BulletClone,5f);
        }
    }
}
