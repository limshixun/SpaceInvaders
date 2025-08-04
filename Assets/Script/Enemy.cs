using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;

    void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }
}
