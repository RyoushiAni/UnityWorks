using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.Ui{
public class UIInventoryItem : MonoBehaviour, IPointerClickHandler,IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
   [SerializeField]
   private Image itemImage;
   [SerializeField]
   private Text AmountDisplay;
   [SerializeField]
   private Image BorderImg;
   public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag,OnRMouseBtnClicked;
   private bool EmptySlot = false;

   public void Awake(){
    ResetData();
    Deselect();
   }
   public void ResetData(){
    this.itemImage.gameObject.SetActive(false);
    EmptySlot =true;
   }
   public void Deselect(){
    BorderImg.enabled = false;
   }
   public void SetData(Sprite sprite, int amount){
    this.itemImage.gameObject.SetActive(true);
    this.itemImage.sprite = sprite;
    this.AmountDisplay.text = amount + "";
    EmptySlot = false;
   }
    public void Select(){
        BorderImg.enabled = true;
    }
    public void OnPointerClick(PointerEventData pointerData)
    {
        if(pointerData.button == PointerEventData.InputButton.Right){
            OnRMouseBtnClicked?.Invoke(this);
        } 
        else{
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(EmptySlot)
            return;
        OnItemBeginDrag?.Invoke(this);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}
}