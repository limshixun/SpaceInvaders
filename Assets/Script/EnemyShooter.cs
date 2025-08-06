using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private float shootTimer;

    void Awake()
    {
        resetTimer();
    }
    void Start()
    {
        
    }

    private void resetTimer()
    {
        shootTimer = Random.Range(1, 15);
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
        GameObject bullet = ObjectPool.instance.GetFromPool(false);
        bullet.GetComponent<EnemyBullet>().Initiate(ObjectPool.instance);
        bullet.transform.position = this.transform.position;
    }
}
