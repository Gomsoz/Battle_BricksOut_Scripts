using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager gameMgr;

    public Defines.EnemyLevel enemyLevel;
    public Action<Defines.EnemyLevel> ChangeEnemyLevel = null;

    public static GameManager GameMgr
    {
        get
        {
            if(gameMgr == null)
            {
                return null;
            }
            return gameMgr;
        }
    }

    private void Awake()
    {
        if(gameMgr == null)
        {
            gameMgr = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void CheckEnemyLevel()
    {
        ChangeEnemyLevel.Invoke(enemyLevel);
    }
}
