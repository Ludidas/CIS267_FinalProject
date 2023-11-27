using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorizeGameBlocks : MonoBehaviour
{
    [SerializeField] private float numOrder;

    public void interacted()
    {
        Debug.Log("Interaction Memorize Block");
        GetComponentInParent<MemorizeGame>().blockClicked(numOrder);
    }
}
