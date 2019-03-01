using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum Element
    {
        Physical,
        Fire,
        Water,
        Electric
    }

    public GameObject player;
    public float playerHealth;
    public float maxPlayerHealth;
    public int[] spellPoints;
    public int healthSpheres;
    public int goldenSpheres;
    public Element playerElement;


    public float attackCd;
    public float maxAttackCd;

    [Space(20)]
    //UI Elements
    public Color[] elementColors;
    public Material[] elementMaterials;
    public int levelCounter;
    //Singleton
    private void Awake()
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
        DontDestroyOnLoad(gameObject);

        maxAttackCd = attackCd;
    }
    // Start is called before the first frame update
    void Start()
    {
        maxPlayerHealth = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerHealth <= 0)
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            goldenSpheres = 0;

            healthSpheres = 3;
            spellPoints[0] = 3;
            spellPoints[1] = 3;
            spellPoints[2] = 3;

            if (sceneIndex == 6)
            {
                levelCounter = 3;
                return;
            }


            if (sceneIndex > 0 && sceneIndex < 4)
            {
                CanvasController.instance.Golden();
            }

            LevelChanger.instance.FadeToLevel(sceneIndex);
        }
    }

    public void ToOutro()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(5);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayerElementSwitch(int elementId)
    {
        if (playerElement == (Element)elementId)
        {
            playerElement = Element.Physical;
        }
        else
        {
            switch (elementId)
            {
                case 1:
                    playerElement = Element.Fire;
                    player.GetComponent<Player>().StaffPuff();
                    break;
                case 2:
                    playerElement = Element.Water;
                    player.GetComponent<Player>().StaffPuff();
                    break;
                case 3:
                    playerElement = Element.Electric;
                    player.GetComponent<Player>().StaffPuff();
                    break;
                default:
                    break;
            }
        }
    }

    public void LevelChange(int level)
    {
        LevelChanger.instance.FadeToLevel(level);
    }

    /// <summary>
    /// Gives +1 to the correct value according to item
    /// 0 = healthPotion
    /// 1 = Fire
    /// 2 = Water
    /// 3 = Electric
    /// 4 = Golden
    /// </summary>
    /// <param name="itemId"></param>
    public void Collect(int itemId)
    {
        switch (itemId)
        {
            case 0:
                healthSpheres++;
                break;

            case 1:
                spellPoints[0]++;
                break;
            case 2:
                spellPoints[1]++;
                break;
            case 3:
                spellPoints[2]++;
                break;
            case 4:
                goldenSpheres++;
                CanvasController.instance.Golden();
                break;

            default:
                break;
        }
    }

    public void UseItem(int itemId)
    {
        switch (itemId)
        {
            case 0:
                healthSpheres--;
                break;

            case 1:
                spellPoints[0]--;
                if (spellPoints[0] < 1)
                {
                    playerElement = Element.Physical;
                }
                break;
            case 2:
                spellPoints[1]--;
                if (spellPoints[1] < 1)
                {
                    playerElement = Element.Physical;
                }
                break;
            case 3:
                spellPoints[2]--;
                if (spellPoints[2] < 1)
                {
                    playerElement = Element.Physical;
                }
                break;

            default:
                break;
        }
    }
}
