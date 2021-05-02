using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject go = Load<GameObject>(path);

        if(go == null)
        {
            Debug.Log($"Loaded Prefabs are Error : {path}");
            return null;
        }

        return GameObject.Instantiate(go, parent);
    }
}
