using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [HideInInspector]
    public static ScreenManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    [HideInInspector]
    public Stack<Screen> screenStack = new Stack<Screen>();

    public void OpenScreen(Screen screen)
    {
        if (screenStack.Count > 0 && !screen.openOnTop)
        {
            screenStack.Peek().DeactivateScreen();
        }
        screenStack.Push(screen);
        screen.ActivateScreen();
    }

    public void GoBack()
    {
        if (screenStack.Count > 0)
        {
            screenStack.Pop().DeactivateScreen();
        }
        if (screenStack.Count > 0 && !screenStack.Peek().screen.activeSelf)
        {
            screenStack.Peek().ActivateScreen();
        }
    }

}
