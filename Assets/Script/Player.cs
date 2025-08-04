using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ObjectPool bulletPool;
    private float _health;
    private Vector2 currentMoveInput;
    private float shootTimer;
    public float movespeed;
    public float firerate;
    public float acceleration;
    public float maxSpeed;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        // rb.interpolation = RigidbodyInterpolation2D.Interpolate;wa
        shootTimer = 0f;
        movespeed = 5;
        firerate = 0.2f;
        acceleration = 20f;
        maxSpeed = 20f;
        _health = 3;
    }

    void Update()
    {
        currentMoveInput = UserInput.instance.MoveInput;

        if (UserInput.instance.ShootJustPressed)
        {
            Shoot();
            shootTimer = firerate;
        }

        if (UserInput.instance.ShootIsHeld)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                Shoot();
                shootTimer = firerate;
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + currentMoveInput * movespeed * Time.fixedDeltaTime);
        // if (currentMoveInput != Vector2.zero)
        // {
        //     rb.AddForce(currentMoveInput * acceleration, ForceMode2D.Force);
        //     if (rb.linearVelocity.magnitude > maxSpeed)
        //     {
        //         rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        //     }
        // }
        // else if (currentMoveInput == Vector2.zero)
        // {
        //     // Debug.Log("no movement input");
        //     rb.AddForce(rb.linearVelocity * -acceleration*2, ForceMode2D.Force);
        // }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= 1;
        Debug.Log(_health);
    }

    private void Shoot()
    {
        var spawnPos = this.transform.position + transform.up * 0.5f;
        GameObject bullet = bulletPool.GetFromPool();
        bullet.transform.position = spawnPos;
        bullet.GetComponent<Bullet>().Initiate(bulletPool);
    }
}