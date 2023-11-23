using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadZone : MonoBehaviour
{
    //This script takes the given information and loads a scene with the name zoneName
    //and places the player in the provided x y coordinates of the area.
    [Header("Load Zone Info")]
    [SerializeField] private string zoneName;
    [SerializeField] private float xLoad;
    [SerializeField] private float yLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Ensure the player has regular control of the player upon entering the loadzone.
            //This will prevent loadzones from working when the player is on ice or falling.
            //This most likely won't ever be a problem but just in case this code is here.
            if (collision.gameObject.GetComponent<PlayerManager>().movementType == 1)
            {
                GameManager.setSpawnLocation(new Vector2(xLoad, yLoad));
                SceneManager.LoadScene(zoneName);
            }
            
        }
    }
}
