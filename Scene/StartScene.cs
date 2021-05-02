using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        sceneType = Defines.SceneType.Start;
    }

    public override void Clear()
    {
    }
}
