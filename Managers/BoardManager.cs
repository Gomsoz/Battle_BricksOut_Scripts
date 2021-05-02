using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager
{
    BoardSetting boardSetting = new BoardSetting();

    private int numOfItemSprites;

    public void Init()
    {
        boardSetting.Init();
        BoardSetting();
    }

    public void BoardSetting()
    {
        boardSetting.SetBoard();
    }
}
