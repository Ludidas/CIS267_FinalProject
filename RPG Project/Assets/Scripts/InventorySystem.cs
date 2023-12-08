using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public static class InventorySystem
{
    //=========================================================================================================
    //INVENTORY SYSTEM SCRIPT
    //
    //Author: Alex Thebolt
    //
    //Summary: handles the items that are currently in inventory
    //add and remove item functions are for public use if there
    //is ever a need to add/remove an item from the player's inventory
    //
    //Useable functions:
    //addItemToInv(GameObject) - adds item to the inventory
    //removeItem() - removes the current item in hand from inventory (DOES NOT DELETE THE ITEM IN HAND)
    //itemCycle() - switches to the next item in the inventory
    //displayInventory() - displays all the inventory items in the log (for testing)
    //getCurItem() - returns the current Item
    //resetInventory() - clears inventory and all weapon slots
    //
    //
    //pls msg me if there is any sort of problem with this script or if you suggest changes.
    //===========================================================================================================


    //This is the inventory size plus the empty slot for the hand
    //==============================================
    private static int inventoryMaxSize = 5;
    //==============================================

    private static string[] inventory = new string[inventoryMaxSize];
    private static string swordSlot;
    private static string upgradedSwordSlot;
    private static string armCannonSlot;
    private static string curItem;
    private static int curPos = 0;
    

    //puts item into the player's inventory
    public static bool addItemToInv(string itemName)
    {
        //if the item is a sword or other weapon, then it gets its own slot
        if(itemName == "Sword")
        {
            swordSlot = itemName;

            curPos = 10;

            return true;
        }
        else if(itemName == "Upgraded Sword")
        {
            upgradedSwordSlot = itemName;

            curPos = 11;

            return true;
        }
        else if(itemName == "ArmCannon")
        {
            armCannonSlot = itemName;

            curPos = 12;

            return true;
        }

        //set current position in inventory to 0
        curPos = 0;

        // inventory[0] is currently held item ?
        //increment inventory position until there is an empty slot or the curPos is outside of the inventory Boundary
        do 
        {
            curPos++;
        } while(inventory[curPos] != null && curPos < inventoryMaxSize-1);

        //inventory is full
        if(curPos == inventoryMaxSize-1 && inventory[curPos] != null)
        {
            Debug.Log("Full Inventory");

            return false;
        }
        
        Debug.Log(itemName + " stored in slot " + curPos);

        //the empty spot in the inventory will now be the item
        inventory[curPos] = itemName;

        //set curItem to the new item in inventory
        curItem = inventory[curPos];

        displayInventory();

        return true;
    }

    //sets current item in inventory to empty
    public static void removeItem()
    {
        curItem = null;

        inventory[curPos] = null;
    }

    //cycle to next spot in inventory
    public static void itemCycle()
    {
        //Debug.Log("CurPos before item cycle " + curPos);

        //cycle up in inventory
        if(curPos >= (inventoryMaxSize - 1))
        {
            curPos = 0;

            curItem = inventory[0];

            Debug.Log("CurPos (0 is first slot): " + curPos);
            Debug.Log("Cycled to: " + curItem);
        }
        else
        {
            curPos++;

            curItem = inventory[curPos];

            Debug.Log("CurPos (0 is first slot): " + curPos);
            Debug.Log("Cycled to: " + curItem);
        }
    }

    public static void equipSword()
    {
        curItem = swordSlot;
    }

    public static void equipUpgradedSword()
    {
        curItem = upgradedSwordSlot;
    }

    public static void equipArmCannon()
    {
        curItem = armCannonSlot;
    }

    //for testing purposes only
    public static void displayInventory()
    {
        for (int i = 0; i < inventoryMaxSize; i++)
        {
            Debug.Log(inventory[i]);
        }
    }
    
    //get the current item in hand
    public static string getCurItem()
    {
        return curItem;
    }

    public static void resetInventory()
    {
        swordSlot = null;
        upgradedSwordSlot = null;
        armCannonSlot = null;

        for (int i = 0; i < inventoryMaxSize; i++)
        {
            inventory[i] = null;
        }

        curPos = 0;
    }
}
