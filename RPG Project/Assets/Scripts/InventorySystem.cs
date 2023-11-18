using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //useItem() - this is where the code of of an item use will go (uses current item)
    //removeItem() - removes the current item in hand from inventory (DOES NOT DELETE THE ITEM IN HAND)
    //itemCycleUp() - switches to the next item in the inventory
    //itemCycleDown() - switches to the previous item in the inventory
    //displayInventory() - displays all the inventory items in the log (for testing)
    //getCurItem() - returns the current Item (useful when instatiating)
    //
    //NOTE: THIS CODE DOES NOT AND CAN NOT HANDLE THE INSTANTIATE FUNCTION! This means the script cannot make an
    //item spawn in the game, it will only cycle through items in the inventory. (instantiation must be handled
    //in a different, nonstatic script).
    //
    //pls msg me if there is any sort of problem with this script or if you suggest changes.
    //===========================================================================================================


    private static GameObject[] inventory;
    private static GameObject curItem;
    private static int curPos;

    public static int inventorySize;
    public static GameObject empty;

    //========================================
    //PLEASE SET THESE

    //size of the inventory
    public static void setInventorySize(int s)
    {
        inventorySize = s;
    }

    //when a slot in the inventory is empty, an empty gameObject called "empty" will be a placeholder 
    public static void setEmpty(GameObject e)
    {
        empty = e;
    }
    //========================================

    
    //creates inventory and sets every spot in the inventory to empty    
    public static void initializeInv()
    {
        curPos = 0;

        inventorySize++;

        inventory = new GameObject[inventorySize];

        for(int i = 0; i < inventorySize; i++)
        {
            inventory[i] = empty;
        }

        curItem = inventory[curPos];

        //displayInventory();
    }

    //puts item into the player's inventory
    public static void addItemToInv(GameObject item)
    {
        //set current position in inventory to 0
        curPos = 0;

        //increment inventory position until there is an empty slot or the curPos is outside of the inventory Boundary
        do
        {
            curPos++;
        } while(inventory[curPos] != empty && curPos <= inventorySize);

        //inventory is full
        if(curPos == inventorySize && inventory[curPos] != empty)
        {
            return;
        }
           
        //the empty spot in the inventory will now be the item
        inventory[curPos] = item;

        //set curItem to the new item in inventory
        curItem = inventory[curPos];

        //this is the part where you will use instantiate in your script to show the item in the player's hand

        //displayInventory();
    }
    
    //once we have collectables
    //collectables with different tags can be used differently
    public static void useItem()
    {
        //Debug.Log("Using Item");

        //these are example tags to show how the code works
        if(curItem.tag.Equals("Food"))
        {
            //eats item
            //Debug.Log("Eating item");

            //removes it from inventory
        }
        else if(curItem.tag.Equals("Sword"))
        {
            //sword swing
        }
        else if(curItem.tag.Equals("Shield"))
        {
            //shield block
        }
    }

    //sets current item in inventory to empty
    public static void removeItem()
    {
        inventory[curPos] = empty;
    }

    //cycle to next spot in inventory
    public static void itemCycleUp()
    {
        //cycle up in inventory
        if(curPos == inventorySize)
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

    //cycle to previous spot in inventory
    public static void itemCycleDown()
    {
        //cycle down in inventory
        if(curPos == 0)
        {
            curPos = inventorySize;

            curItem = inventory[inventorySize];
        }
        else
        {
            curPos--;

            curItem = inventory[curPos];
        }
    }

    //for testing purposes only
    public static void displayInventory()
    {
        for (int i = 0; i < inventorySize; i++)
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
