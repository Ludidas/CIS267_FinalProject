using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootHitbox : MonoBehaviour
{
    private PlayerManager pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponentInParent<PlayerManager>();
    }

    #region Collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ice Collision
        pm.iceTriggerEnter(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Ice Block Collision
        pm.iceBlockCollisionStay(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Ice Collision
        pm.iceTriggerExit(collision);

        //Cave Ground Collision
        pm.caveGroundTriggerExit(collision);
    }

        #endregion
    }
