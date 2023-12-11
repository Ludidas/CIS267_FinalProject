using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class InventoryDisplay : MonoBehaviour{

    [SerializeField] private Sprite[] items;
    [Header("Must Match Sprite Images")]
    [SerializeField] private string[] itemNames;

    [SerializeField] private GameObject displayedItem;

    [Header("Text Display")]
    [SerializeField] private GameObject slotTextGO;
    private TextMeshProUGUI slotText;

    private string localItem;


    void Start()
    {
        changeItem();
        slotText = slotTextGO.GetComponent<TextMeshProUGUI>();
    }

    void Update(){

        if (localItem != InventorySystem.getCurItem()) {
            changeItem();
        }

        changeTextDisplay();
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

    public void changeTextDisplay()
    {
        localItem = InventorySystem.getCurItem();
        string item = "";

        if (InventorySystem.curPos == -1)
        {
            if (localItem == "Sword")
            {
                item = "Sword";
            }
            if (localItem == "UpgradedSword")
            {
                item = "Upgraded Sword";
            }
            if (localItem == "ArmCannon")
            {
                item = "Arm Cannon";
            }
        }
        else
        {
            item = (InventorySystem.curPos + 1) + "";
        }

        slotText.SetText("Slot:\n" + item);
    }
}
