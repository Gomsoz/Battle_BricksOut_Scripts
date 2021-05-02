using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UI_StartScene : UI_Base
{
    public enum StartSceneButtons
    {
        StartButton,
        ExitButton,
    }

    public enum LevelSlider 
    {
        SelectEnemyLevel,
    }

    private void Start()
    {
        Bind<Button>(typeof(StartSceneButtons));
        Bind<Slider>(typeof(LevelSlider));

        GameObject _startButton = GetButton((int)StartSceneButtons.StartButton).gameObject;
        GameObject _exitButton = GetButton((int)StartSceneButtons.ExitButton).gameObject;

        AddUIHandler(_startButton, ClickStartButton);
        AddUIHandler(_exitButton, ClickExitButton);
    }

    public void ClickStartButton(PointerEventData evt)
    {
        Managers.Scene.LoadScene(Defines.SceneType.Game);
    }

    public void ClickExitButton(PointerEventData evt)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    Button GetButton(int idx)
    {
        return Get<Button>(idx);
    }
}
