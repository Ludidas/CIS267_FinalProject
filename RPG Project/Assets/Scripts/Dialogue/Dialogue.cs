// You don't need to touch this class.
// You will add a name and dialogue to your gameobject when you attach the DialgoueTrigger script to it. 

using UnityEngine;

[System.Serializable]
public class Dialogue{
    public string name;

    [TextArea(4,10)]
    public string[] sentences;
}
