using UnityEngine;
using System.Collections.Generic;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float damage = 1f;
    [SerializeField] private Rigidbody2D rb;
    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    public void Initiate()
    {
        force = Random.Range(4, 10);
        // So that it can return to the poolref
        rb.linearVelocity = -transform.up * force;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
            OnBecameInvisible();
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
