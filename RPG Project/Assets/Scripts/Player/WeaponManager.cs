using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject bullet;

    public void attackSword()
    {
        anim.SetTrigger("swordAttack");
    }

    public void shootArmCannon()
    {
        anim.SetTrigger("cannonShoot");
        GameObject bul = Instantiate(bullet);
        bul.GetComponent<Transform>().position = transform.position;
        bul.GetComponent<Bullet>().setMovement(transform.rotation);
    }

    public void attackUpgradedSword()
    {
        anim.SetTrigger("upgradedSwordAttack");
    }
}
