using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.Multiplayer.Center.Common;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int InitialPoolSize;
    [SerializeField] private int EnemyInitialPoolSize;

    private Queue<GameObject> pool = new Queue<GameObject>();
    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    private List<GameObject> activeObjects = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < InitialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }

        for (int i = 0; i < EnemyInitialPoolSize; i++)
        {
            GameObject obj = Instantiate(enemyPrefab, transform);
            obj.SetActive(false);
            enemyPool.Enqueue(obj);
        }
    }

    public GameObject GetFromPool(bool player)
    {
        var selectedPool = player ? pool : enemyPool;
        if (selectedPool.Count > 0)
        {
            GameObject pooledObject = selectedPool.Dequeue();
            pooledObject.SetActive(true);
            activeObjects.Add(pooledObject);
            return pooledObject;
        }
        else
        {
            return null;
        }
    }

    public void ReturnToPool(GameObject obj)
    {
        var selectedPool = obj.CompareTag("Player") ? pool : enemyPool;
        obj.SetActive(false);
        activeObjects.Remove(obj);
        selectedPool.Enqueue(obj);
    }
    public void ReturnAllBullets()
    {
        // Create a copy to avoid modifying the list while iterating
        var activeCopy = new List<GameObject>(activeObjects);

        // Clear the actual list first so we're not modifying it during the loop
        activeObjects.Clear();

        foreach (var bullet in activeCopy)
        {
            if (bullet != null)
            {
                bullet.SetActive(false);
                if (bullet.CompareTag("Player"))
                {
                    if (!pool.Contains(bullet)) pool.Enqueue(bullet);
                }
                else
                {
                    if (!enemyPool.Contains(bullet)) enemyPool.Enqueue(bullet);
                }
            }
        }
    }


}
