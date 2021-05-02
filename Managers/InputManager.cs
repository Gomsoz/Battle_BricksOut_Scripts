using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager
{
    public Action KeyboardInput = null;
    
    public void InputUpdate()
    {
        if (KeyboardInput != null && Input.anyKeyDown)
        {
            KeyboardInput.Invoke();
        }
    }
}
