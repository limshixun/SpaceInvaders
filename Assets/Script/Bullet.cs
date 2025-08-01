using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private Rigidbody2D rb;

    void Awake()
    {
        if (rb == null)
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(collision + " Take Damage = " + damage);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Travel()
    {
        rb.linearVelocity = transform.up * force;
    }

}
