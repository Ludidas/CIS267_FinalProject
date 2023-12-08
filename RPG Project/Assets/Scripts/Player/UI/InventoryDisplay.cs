using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class InventoryDisplay : MonoBehaviour{

    [SerializeField] private Sprite[] items;
    [Header("Must Match Sprite Images")]
    [SerializeField] private string[] itemNames;

    [SerializeField] private GameObject displayedItem;

    private string localItem;


    void Start(){
        changeItem();
    }

    void Update(){

        if (localItem != InventorySystem.getCurItem()) {
            changeItem();
        }
    }


    public void changeItem() {
        localItem = InventorySystem.getCurItem();

        int pos = 0; //pos 0 is for no item

        // start at 1 because 0 is used for no item held
        for (int i = 1; i < itemNames.Length; i++) {
            if (localItem == itemNames[i]) {
                pos = i; break;
            }
        }

        displayedItem.GetComponent<Image>().sprite = items[pos];
    }
}
