using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float movespeed = 5;
    [SerializeField] private float firerate = 0.2f;
    [SerializeField] private ObjectPool bulletPool;
    private Vector2 currentMoveInput;
    private float shootTimer = 0f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
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
    }

    private void Shoot()
    {
        var spawnPos = this.transform.position + transform.up * 0.5f;
        GameObject bullet = bulletPool.GetFromPool();
        bullet.transform.position = spawnPos;
        bullet.GetComponent<Bullet>().Initiate(bulletPool);
    }
}