using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IPickup
{
    public void Pick(SOPickup pickup);
}


public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryGrid;
    public GameObject gridPrefab;

    Dictionary<string, GridElement> inventoryList = new Dictionary<string, GridElement>();
    bool inventoryVisible;

    internal void addItem(SOPickup item)
    {

        if (!inventoryList.ContainsKey(item.name))
        {
            GameObject go = Instantiate(gridPrefab, inventoryGrid.transform);
            GridElement gridElem = go.GetComponent<GridElement>();
            inventoryList.Add(item.name, gridElem );

            gridElem.itemCount = 1;
            gridElem.itemImage = item.itemImage;
        }

        else
        {
            inventoryList[item.name].itemCount++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryVisible = !inventoryVisible;
            if(inventoryVisible)
            {
                
            }
            inventoryGrid.SetActive(inventoryVisible);
            
        }
    }

}
