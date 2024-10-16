using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 0.025f;
    private float shootSpeed = 20f;
    Vector3 mousePosition;
    Rigidbody2D rb;
    Vector2 position;
    public Vector2 turn;
    [SerializeField] private Transform spawnProjectile;
    [SerializeField] private GameObject prefabProjectile;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition =  Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.right = direction;
        position = Vector2.Lerp(transform.position, mousePosition, speed);
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(prefabProjectile, spawnProjectile.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = shootSpeed * direction;
            Destroy(bullet, 1.5f);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Time.timeScale = 0f;
        }
    }
}
