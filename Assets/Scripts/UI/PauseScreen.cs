using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : Page
{
    void Start()
    {
        openOnTop  = true;
        InputManager.OnPause += HandlePause;
    }

    private void HandlePause(Vector2 input)
    {
        //Pause Ui needs to appear only if game screen is active 
        if (PageManager.Instance.pageStack.Peek().GetType() == typeof(GameUI)) 
            PageManager.Instance.OpenPage(this);
        else
        {
            PageManager.Instance.GoBack();
        }
    }
}
