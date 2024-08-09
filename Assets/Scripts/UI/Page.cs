using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    [Header("Screen Elements")]
    public GameObject page;
    public bool pauseGame = true;
    public bool openOnTop = false;

    public virtual void ActivatePage()
    {
        page.SetActive(true);
        if(pauseGame)
            SceneManager.Instance.ChangeGameState(GameState.Pause);
    }

    public virtual void DeactivatePage()
    {
        page.SetActive(false);
        SceneManager.Instance.ChangeGameState(GameState.Playing);
    }

    public virtual void ActiveSelf(){
        PageManager.Instance.OpenPage(this);
    }
}
