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
    [SerializeField] private float bulletCooldown;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject upgradedSword;

    private float bulletTimer;

    private void Start()
    {
        bulletTimer = 0f;
    }

    private void Update()
    {
        bulletTimer = bulletTimer - Time.deltaTime;
    }

    public void attackSword()
    {
        anim.SetTrigger("swordAttack");
        sword.GetComponent<Weapon>().startAnim();
    }

    public void shootArmCannon()
    {
        if (bulletTimer < 0)
        {
            anim.SetTrigger("cannonShoot");
            GameObject bul = Instantiate(bullet);
            bul.GetComponent<Transform>().position = transform.position;
            bul.GetComponent<Bullet>().setMovement(transform.rotation);
            bulletTimer = bulletCooldown;
        }

    }

    public void attackUpgradedSword()
    {
        anim.SetTrigger("upgradedSwordAttack");
        upgradedSword.GetComponent<Weapon>().startAnim();
    }
}
