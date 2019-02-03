using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour {

    public static Pooler current;
    public GameObject objectPooler;
    [SerializeField] private int poolSize = 5;

    List<GameObject> objects;

    void Awake()
    {
        current = this;
    }

	// Use this for initialization
	void Start () {
        objects = new List<GameObject> ();

        for (int i = 0; i < poolSize; i++)
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
            if (!objects[i].activeInHierarchy)
                return objects[i];
        }
        GameObject obj = (GameObject)Instantiate(objectPooler);
        obj.SetActive(false);
        objects.Add(obj);
        return obj;
    }
}
