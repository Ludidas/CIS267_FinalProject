using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class IcefirePillers : MonoBehaviour
{
    public bool inrange;
    private float x;
    private float y;
    public GameObject icefire;
    private Icefire ic;
    public Transform Shootpoint;
    private float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        inrange = false;
        ic = icefire.GetComponent<Icefire>();
        cooldown = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(inrange )
        {
            if (cooldown <= 0)
            {
                shoot();
                cooldown = 3;
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }
    private void shoot()
    {
        Instantiate(ic, Shootpoint.position, transform.rotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inrange = true;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inrange = true;
        }
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inrange = false;
        }
    }
}
