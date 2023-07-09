using UnityEngine;

public class PlayerPhysics : MonoBehaviour, ObjectControl
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform colliderParent;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private FlashHitEffect flash;

    [SerializeField] private bool ignoreEnemyCollision;

    private Vector2 lastVelocity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            return;
    }

    void FixedUpdate()
    {
        GetDirection();
        KeepSpeed();
    }

    void Start()
    {
        if (ignoreEnemyCollision) IgnoreEnemies();
        Launch();
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

    private void IgnoreEnemies()
    {
        Collider2D[] enemies = colliderParent.GetComponentsInChildren<Collider2D>(true);

        foreach (Collider2D col in enemies)
            Physics2D.IgnoreCollision(col, playerCollider);
    }

    private void KeepSpeed()
    {
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
    }

    public void StopObject()
    {
        rb.velocity = Vector3.zero;
    }

    public void PlayObject()
    {
        if (gameObject) Launch();
    }

    public void FlashEffect()
    {
        flash.Flash();
    }
}
