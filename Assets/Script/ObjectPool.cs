using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int InitialPoolSize;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        InitialPoolSize = 1;
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
        // if (pool.Count == 0)
        // {
        //     GameObject obj = Instantiate(prefab, transform);
        //     obj.SetActive(false);
        //     pool.Enqueue(obj);
        // }

        // GameObject pooledObj = pool.Dequeue();
        // pooledObj.SetActive(true);
        // return pooledObj;

        if (pool.Count > 0)
        {
            GameObject pooledObject = pool.Dequeue();
            Debug.Log(pool.Count);
            pooledObject.SetActive(true);
            return pooledObject;
        }
        else
        {
            return null;
        }
        
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
        Debug.Log("qwe");
    }
}
