using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    [HideInInspector]
    public static PageManager Instance { get; private set; }

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
    public Stack<Page> pageStack = new Stack<Page>();

    public void OpenPage(Page page)
    {
        if (pageStack.Count > 0 && !page.openOnTop)
        {
            pageStack.Peek().DeactivatePage();
        }
        pageStack.Push(page);
        page.ActivatePage();
    }

    public void GoBack()
    {
        if (pageStack.Count > 0)
        {
            pageStack.Pop().DeactivatePage();
        }
        if (pageStack.Count > 0 && !pageStack.Peek().page.activeSelf)
        {
            pageStack.Peek().ActivatePage();
        }
    }

}
