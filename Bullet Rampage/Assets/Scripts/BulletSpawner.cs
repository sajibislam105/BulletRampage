using System;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject OrignalBullet;
    [SerializeField] private GameObject BulletContainer;
    [SerializeField ]private Player _player;
    
    private float forceSize = 30f;

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
            var instantiatePosition = _player.transform.position;
            instantiatePosition.y += 1f;
            GameObject BulletClone = Instantiate(OrignalBullet, instantiatePosition, OrignalBullet.transform.rotation);
            BulletClone.transform.parent = BulletContainer.transform;
            BulletClone.name = "Bullet";
            BulletClone.GetComponent<Rigidbody>().AddForce(forceDirection * forceSize,ForceMode.Impulse);
            BulletClone.GetComponent<AudioSource>().Play();
            if (_player.PlayerHealth <= 0)
            {
                _player.BulletGenerateAction -= SpawnBullet;
            }
            Destroy(BulletClone,10f);
        }
    }
}
