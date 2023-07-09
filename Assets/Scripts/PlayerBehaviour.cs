using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject bullet, smoke;
    private GameObject cacheBullet = null;

    void Start()
    {
        InvokeRepeating("SpawnBullet", 1f, 1f);
    }

    void Update()
    {
        Debug_SpawnBullet();
    }

    private void SpawnBullet()
    {
        if (!CanSpawnBullet()) return;

        anim.Play("Player_Shoot", -1, 0f);
        cacheBullet = Instantiate(bullet, transform);
        cacheBullet.transform.SetParent(transform.parent);
        StartCoroutine(cacheBullet.GetComponent<DragShoot>().EnableDrag());

        Instantiate(smoke, transform);
    }

    private bool CanSpawnBullet()
    {
        return ((cacheBullet == null) && !GameManager.instance.enemySpawner.isWaveCompleted && GameManager.instance.enemySpawner.isBatchLoaded);
    }
    public bool IsBulletAlive()
    {
        return (cacheBullet != null);
    }

    private void ReturnWalk()
    {
        anim.Play("Player_Walk", -1, 0f);
    }

    private void Debug_SpawnBullet()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnBullet();
    }
}
