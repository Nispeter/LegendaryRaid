using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : Screen
{
    void Start()
    {
        openOnTop  = true;
        InputManager.OnPause += HandlePause;
    }

    private void HandlePause(Vector2 input)
    {
        //Pause Ui needs to appear only if game screen is active 
        if (ScreenManager.Instance.screenStack.Peek().GetType() == typeof(GameUI)) 
            ScreenManager.Instance.OpenScreen(this);
        else
        {
            ScreenManager.Instance.GoBack();
        }
    }
}
