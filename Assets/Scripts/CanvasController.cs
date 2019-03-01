using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public static CanvasController instance;

    public Image[] barImages;
    public Sprite activeBar;
    public Sprite standardBar;
    public GameObject crosshair;
    public Image crosshairFill;
    public Image healthFill;
    [SerializeField] TextMeshProUGUI[] barCounters;
    public GameObject golden;
    public TextMeshProUGUI goldenText;
    [SerializeField] Animation hitIndication;
    [SerializeField] GameObject pauseMenu;
    bool isPaused = false;


    //Singleton
    private void Awake()
    {
        Cursor.visible = false;

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
    }

    private void Update()
    {
        Crosshair();
        Health();
        SpellBarUpdate();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (sceneIndex == 1 || sceneIndex == 2 || sceneIndex == 3 || sceneIndex == 4)
            {
                if (isPaused)
                {
                    UnPause();
                }
                else
                {
                    Pause();
                }
            }
        }
    }


    public void Outro()
    {
        GameManager.instance.ToOutro();
        gameObject.SetActive(false);
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    void Health()
    {
        healthFill.fillAmount = GameManager.instance.playerHealth / GameManager.instance.maxPlayerHealth;
    }

    private void Crosshair()
    {
        crosshair.transform.position = Input.mousePosition;
        crosshairFill.fillAmount = 1 - GameManager.instance.attackCd / GameManager.instance.maxAttackCd;
    }

    public void Golden()
    {
        goldenText.text = GameManager.instance.goldenSpheres.ToString() + "/10";
        golden.SetActive(true);
        golden.GetComponent<Animation>().Play();
        StartCoroutine(NoGolden());
    }

    IEnumerator NoGolden()
    {
        yield return new WaitForSeconds(3);
        golden.SetActive(false);
    }

    public void HitFlash()
    {
        hitIndication.Play();
    }

    /// <summary>
    /// Run after switching spells
    /// </summary>
    public void SpellBarUpdate()
    {
        switch (GameManager.instance.playerElement)
        {
            case GameManager.Element.Physical:
                barImages[0].sprite = standardBar;
                barImages[0].transform.localScale = Vector3.one;
                barImages[1].sprite = standardBar;
                barImages[1].transform.localScale = Vector3.one;
                barImages[2].sprite = standardBar;
                barImages[2].transform.localScale = Vector3.one;
                break;
            case GameManager.Element.Fire:
                barImages[0].sprite = activeBar;
                barImages[0].transform.localScale = Vector3.one * 1.1f;
                barImages[1].sprite = standardBar;
                barImages[1].transform.localScale = Vector3.one;
                barImages[2].sprite = standardBar;
                barImages[2].transform.localScale = Vector3.one;
                break;
            case GameManager.Element.Water:
                barImages[0].sprite = standardBar;
                barImages[0].transform.localScale = Vector3.one;
                barImages[1].sprite = activeBar;
                barImages[1].transform.localScale = Vector3.one * 1.1f;
                barImages[2].sprite = standardBar;
                barImages[2].transform.localScale = Vector3.one;
                break;
            case GameManager.Element.Electric:
                barImages[0].sprite = standardBar;
                barImages[0].transform.localScale = Vector3.one;
                barImages[1].sprite = standardBar;
                barImages[1].transform.localScale = Vector3.one;
                barImages[2].sprite = activeBar;
                barImages[2].transform.localScale = Vector3.one * 1.1f;
                break;
            default:
                break;
        }

        barCounters[0].text = GameManager.instance.healthSpheres.ToString();
        barCounters[1].text = GameManager.instance.spellPoints[0].ToString();
        barCounters[2].text = GameManager.instance.spellPoints[1].ToString();
        barCounters[3].text = GameManager.instance.spellPoints[2].ToString();
    }
}
