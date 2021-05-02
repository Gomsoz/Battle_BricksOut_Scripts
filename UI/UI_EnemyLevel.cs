using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyLevel : UI_Base
{
    Slider _enemyLevelSlider;
    public enum Sliders
    {
        EnemyLevelSlider
    }

    private void Start()
    {
        Bind<Slider>(typeof(Sliders));
        _enemyLevelSlider = GetSlider((int)Sliders.EnemyLevelSlider);
        _enemyLevelSlider.onValueChanged.AddListener(delegate { CheckValueChange(); });
    }

    public void CheckValueChange()
    {
        Debug.Log(_enemyLevelSlider.value);
        if (_enemyLevelSlider.value < 0.3f)
        {
            _enemyLevelSlider.value = 0f;
            GameManager.GameMgr.enemyLevel = Defines.EnemyLevel.Easy;
        }       
        else if (_enemyLevelSlider.value < 0.6f)
        {
            _enemyLevelSlider.value = 0.5f;
            GameManager.GameMgr.enemyLevel = Defines.EnemyLevel.Normal;
        }
        else
        {
            _enemyLevelSlider.value = 1f;
            GameManager.GameMgr.enemyLevel = Defines.EnemyLevel.Hard;
        }
            
    }

    Slider GetSlider(int idx)
    {
        return Get<Slider>(idx);
    }
}
