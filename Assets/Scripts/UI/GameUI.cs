using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : Page
{
    void Start()
    {
        pauseGame = false;
        PageManager.Instance.OpenPage(this);
    }
}
