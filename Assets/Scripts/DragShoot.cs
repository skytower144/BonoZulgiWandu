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
    private bool isHolding = false;

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
            RotateSprite(movDir);
        }
    }
    void Update()
    {
        RotateBullet();

        if (Input.GetMouseButtonDown(0))
        {
            PressMouse();
        }

        if (Input.GetMouseButton(0))
        {
            PressMouse();
            tr.RenderLine(transform.localPosition, cam.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ReleaseMouse();
            tr.EndLine();
            MoveBullet();
        }
    }

    private void PressMouse()
    {
        Time.timeScale = 0;
        rb.velocity = Vector3.zero;
        clickedPoint = transform.localPosition;
        isHolding = true;
    }

    private void ReleaseMouse()
    {
        Time.timeScale = 1;
        releasePoint = cam.ScreenToWorldPoint(Input.mousePosition);
        isHolding = false;
    }

    private void MoveBullet()
    {
        movDir = (transform.localPosition - releasePoint).normalized;
        rb.velocity = movDir * speed;
    }

    private void RotateBullet()
    {
        if (!isHolding) return;
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector3 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.Rotate(0, 0, 90);
    }

    private void RotateSprite(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        while (angle < 0f)
        {
            angle += 360f;
        }
        angle %= 360;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
