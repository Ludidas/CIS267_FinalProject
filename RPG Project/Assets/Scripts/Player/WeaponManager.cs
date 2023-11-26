using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private float delay;

    [SerializeField] private float damage;

    private float timeUntilMelee;
    

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    public void attackSword()
    {
        Debug.Log("WeaponManager Called");
        anim.SetTrigger("swordAttack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Damage Enemies
    }
}
