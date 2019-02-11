using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour {

    public static Pooler sharedInstance;
    public GameObject objectPooler;
    [SerializeField] private int poolSize = 5;

    public List<GameObject> objects;

    void Awake()
    {
        sharedInstance = this;                                          // Pointer to itself for other gameobjects to access this instance
    }

    // Use this for initialization
    void Start () {
        objects = new List<GameObject> ();

        for (int i = 0; i < poolSize; i++)                              // Initialize list with the poolSize quantity of instances
        {
            GameObject obj = (GameObject)Instantiate(objectPooler);
            obj.SetActive(false);
            objects.Add(obj);
        }
	}
	
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].activeInHierarchy)                          // Find an inactive instance to return to caller
                return objects[i];
        }

        return null;                                                    // All instances are active so nothing can be returned
    }
}
