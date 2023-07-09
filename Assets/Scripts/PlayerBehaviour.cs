using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private GameObject cacheBullet = null;

    void Start()
    {
        //InvokeRepeating("SpawnBullet", 0.5f, 1f);
    }

    void Update()
    {
        Debug_SpawnBullet();
    }

    private void SpawnBullet()
    {
        if (IsBulletAlive()) return;

        cacheBullet = Instantiate(bullet, transform.parent);
    }

    private bool IsBulletAlive()
    {
        return (cacheBullet != null);
    }

    private void Debug_SpawnBullet()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnBullet();
    }
}
