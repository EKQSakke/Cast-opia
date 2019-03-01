using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class Mortar : MonoBehaviour
{
    public GameObject rock;
    [SerializeField] float rockSpeed;
    [SerializeField] float startDelay;
    [SerializeField] AudioClip[] clips;
    AudioSource audioSource;
    int rnd;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rnd = Random.Range(0, 5);
    }
    // Update is called once per frame
    void Update()
    {
        startDelay -= Time.deltaTime;

        if (startDelay < 0)
        {
            rock.transform.Translate(Vector3.down * Time.deltaTime * rockSpeed);
            if (rock.transform.position.y < transform.position.y + 2)
            {
                if (!audioSource.isPlaying)
                audioSource.PlayOneShot(clips[rnd]);
                GetComponent<DecalProjectorComponent>().enabled = false;
                Destroy(gameObject, 0.2f);
            }
        }
    }
}
