using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : UI_Base
{
    int score = 0;
    enum Buttons
    {
        ScoreButton
    }

    enum Texts
    {
        ScoreText
    }

    private void Start()
    {
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GameObject go = GetButton((int)Buttons.ScoreButton).gameObject;
        AddUIHandler(go, ClickButton);
    }

    void ClickButton(PointerEventData evt)
    {
        GetText((int)Texts.ScoreText).text = score.ToString();
        score++;
    }

    Text GetText(int idx)
    {
        return Get<Text>(idx);
    }

    Button GetButton(int idx)
    {
        return Get<Button>(idx);
    }

    Image GetImage(int idx)
    {
        return Get<Image>(idx);
    }
}
