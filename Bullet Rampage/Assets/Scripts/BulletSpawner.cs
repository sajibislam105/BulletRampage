using System;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject OrignalBullet;
    [SerializeField] private GameObject BulletContainer;

    [SerializeField ]private Gun _gun;
    [SerializeField ]private Player _player;
    private float forceSize = 5f;

    private void OnEnable()
    {
        _gun.BulletGenerateAction += SpawnBullet;
    }
    private void OnDisable()
    {
        _gun.BulletGenerateAction -= SpawnBullet;
    }
    void SpawnBullet(float number, Vector3 forceDirection)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject BulletClone = Instantiate(OrignalBullet, new Vector3(OrignalBullet.transform.position.x, OrignalBullet.transform.position.y, (OrignalBullet.transform.position.z)), OrignalBullet.transform.rotation);
            _gun = BulletClone.GetComponent<Gun>();
            BulletClone.transform.parent = BulletContainer.transform;
            BulletClone.name = "Bullet";
            //bulletSpeed
            //BulletClone.transform.position = _player.transform.position;
            BulletClone.GetComponent<Rigidbody>().AddForce(forceDirection * forceSize,ForceMode.Impulse);
            Destroy(BulletClone,5f);
        }
    }
}
