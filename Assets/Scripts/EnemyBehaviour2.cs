using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour2 : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 lastVelocity;

    void FixedUpdate()
    {
        GetDirection();
        KeepSpeed();
    }

    void Start()
    {
        Launch();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);

        if (collision.gameObject.CompareTag("Bullet")) {
            collision.gameObject.GetComponent<DragShoot>().PlayAngryAnimation();
            StartCoroutine(DestroyEnemy());
        }
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

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSecondsRealtime(0.05f);
        Destroy(gameObject);
    }
}
