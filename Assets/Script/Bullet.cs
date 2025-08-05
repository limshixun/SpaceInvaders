using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private Rigidbody2D rb;
    private ObjectPool bulletPool;
    private Camera cam;

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (cam == null) cam = Camera.main;
    }

    public void Initiate(ObjectPool poolRef)
    {
        // So that it can return to the poolref
        bulletPool = poolRef;
        rb.linearVelocity = transform.up * force;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            ReturnToPool();
            Debug.Log("triggered " + other);
        }
    }

    void ReturnToPool()
    {
        rb.linearVelocity = Vector2.zero;
        Debug.Log("asd");
        bulletPool.ReturnToPool(gameObject);
    }

    void Update()
    {
        if (cam.WorldToViewportPoint(transform.position).y > 1)
        {
            ReturnToPool();
        }
    }
}
