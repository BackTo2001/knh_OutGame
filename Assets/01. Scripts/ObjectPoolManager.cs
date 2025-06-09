using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    [System.Serializable]
    public class Pool
    {
        public EPoolType poolType;
        public GameObject prefab;
        public int initialSize = 10;
    }

    public List<Pool> pools;
    private Dictionary<EPoolType, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        Instance = this;
        poolDictionary = new Dictionary<EPoolType, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.initialSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }

            poolDictionary.Add(pool.poolType, objectQueue);
        }
    }

    public GameObject Spawn(EPoolType type, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogWarning($"PoolType {type} not found!");
            return null;
        }

        GameObject obj = poolDictionary[type].Count > 0 ? poolDictionary[type].Dequeue() : null;

        if (obj == null)
        {
            Pool poolConfig = pools.Find(p => p.poolType == type);
            obj = Instantiate(poolConfig.prefab);
        }

        obj.transform.SetParent(transform);
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        return obj;
    }


    public void ReturnToPool(EPoolType type, GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);

        if (poolDictionary.ContainsKey(type))
        {
            poolDictionary[type].Enqueue(obj);
        }
        else
        {
            Destroy(obj);
        }
    }

}
