using UnityEngine;
using System.Collections.Generic;
using System;

public class ItemManager
{
    public Action<GameObject> OnPlayerGetItem = null;
    public Action<Defines.SpeciesofItem> OnItemUsed = null;

    public Dictionary<Defines.SpeciesofItem , bool> _isItemUsed = new Dictionary<Defines.SpeciesofItem, bool>();
    Defines.SpeciesofItem _EitemNames = 0;

    GameObject _itemGo;

    Sprite[] _itemSprites = new Sprite[(int)Defines.SpeciesofItem.ItemCount];
    private string[] _itemNames = null;
    public string[] ItemNames { get { return _itemNames; } }

    string _itemGoPath = $"Prefabs/Item";
    string _ItemSpritePath = $"ItemImage/";

    public void Init()
    {
        _itemNames = System.Enum.GetNames(typeof(Defines.SpeciesofItem));

        for (int i = 0; i < (int)Defines.SpeciesofItem.ItemCount; i++)
        {
            _itemSprites[i] = Managers.Resources.Load<Sprite>($"{_ItemSpritePath}{_itemNames[i]}");
            _isItemUsed.Add(_EitemNames++ , false);
            Managers.Effect.AddEffect($"Item{ItemNames[i]}");
        }

        _itemGo = Managers.Resources.Instantiate($"{_itemGoPath}");
    }

    public bool CheckItemUsed(Defines.SpeciesofItem itemname)
    {
        if (_isItemUsed[itemname])
            return true;
        return false;
    }

    public void ChangeItemUsed(Defines.SpeciesofItem itemName)
    {
        _isItemUsed[itemName] = !_isItemUsed[itemName];
    } 

    public GameObject CreateItem(Transform parent)
    {
        GameObject itemInstance = GameObject.Instantiate(_itemGo);
        itemInstance.GetComponent<SpriteRenderer>().sprite = _itemSprites[UnityEngine.Random.Range(0, 3/*_itemSprites.Length - 1*/)];
        itemInstance.transform.parent = parent.transform;
        itemInstance.transform.position = parent.transform.position;
        return itemInstance;
    }
}