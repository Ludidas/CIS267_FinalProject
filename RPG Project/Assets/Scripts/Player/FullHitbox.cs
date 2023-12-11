using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHitbox : MonoBehaviour
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
        //Cave Under Collision
        pm.caveUnderTriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Cave Under Collision
        pm.caveUnderTriggerExit(collision);
    }

    #endregion
}
