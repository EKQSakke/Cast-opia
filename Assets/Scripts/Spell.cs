using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] ParticleSystemRenderer particleRenderer;
    [SerializeField] Renderer spellRenderer;
    public int element;

    // Start is called before the first frame update
    void Start()
    {
        int mat = (int)GameManager.instance.playerElement;
        particleRenderer.material = GameManager.instance.elementMaterials[mat];
        spellRenderer.material = GameManager.instance.elementMaterials[mat];
        Destroy(gameObject, 2f);
    }
}
