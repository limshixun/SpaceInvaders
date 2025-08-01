using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float movespeed;
    [SerializeField] private GameObject bulletPrefab;
    private Vector2 currentMoveInput;

    void Awake()
    {
        movespeed = 5;
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
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + currentMoveInput * movespeed * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        var spawnPos = this.transform.position + transform.up * 0.5f;
        var bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        Bullet script = bullet.GetComponent<Bullet>();
        script.Travel();
    }
}