using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Object{
[CreateAssetMenu(menuName = "Inventory", fileName = "New Inentory")]
public class InventoryObject : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> inventoryItems;

    [field: SerializeField]
    public int Size{get;set;} = 10;
    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdate;
        public void Initialize(){
        inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }
    public int AddItem(ItemScript newItem, int newAmount, List<ItemParameter> ItemState = null){
        if(newItem.IsStackable == false)
        {
            for (int i = 0; i < inventoryItems.Count; i++){
            while(newAmount > 0 && IsInventoryFull() == false){
                newAmount -= AddtoFreeSlot(newItem, 1, ItemState);
                
            }
            InformAboutChange();
            return newAmount;
            }
            
        }
            newAmount = AddStackableItem(newItem, newAmount);
            InformAboutChange();
            return newAmount;
    }

        private int AddtoFreeSlot(ItemScript Item, int NAmount, List<ItemParameter> itemState = null)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = Item,
                Amount = NAmount,
                itemState = new List<ItemParameter>(itemState == null ? Item.DefaultParametersList : itemState)
            };
            
            for (int i = 0; i < inventoryItems.Count; i++){
                if(inventoryItems[i].IsEmpty){
                    inventoryItems[i] = newItem;
                    return NAmount;
            }
            }
            return 0;
        }

        private bool IsInventoryFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false;


        private int AddStackableItem(ItemScript newItem, int newAmount)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if(inventoryItems[i].IsEmpty)
                    continue;
                if(inventoryItems[i].item.ID == newItem.ID){
                    int ItemLimit = inventoryItems[i].item.MaxStackSize - inventoryItems[i].Amount;
                    if(newAmount > ItemLimit){
                        inventoryItems[i] = inventoryItems[i].ChangeAmount(inventoryItems[i].item.MaxStackSize);
                        newAmount -= ItemLimit;
                    }
                    else{
                        inventoryItems[i] = inventoryItems[i].ChangeAmount(inventoryItems[i].Amount + newAmount);
                        InformAboutChange();
                        return 0; 
                    }

                }
            }
            while(newAmount > 0 && IsInventoryFull() == false){
                int ClampAmount = Mathf.Clamp(newAmount, 0, newItem.MaxStackSize);
                newAmount -= ClampAmount;
                AddtoFreeSlot(newItem, ClampAmount);
            }
            return newAmount;
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if(inventoryItems.Count > itemIndex){
                
            if(inventoryItems[itemIndex].IsEmpty)
                return;
            int remainder = inventoryItems[itemIndex].Amount - amount;
            if (remainder<=0)
                inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
            else
                inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeAmount(remainder);
            InformAboutChange();
            }
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.Amount);
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState() {
      Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
      for (int i = 0; i < inventoryItems.Count; i++)
      {
        if(inventoryItems[i].IsEmpty)
            continue;
        returnValue[i] = inventoryItems[i];
      }  
      return returnValue;
    }

    public InventoryItem GetItemAt(int itemIndex)
    {
        return inventoryItems[itemIndex];
    }

    public void SwapItems(int itemIndex1, int itemIndex2)
        {
            InventoryItem item1 = inventoryItems[itemIndex1];
            inventoryItems[itemIndex1] = inventoryItems[itemIndex2];
            inventoryItems[itemIndex2] = item1;
            InformAboutChange();
        }
    public void InformAboutChange()
        {
            OnInventoryUpdate?.Invoke(GetCurrentInventoryState());
        }
    }
[Serializable]
public struct InventoryItem{
    public int Amount;
    public ItemScript item;
    public List<ItemParameter> itemState;
    public bool IsEmpty => item == null;
    public InventoryItem ChangeAmount(int newAmount){
        return new InventoryItem{
            item = this.item,
            Amount = newAmount,
            itemState = new List<ItemParameter>(this.itemState),
        };
    }
    public static InventoryItem GetEmptyItem() => new InventoryItem{
        item = null,
        Amount = 0,
        itemState = new List<ItemParameter>()
    };
}
}