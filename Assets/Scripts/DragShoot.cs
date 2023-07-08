using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragShoot : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrajectoryRenderer tr;

    private Vector3 clickedPoint, releasePoint, movDir;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy"))
        {
            movDir = Vector2.Reflect(movDir, other.contacts[0].normal).normalized;
            rb.velocity = movDir * speed * 0.5f;
            Debug.Log($"{movDir}, velocity : {rb.velocity}");
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 0;
            rb.velocity = Vector3.zero;

            clickedPoint = transform.localPosition;
        }

        if (Input.GetMouseButton(0))
        {
            Time.timeScale = 0;
            rb.velocity = Vector3.zero;

            releasePoint = cam.ScreenToWorldPoint(Input.mousePosition);
            tr.RenderLine(transform.localPosition, releasePoint);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            Time.timeScale = 1;
            releasePoint = cam.ScreenToWorldPoint(Input.mousePosition);
            tr.EndLine();
            MoveBullet();
        }
    }

    private void MoveBullet()
    {
        movDir = (transform.localPosition - releasePoint).normalized;
        rb.velocity = movDir * speed;
        Debug.Log($"==========={movDir}, {rb.velocity}");
    }
}
