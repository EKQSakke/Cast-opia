using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float health;
    float maxHealth;
    Animator anim;


    [SerializeField] float mortarTimer;
    float maxMortarTimer;
    [SerializeField] float enemyTimer;
    float maxEnemyTimer;
    [SerializeField] GameObject mortarPrefab;
    [SerializeField] Transform[] mortarSpawnLocations;
    [SerializeField] Transform[] enemySpawnLocations;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Image hpFill;
    [SerializeField] AudioClip[] clips;
    AudioSource audioSource;

    string[] attackAnims = 
        new string[] { "sweep", "sweepMirror", "smash",
            "smashMirror", "mortarEnter", "mortarExit"};

    // Start is called before the first frame update
    void Start()
    {
        maxEnemyTimer = enemyTimer;
        if (GameManager.instance.levelCounter == 1)
        {
            health = 1000;
        }
        maxHealth = health;
        maxMortarTimer = mortarTimer;
        mortarTimer = -1;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        hpFill.fillAmount = health / maxHealth;
        enemyTimer -= Time.deltaTime;
        if (enemyTimer < 0)
        {
            SpawnEnemy();
        }


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("bossIdle"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(clips[0]);
            }
            Attack();
        }
        else if(anim.GetCurrentAnimatorStateInfo(0).IsName("mortarIdle"))
        {
            SpawnMortar();
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("mortarExit"))
        {
            mortarTimer = -1;
        }

    }

    private void SpawnEnemy()
    {
        enemyTimer = maxEnemyTimer;

        float spawn0 = Vector3.Distance(GameManager.instance.player.transform.position, enemySpawnLocations[0].position);
        float spawn1 = Vector3.Distance(GameManager.instance.player.transform.position, enemySpawnLocations[1].position);
        GameObject enemy = Instantiate(enemyPrefab);
        if (spawn0 > spawn1)
        {
            enemy.transform.position = enemySpawnLocations[0].position;
        }
        else
        {
            enemy.transform.position = enemySpawnLocations[1].position;
        }
    }

    private void SpawnMortar()
    {
        mortarTimer -= Time.deltaTime;
        if (mortarTimer < 0)
        {
            int rnd = (int)UnityEngine.Random.Range(0, mortarSpawnLocations.Length);
            GameObject mortar = Instantiate(mortarPrefab);
            mortar.transform.position = mortarSpawnLocations[rnd].position;
            mortarTimer = maxMortarTimer;
        }
    }

    void Attack()
    {
        int rnd = (int)UnityEngine.Random.Range(0,5);
        if (health < 1)
        {
            audioSource.PlayOneShot(clips[1]);
            rnd = 5;
        }
        anim.SetInteger("attack", rnd);
    }

    void EndGame()
    {
        LevelChanger.instance.FadeToLevel(5);
        CanvasController.instance.gameObject.SetActive(false);
    }
}
