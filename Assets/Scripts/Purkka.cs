using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purkka : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CanvasController.instance.gameObject.SetActive(true);
        Cursor.visible = false;
    }
}
