using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    PlayerController playerController;

    // Taking Damage
    [SerializeField] float damageCooldown;
    float maxDamageCooldown;
    [SerializeField] float knockbackForce;
    [SerializeField] float damageTime;
    [SerializeField] Renderer staffRnd;
    [SerializeField] ParticleSystem staffParticle;
    [SerializeField] ParticleSystem staffPuff;
    [SerializeField] AudioClip[] clips;
    AudioSource audioSource;





    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.player == null)
            GameManager.instance.player = gameObject;

        GameManager.instance.playerHealth = GameManager.instance.maxPlayerHealth;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        maxDamageCooldown = damageCooldown;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(damageCooldown >= 0)
        damageCooldown -= Time.deltaTime;

        staffRnd.material = GameManager.instance.elementMaterials[(int)GameManager.instance.playerElement];
        var staffParticleMain = staffParticle.main;
        staffParticleMain.startColor = GameManager.instance.elementColors[(int)GameManager.instance.playerElement];
    }

    public void StaffPuff()
    {
        var staffPuffMain = staffPuff.main;
        staffPuffMain.startColor = GameManager.instance.elementColors[(int)GameManager.instance.playerElement];
        staffPuff.Play();
    }

    void Knockback(Transform collision)
    {
        rb.isKinematic = false;
        rb.AddForce((transform.position - collision.position).normalized * knockbackForce, ForceMode.Impulse);
        StartCoroutine(StopKnockback());
    }

    IEnumerator StopKnockback()
    {
        yield return new WaitForSeconds(damageTime);
        rb.isKinematic = true;
        playerController.playerAgent.destination = transform.position;

    }

    public void HealSound()
    {
        audioSource.PlayOneShot(clips[1]);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Enemy") && damageCooldown < 0)
        {
            GameManager.instance.playerHealth--;
            if (GameManager.instance.playerHealth < 1)
            {
                audioSource.PlayOneShot(clips[2]);
            }
            else
            {
                audioSource.PlayOneShot(clips[0]);
            }
            damageCooldown = maxDamageCooldown;
            CanvasController.instance.HitFlash();
            Knockback(other.transform);
        }
    }


}
