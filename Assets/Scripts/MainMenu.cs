using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        LevelChanger.instance.FadeToLevel(4);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
