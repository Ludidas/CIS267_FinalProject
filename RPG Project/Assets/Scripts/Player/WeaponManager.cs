using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void attackSword()
    {
        Debug.Log("WeaponManager Called");
        anim.SetTrigger("swordAttack");
    }
}
