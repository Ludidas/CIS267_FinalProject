using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionText : MonoBehaviour
{
    [Header("Displayable Text")]
    [SerializeField] private string text;

    public string getText()
    {
        return text;
    }
}
