using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour2 : MonoBehaviour, ObjectControl
{
    [SerializeField] private EnemyType enemyType; public EnemyType enemy_type => enemyType;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject smoke;
    private Vector2 lastVelocity;
    private bool isDestroyed = false;

    void FixedUpdate()
    {
        GetDirection();
        KeepSpeed();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }

    private void GetDirection()
    {
        Vector2 playerVector = rb.velocity.normalized;
        if ((playerVector.x < 0 && transform.localScale.x > 0) || (playerVector.x > 0 && transform.localScale.x < 0))
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    private void KeepSpeed()
    {
        lastVelocity = rb.velocity;
    }

    public IEnumerator DestroyEnemy()
    {
        if (isDestroyed) yield break;

        isDestroyed = true;

        Instantiate(smoke, transform).transform.SetParent(transform.parent);

        GameManager.instance.enemySpawner.CheckNextWave();
        Destroy(gameObject);
    }

    public void StopObject()
    {
        rb.velocity = Vector3.zero;
    }

    public void PlayObject()
    {
        Launch();
    }
}

public enum EnemyType { Knight }
