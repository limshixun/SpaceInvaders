using UnityEngine;
using System.Collections.Generic;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float damage = 1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ObjectPool pool;
    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    public void Initiate(ObjectPool poolRef)
    {
        force = Random.Range(4, 10);
        rb.linearVelocity = -transform.up * force;
        pool = poolRef;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            OnBecameInvisible();
        }
    }

    void OnBecameInvisible()
    {
        rb.linearVelocity = Vector2.zero;
        pool.ReturnToPool(gameObject);
    }
}
