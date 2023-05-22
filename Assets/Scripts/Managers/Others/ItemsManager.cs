using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : ProjectManager<ItemsManager>
{

    [SerializeField] private List<Item> itemsListPrefab;
    [SerializeField] private List<Item> inventory;
    [SerializeField] private Transform canvas;

    protected override void Awake()
    {
        base.Awake();
        InstantiateObject("Lettre");
        InstantiateObject("Ticket de séjour");
    }

    public void InstantiateObject(string nameObject)
    {
        Item itemPrefab = itemsListPrefab.Find(item => item.itemData.itemName == nameObject);
        if(itemPrefab != null)
        {
            Item item = Instantiate(itemPrefab, canvas);
            item.gameObject.SetActive(false);
            inventory.Add(item);
        }
    }

    public List<Item> GetInventory()
    {
        return inventory;
    }

    public List<Item> GetExaminedObjects()
    {
        return inventory.FindAll(item => item.itemData.isProofs);
    }

    public void ClearInventory(bool isInEditor = false)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if(isInEditor)
            {
                DestroyImmediate(inventory[i].gameObject);
            }
            else
            {
                Destroy(inventory[i].gameObject);
            }
            
        }
        inventory.Clear();
    }

    public void RemoveItem(string itemName, bool isInEditor = false)
    {
        int index = inventory.FindIndex(item => item.itemData.itemName == itemName);
        if(index == -1)
        {
            return;
        }
        if(isInEditor)
        {
            DestroyImmediate(inventory[index].gameObject);
        }
        else
        {
            Destroy(inventory[index].gameObject);
        }
        inventory.RemoveAt(index);
    }

}
