using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    [SerializeField]
    protected GameObject objectToPool;
    [SerializeField]
    protected int poolSize = 10;
    protected Queue<GameObject> objectPool;
    public Transform objectParent;

    private void Awake() {
        objectPool = new Queue<GameObject>();
    }

    public void InitPools(GameObject objectToPool, int poolSize = 10) {
        this.objectToPool = objectToPool;
        this.poolSize = poolSize;
    }

    public GameObject CreateObject() {
        CreateParentIfNeeded();
        GameObject spawnedObject = null;

        if (objectPool.Count < poolSize) {
            spawnedObject = Instantiate(objectToPool, transform.position, Quaternion.identity);
            spawnedObject.transform.SetParent(objectParent);
        } else {
            spawnedObject = objectPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }

        objectPool.Enqueue(spawnedObject);
        return spawnedObject;
    }

    public void CreateParentIfNeeded() {
        if (objectParent == null) {
            string name = "ObjectPool_" + objectToPool.name;
            var parentObject = GameObject.Find(name);
            if (parentObject != null) {
                objectParent = parentObject.transform;
            } else {
                objectParent = new GameObject(name).transform;
            }
        }
    }
}
