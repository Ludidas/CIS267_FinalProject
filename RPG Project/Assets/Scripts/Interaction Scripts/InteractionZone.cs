using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    private GameObject selectedObject;

    private void Start()
    {
        selectedObject = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (selectedObject == null && collision.CompareTag("Text"))
        {
            selectedObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (selectedObject == collision.gameObject)
        {
            selectedObject = null;
        }
    }

    public GameObject getInteractionObject()
    {
        return selectedObject;
    }
}
