using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fakeitem : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    void Start()
    {
        timer = (float).5;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <=0)
        {
            Destroy(this.gameObject);
        }
    }
}
