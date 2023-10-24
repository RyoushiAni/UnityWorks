using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Ui{
public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;
    [SerializeField]
    private UIInventoryDescription UIDescription;
    [SerializeField]
    private RectTransform ContentPanel;
    [SerializeField]
    private Mousefollower MouseFollow;
    [SerializeField]
    private ItemActionMenuUI ActionMenu;

    private int currentlyDraggedInd = -1;
    public event Action<int> OnDescriptionRequested, 
                             OnActionRequested,
                             OnStartDragging;
    public event Action<int,int> OnSwapItems;


    List<UIInventoryItem> ListofItems = new List<UIInventoryItem>();
    private void Awake(){
        Hide();
        MouseFollow.Toggle(false);
        UIDescription.ResetDescription();
    }
    public void InitializeInventoryUi(int InventorySize){
        for(int i = 0; i < InventorySize; i++){
            UIInventoryItem UiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            UiItem.transform.SetParent(ContentPanel);
            ListofItems.Add(UiItem);
            UiItem.OnItemClicked += HandleItemSelection;
            UiItem.OnItemBeginDrag += HandleBeginDrag;
            UiItem.OnItemDroppedOn += HandleSwap;
            UiItem.OnItemEndDrag += HandleEndDrag;
            UiItem.OnRMouseBtnClicked += HandleShowItemOptions;
        }
    }

        internal void ResetAllItems()
        {
            foreach (var item in ListofItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        UIDescription.SetDescription(itemImage,name,description);
        DeselectAllItems();
        ListofItems[itemIndex].Select();
    }

    public void Show(){
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void ResetSelection()
    {
        UIDescription.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        ActionMenu.Toggle(false);
        foreach (UIInventoryItem item in ListofItems)
        {
            item.Deselect();
        }
        
    }
    public void AddAction(string ActionName, Action performAction){
        ActionMenu.AddButton(ActionName, performAction);
    }
    public void ShowItemAction(int itemIndex){
        ActionMenu.Toggle(true);
        ActionMenu.transform.position = ListofItems[itemIndex].transform.position;
    }

    public void Hide(){
        ActionMenu.Toggle(false);
        gameObject.SetActive(false);
        ResetDragData();
        
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int ItemAmount){
        if(ListofItems.Count > itemIndex){
            ListofItems[itemIndex].SetData(itemImage, ItemAmount);
        }
    }
    private void HandleShowItemOptions(UIInventoryItem UIitem)
    {
        int index = ListofItems.IndexOf(UIitem);
        if(index == -1)
            return;
        OnActionRequested?.Invoke(index);
    }

    private void HandleEndDrag(UIInventoryItem UIItem)
    {
        ResetDragData();
    }

    private void HandleSwap(UIInventoryItem UIItem)
    {
        int index = ListofItems.IndexOf(UIItem);
        if(index == -1){
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedInd, index);
        HandleItemSelection(UIItem);
            
    }

    private void HandleBeginDrag(UIInventoryItem UIItem)
    {
        
        int index = ListofItems.IndexOf(UIItem);
        if(index == -1)
            return;
        currentlyDraggedInd = index;
        HandleItemSelection(UIItem);
        OnStartDragging?.Invoke(index);
        
         
    }

    private void HandleItemSelection(UIInventoryItem UIItem)
    {
        int index = ListofItems.IndexOf(UIItem);
        if(index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);
    }
    public void CreateDragItem(Sprite sprite, int Amount){
        MouseFollow.Toggle(true);
        MouseFollow.SetData(sprite, Amount);
    }
    public void ResetDragData(){
            MouseFollow.Toggle(false);
            currentlyDraggedInd = -1;
    }
}
}
