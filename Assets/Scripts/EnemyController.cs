using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private float rotationSpeed = 5f;
    private float moveSpeed = 1.5f;
    private float distance;
    [SerializeField] private Transform spawnProjectile;
    [SerializeField] private GameObject prefabProjectile;
    private float rotationModifier = 1f;
    private float shootSpeed = 1.25f;
    private float bulletDelay;
    private Vector3 vectorMoveDirection, vectorAimDirection;
    //new events
    public UnityEvent scoreEvent;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scoreEvent.AddListener(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().IncrementScore);
        scoreEvent.AddListener(GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>().UpdateScore);
    }
    void Update()
    {
        if (player != null)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            SpawnBullet();
            vectorAimDirection = player.transform.position - transform.position;
            vectorMoveDirection = transform.position - player.transform.position;
            vectorMoveDirection.Normalize();
            float angle = Mathf.Atan2(vectorAimDirection.y, vectorAimDirection.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
            transform.Translate(moveSpeed * Time.deltaTime * vectorMoveDirection);
        }
    }
    private void SpawnBullet(float timeCount = 1.7f)
    {
        bulletDelay += Time.deltaTime;
        if (bulletDelay >= timeCount) 
        {
            vectorAimDirection = player.transform.position - transform.position;
            GameObject bullet = Instantiate(prefabProjectile, spawnProjectile.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = shootSpeed * vectorAimDirection;
            Destroy(bullet, 1.5f);
            bulletDelay = 0f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            scoreEvent.Invoke();
            Destroy(gameObject);
        }
    }
}
