using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSpell : MonoBehaviour
{
    [SerializeField] Transform spell;

    private void Update()
    {
        transform.position = spell.position;
    }
}
