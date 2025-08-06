using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private Animator animator;
    [SerializeField] private Health _health;
    [SerializeField] private AudioSource explosionAudio;
    private Vector2 currentMoveInput;
    private float shootTimer;
    public float movespeed;
    public float firerate;
    public Vector2 InitialPos;
    public float dmgTimer;

    void Awake()
    {
        if (_health == null) _health = GetComponent<Health>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (explosionAudio == null) explosionAudio = GetComponent<AudioSource>();
        if (animator == null) animator = GetComponent<Animator>();

        InitialPos = transform.position;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        shootTimer = 0f;
        movespeed = 5;
        firerate = 0.2f;
        dmgTimer = 3f;

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
        dmgTimer = Mathf.Max(0, dmgTimer - Time.deltaTime);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + currentMoveInput * movespeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && dmgTimer <= 0)
        {
            animator.SetBool("Explosion", true);
            UserInput.instance.OnDisable();
            explosionAudio.Play();
            dmgTimer = 3;
        }
    }

    private void Shoot()
    {
        var spawnPos = this.transform.position + transform.up * 0.5f;
        GameObject bullet = bulletPool.GetFromPool(true);
        if (bullet)
        {
            bullet.transform.position = spawnPos;
            bullet.GetComponent<Bullet>().Initiate(bulletPool);
        }
    }

    public void Reset()
    {
        animator.SetBool("Explosion", false);
        transform.position = InitialPos;
        UserInput.instance.OnEnable();
        dmgTimer = 3f;
    }

    public void onExplosionEnd()
    {
        _health.dmgHealth(1);
        Reset();
    }


}