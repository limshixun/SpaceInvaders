using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private float shootTimer;

    void Awake()
    {
        resetTimer();
    }

    private void resetTimer()
    {
        shootTimer = Random.Range(3, 5);
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            Shoot();
            resetTimer();
        }
    }

    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().Initiate();
    }
}
