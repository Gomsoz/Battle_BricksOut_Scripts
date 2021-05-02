using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UsingItem : MonoBehaviour
{
    public static Action<Defines.SpeciesofItem> UsingItemListnerList = null;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        Managers.Items.OnItemUsed += UsingItemFunc;        
    }

    void AddInteractionObject(string objectName, string path = null)
    {
        GameObject findGo = GameObject.Find($"{objectName}");

        if(findGo == null)
        {
            GameObject findResourcesGo = Managers.Resources.Load<GameObject>($"{path}{objectName}");

            if(findResourcesGo == null)
            {
                Debug.Log($"Fail to find {objectName}");
                return;
            }          
        }      
    }

    public void UsingItemFunc(Defines.SpeciesofItem itemName)
    {
        Invoke(itemName);
    }

    void Invoke(Defines.SpeciesofItem e_ItemName)
    {
        string s_ItemName = Enum.GetName(typeof(Defines.SpeciesofItem), (int)e_ItemName);
        Managers.Sound.Play(s_ItemName, 1f, e_ItemName);
        UsingItemListnerList.Invoke(e_ItemName);
    }
    

    IEnumerator PowerShot(Defines.SpeciesofItem itemName)
    {
        Managers.Items.ChangeItemUsed(itemName);
        yield return new WaitForSeconds(3);
    }
    IEnumerator Invincible(Defines.SpeciesofItem itemName)
    {
        Managers.Items.ChangeItemUsed(itemName);
        yield return new WaitForSeconds(3);
        Managers.Items.ChangeItemUsed(itemName);
    }

}
