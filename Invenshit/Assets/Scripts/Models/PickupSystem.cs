using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Object;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField]
    private InventoryObject InventoryData, PermanentData, ConsumableData, CollectableData;
    private void OnTriggerStay(Collider collider){
        if((Input.GetKeyDown(KeyCode.F))){
            ItemPickup(collider.gameObject);
        }
    }

    public void ItemPickup(GameObject ColItem)
    {
        var item = ColItem.GetComponent<PickUpItem>();
        if(item != null){
            int remainder = InventoryData.AddItem(item.Item, item.Amount);
            if(item.Item.Type == "Equipable")
                remainder = PermanentData.AddItem(item.Item, item.Amount);
            else if(item.Item.Type == "Healing")
                remainder = ConsumableData.AddItem(item.Item, item.Amount);
            else if(item.Item.Type == "Collectable")
                remainder = CollectableData.AddItem(item.Item, item.Amount);
            
            if(remainder != 0)
                item.Amount = remainder;
                
        }
    }
}
