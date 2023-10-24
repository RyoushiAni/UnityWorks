using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Object;
using Inventory.Ui;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage InventoryUi;
    [SerializeField]
    private InventoryObject inventoryData;
    public List<InventoryItem> InitialItems = new List<InventoryItem>();
    
    private void Start(){
        PrepareUI();
        PrepareInventoryData();
    }
    private void PrepareInventoryData(){
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdate += UpdateInventoryUI;
        foreach (InventoryItem item in InitialItems)
        {
            if(item.IsEmpty)
                continue;
            inventoryData.AddItem(item);
        }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            InventoryUi.ResetAllItems();
            foreach (var item in inventoryState)
            {
                InventoryUi.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.Amount);
            }
            
        }

        private void PrepareUI(){
        InventoryUi.InitializeInventoryUi(inventoryData.Size);
        this.InventoryUi.OnDescriptionRequested += HandleDescriptionRequest;
        this.InventoryUi.OnSwapItems += HandleSwapItems;
        this.InventoryUi.OnStartDragging += HandleDragging;
        this.InventoryUi.OnActionRequested += HandleActionRequest;
    }

    private void HandleActionRequest(int itemIndex)
    {
       InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
       if(inventoryItem.IsEmpty)
            return;
        IItemAction itemAction = inventoryItem.item as IItemAction;
        if(itemAction != null){
            InventoryUi.ShowItemAction(itemIndex);
            InventoryUi.AddAction(itemAction.ActionName, () => performAction(itemIndex));
        }
        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if(destroyableItem != null){
            InventoryUi.AddAction("Drop", ()=> DropItem(itemIndex, inventoryItem.Amount));
        }
    }

        private void DropItem(int itemIndex, int amount)
        {
            inventoryData.RemoveItem(itemIndex, amount);
            InventoryUi.ResetSelection();
        }

        public void performAction(int itemIndex){
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
       if(inventoryItem.IsEmpty)
            return;
        IItemAction itemAction = inventoryItem.item as IItemAction;
        if(itemAction != null){
            itemAction.PerFormAction(gameObject, inventoryItem.itemState);
            if(inventoryData.GetItemAt(itemIndex).IsEmpty)
                InventoryUi.ResetSelection();
        }
        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if(destroyableItem != null){
            inventoryData.RemoveItem(itemIndex, 1);
        }
    }

    private void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if(inventoryItem.IsEmpty)
            return;
        InventoryUi.CreateDragItem(inventoryItem.item.ItemImage, inventoryItem.Amount);
    }

    private void HandleSwapItems(int itemIndex1, int itemIndex2)
    {
        inventoryData.SwapItems(itemIndex1,itemIndex2);
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if(inventoryItem.IsEmpty){
            InventoryUi.ResetSelection();
            return;
        }
            
        ItemScript item = inventoryItem.item;
        string Description = PrepareDescription(inventoryItem);
        InventoryUi.UpdateDescription(itemIndex, item.ItemImage, item.Name, Description);
    }
    public string PrepareDescription(InventoryItem inventoryItem){
        StringBuilder sb = new StringBuilder();
        sb.Append(inventoryItem.item.Description);
        sb.AppendLine();
        for (int i = 0; i < inventoryItem.itemState.Count; i++)
        {
            sb.Append($"{inventoryItem.itemState[i].parameters.ParameterName}" + $": {inventoryItem.itemState[i].value} / {inventoryItem.item.DefaultParametersList[i].value}");
        }
        return sb.ToString();
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.B)){
            if(InventoryUi.isActiveAndEnabled == false){
                InventoryUi.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    InventoryUi.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.Amount);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            InventoryUi.Hide();
        }


 }
}
}