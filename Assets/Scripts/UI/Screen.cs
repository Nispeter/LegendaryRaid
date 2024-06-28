using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    [Header("Screen Elements")]
    public GameObject screen;
    public bool pauseGame = true;
    public bool openOnTop = false;

    public virtual void ActivateScreen()
    {
        screen.SetActive(true);
        if(pauseGame)
            Time.timeScale = 0f;
    }

    public virtual void DeactivateScreen()
    {
        screen.SetActive(false);
        Time.timeScale = 1f;
    }

    public virtual void ActiveSelf(){
        ScreenManager.Instance.OpenScreen(this);
    }
}
