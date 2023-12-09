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
        damage = 1f;
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Here");
            collision.gameObject.GetComponent<Enemy>().takeDamage(damage);
            if(collision.gameObject.GetComponent<Enemy>().getHealth() <= 0)
            {
                collision.gameObject.GetComponent<Enemy>().onDeath();
            }
        }
        
    }

}
