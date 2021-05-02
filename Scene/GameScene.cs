using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    private void Start()
    {
        GameManager.GameMgr.CheckEnemyLevel();
    }
    protected override void Init()
    {
        base.Init();
        sceneType = Defines.SceneType.Game;
    }

    public override void Clear()
    {
    }

}
