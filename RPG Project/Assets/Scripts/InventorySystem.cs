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
    //initializeInv() - creates inventory (call this in GameManager script or something)
    //addItemToInv(GameObject) - adds item to the inventory
    //removeItem() - removes the current item in hand from inventory (DOES NOT DELETE THE ITEM IN HAND)
    //itemCycle() - switches to the next item in the inventory
    //displayInventory() - displays all the inventory items in the log (for testing)
    //getCurItem() - returns the current Item (useful when instatiating)
    //
    //
    //pls msg me if there is any sort of problem with this script or if you suggest changes.
    //===========================================================================================================


    private static GameObject[] inventory = new GameObject[1];
    private static GameObject swordSlot;
    private static GameObject upgradedSwordSlot;
    private static GameObject armCannonSlot;
    private static GameObject curItem;
    private static int curPos;
    public static int inventoryMaxSize;
    

    //========================================
    //PLEASE SET THIS

    //size of the inventory
    public static void setInventoryMaxSize(int s)
    {
        inventoryMaxSize = s;
    }
    //========================================

    
    //creates inventory    
    public static void initializeInv()
    {
        inventory = new GameObject[inventoryMaxSize];

        curPos = 0;

        curItem = inventory[curPos];

        //displayInventory();
    }

    //puts item into the player's inventory
    public static void addItemToInv(GameObject item)
    {
        //if the item is a sword or other weapon, then it gets its own slot
        if(item.CompareTag("Sword"))
        {
            swordSlot = item;

            return;
        }
        else if(item.CompareTag("Upgraded Sword"))
        {
            upgradedSwordSlot = item;

            return;
        }
        else if(item.CompareTag("Arm Cannon"))
        {
            armCannonSlot = item;

            return;
        }

        //set current position in inventory to 0
        curPos = 0;

        //increment inventory position until there is an empty slot or the curPos is outside of the inventory Boundary
        do
        {
            curPos++;
        } while(inventory[curPos] != null && curPos <= inventoryMaxSize);

        //inventory is full
        if(curPos == inventoryMaxSize && inventory[curPos] != null)
        {
            return;
        }
           
        //the empty spot in the inventory will now be the item
        inventory[curPos] = item;

        //set curItem to the new item in inventory
        curItem = inventory[curPos];

        //displayInventory();
    }

    //sets current item in inventory to empty
    public static void removeItem()
    {
        inventory[curPos] = null;
    }

    //cycle to next spot in inventory
    public static void itemCycle()
    {
        //cycle up in inventory
        if(curPos == inventoryMaxSize)
        {
            curPos = 0;

            curItem = inventory[0];
        }
        else
        {
            curPos++;

            curItem = inventory[curPos];
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
            Debug.Log(inventory[i].tag.ToString());
        }
    }
    
    //get the current item in hand
    public static GameObject getCurItem()
    {
        return curItem;
    }
}
