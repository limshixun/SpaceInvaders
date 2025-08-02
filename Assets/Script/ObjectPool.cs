using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int InitialPoolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

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
    }

    public GameObject GetFromPool()
    {
        if (pool.Count == 0)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }

        GameObject pooledObj = pool.Dequeue();
        pooledObj.SetActive(true);
        return pooledObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
