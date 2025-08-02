using UnityEngine;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private Rigidbody2D rb;
    private ObjectPool bulletPool;

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    public void Initiate(ObjectPool poolRef)
    {
        // So that it can return to the poolref
        bulletPool = poolRef;
        rb.linearVelocity = transform.up * force;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(collision + " Take Damage = " + damage);
            ReturnToPool();
        }
    }

    void OnBecameInvisible()
    {
        ReturnToPool();
    }

    void ReturnToPool()
    {
        rb.linearVelocity = Vector2.zero;
        bulletPool.ReturnToPool(gameObject);
    }
}
