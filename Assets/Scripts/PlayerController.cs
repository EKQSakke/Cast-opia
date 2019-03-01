using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent playerAgent;
    [SerializeField] LayerMask movementMask;
    Camera cam;
    Vector3 destination;
    [SerializeField] GameObject spell;
    [SerializeField] float castOffset;
    Animator anim;
    [SerializeField] ParticleSystem healParticle;



    // Start is called before the first frame update
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ElementSwitch();
        Attack();
        AnimationSelect();
        HealthPotion();

        if (GameManager.instance.attackCd >= 0)
        {
            GameManager.instance.attackCd -= Time.deltaTime;
        }
    }

    private void HealthPotion()
    {
        if (Input.GetKeyDown(KeyCode.Q) && GameManager.instance.healthSpheres > 0)
        {
            gameObject.GetComponent<Player>().HealSound();
            healParticle.Play();
            GameManager.instance.playerHealth = GameManager.instance.maxPlayerHealth;
            GameManager.instance.healthSpheres--;
        }
    }

    void AnimationSelect()
    {
        if (playerAgent.hasPath)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    void ElementSwitch()
    {
        int element = -1;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(GameManager.instance.spellPoints[0] > 0)
             element = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (GameManager.instance.spellPoints[1] > 0)
                element = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (GameManager.instance.spellPoints[2] > 0)
                element = 3;
        }
        else
        {
            return;
        }

        GameManager.instance.PlayerElementSwitch(element);
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(1) && GameManager.instance.attackCd < 0)
        {
            playerAgent.destination = transform.position;
            GameManager.instance.attackCd = GameManager.instance.maxAttackCd;
            GameObject cast = Instantiate(spell);
            // Make a cast transform a part of the player object
            cast.transform.position = transform.position;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, movementMask))
            {
                cast.transform.LookAt(hit.point);
            }
            cast.transform.Translate(Vector3.forward * castOffset);
            cast.GetComponent<Spell>().element = (int)GameManager.instance.playerElement;

            if ((int)GameManager.instance.playerElement != 0)
                GameManager.instance.UseItem((int)GameManager.instance.playerElement);

            anim.Play("playerIdleAttack");

            GameManager.instance.playerElement = 0;
        }
    }

    void Movement()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, movementMask))
            {
                playerAgent.destination = hit.point;
            }
        }    
    }

    public void SaveDestination()
    {
        destination = playerAgent.destination;
        playerAgent.isStopped = true;
    }

    public void LoadDestination()
    {
        playerAgent.isStopped = false;
        playerAgent.destination = destination;
    }
}
