using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent enemyAgent;

    public float aggroRange;
    [SerializeField] float health;
    float maxHealth;
    [SerializeField] int element;
    [SerializeField] Renderer renderer;
    [SerializeField] EnemyHealthbar healthBar;
    [SerializeField] GameObject[] lootItems;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
        maxHealth = health;
        enemyAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        element = (int)UnityEngine.Random.Range(1, 4);
        renderer.material = GameManager.instance.elementMaterials[element];
    }

    // Update is called once per frame
    void Update()
    {
        HealthUpdate();
        if (enemyAgent.isOnNavMesh && Vector3.Distance(GameManager.instance.player.transform.position, transform.position) < aggroRange)
        {
            enemyAgent.SetDestination(GameManager.instance.player.transform.position);
        }else
        {
            NavMeshHit closestHit;

            if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas))
                gameObject.transform.position = closestHit.position;
            else
                Debug.LogError("Could not find position on NavMesh!");
        }
        if (health <= 0)
        {
            DropLoot();
            Destroy(gameObject);
        }

        enemyAgent.enabled = true;
    }

    private void DropLoot()
    {
        int rndLoot = UnityEngine.Random.Range(0, (int)lootItems.Length);
        GameObject loot = Instantiate(lootItems[rndLoot]);
        loot.transform.position = transform.position;
    }

    private void HealthUpdate()
    {
        if (health < maxHealth)
        {
            healthBar.healthBarFill.enabled = true;
            healthBar.healthBarBorder.enabled = true;

            healthBar.healthBarFill.fillAmount = health / maxHealth;
        }
        else
        {
            healthBar.healthBarFill.enabled = false;
            healthBar.healthBarBorder.enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spell"))
        {
            audioSource.Play();
            int otherElement = other.GetComponentInParent<Spell>().element;
            if (otherElement == element)
            {
                health -= 20;
            }
            else if (otherElement != 0)
            {
                health -= 10;
            }
            else
            {
                health -= 5;
            }

        }
    }
}
