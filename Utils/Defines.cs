using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defines
{
    public enum UIEvent
    {
        Click,
    }

    public enum EnemyLevel
    {
        Easy,
        Normal,
        Hard,
    }

    public enum SceneType
    {
        Unknown,
        Start,
        Game,
    }

    public enum SoundType
    {
        Bgm,
        Ball,
        Block,
        SoundCnt,
    }

    public enum SpeciesofItem
    {
        FasterMover,
        MoverExpand,
        FasterBallSpeed,
        Boom,
        PowerShot,
        Invincible,
        ItemCount,
    }

    public enum EnemyState
    {
        Stop,
        Move,
        Default,
    }
}
