using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public static LevelChanger instance;
    Animator anim;
    int levelToLoad;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;

        // to boss
        if (GameManager.instance.levelCounter == 1)
        {
            levelToLoad = 4;
        }
        // to outro
        if (GameManager.instance.levelCounter == 2)
        {
            CanvasController.instance.gameObject.SetActive(false);
            levelToLoad = 6;
        }
        if (GameManager.instance.levelCounter == 3)
        {
            levelToLoad = 1;
        }

        anim.SetTrigger("FadeOut");
    }

    public void Spherelevel(int level)
    {
        levelToLoad = level;
        anim.SetTrigger("FadeOut");
    }

    public void OnDeath()
    {
        CanvasController.instance.Golden();
    }

    public void OnFadeStart()
    {
        GameManager.instance.levelCounter++;
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
