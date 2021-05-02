using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager
{
    Dictionary<string, GameObject> _effects = new Dictionary<string, GameObject>();

    string _effectPath = $"Prefabs/Effect/";


    public void AddEffect(string effectName)
    {
        GameObject effectGo = Managers.Resources.Load<GameObject>($"{_effectPath}{effectName}Effect");
        _effects.Add(effectName, effectGo);
    }
    public GameObject SetItemEffect(GameObject gameObject, string itemName, GameObject effect)
    {
        return GameObject.Instantiate(_effects[$"Item{itemName}"],gameObject.transform);
    }

    public void ActiveEffect(GameObject _effectObject)
    {
        _effectObject.transform.parent = null;
        _effectObject.SetActive(true);
    }
}
