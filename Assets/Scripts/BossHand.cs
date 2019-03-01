using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour
{
    [SerializeField] Boss boss;
    [SerializeField] int element;
    Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        ChangeElement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeElement()
    {
        int newElement = (int)UnityEngine.Random.Range(1, 4);
        while (newElement == element)
        {
            newElement = (int)UnityEngine.Random.Range(1, 4);
        }
        element = newElement;
        renderer.material = GameManager.instance.elementMaterials[element];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spell"))
        {
            int otherElement = other.GetComponentInParent<Spell>().element;
            if (otherElement == element)
            {
                boss.health -= 20;
            }
            else if (otherElement != 0)
            {
                boss.health -= 10;
            }
            else
            {
                boss.health -= 5;
            }
            ChangeElement();
        }
    }
}
