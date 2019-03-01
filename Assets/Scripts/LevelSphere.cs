using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSphere : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] GameObject[] levels;

    private void Start()
    {
        level = (int)UnityEngine.Random.Range(1,4);
        ActivateLevelGfx();
        
    }

    private void Update()
    {
        if (GameManager.instance.goldenSpheres > 9)
        {
            //boss
            level = 4;
            ActivateLevelGfx();
        }
    }

    private void ActivateLevelGfx()
    {
        switch (level)
        {
            case 1:
                levels[0].SetActive(true);
                levels[1].SetActive(false);
                levels[2].SetActive(false);
                levels[3].SetActive(false);
                break;

            case 2:
                levels[0].SetActive(false);
                levels[1].SetActive(true);
                levels[2].SetActive(false);
                levels[3].SetActive(false);
                break;

            case 3:
                levels[0].SetActive(false);
                levels[1].SetActive(false);
                levels[2].SetActive(true);
                levels[3].SetActive(false);
                break;

            case 4:
                levels[0].SetActive(false);
                levels[1].SetActive(false);
                levels[2].SetActive(false);
                levels[3].SetActive(true);
                break;

            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            LevelChanger.instance.Spherelevel(level);
        }
    }
}
