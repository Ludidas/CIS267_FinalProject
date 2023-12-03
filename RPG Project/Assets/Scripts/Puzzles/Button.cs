using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private bool beenClicked = false;

    private void Start()
    {
        GetComponent<Renderer>().material.color = new Color(0.4811f, 0.4811f, 0.4811f);
    }

    public void onClick()
    {
        if (!beenClicked)
        {
            beenClicked = true;
            GetComponent<Renderer>().material.color = Color.white;
            GetComponentInParent<DungeonCavePuzzleManager>().openButtonGate();
        }
        
    }
}
