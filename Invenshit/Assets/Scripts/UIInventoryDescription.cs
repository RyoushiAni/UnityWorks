using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Ui{
    public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField]
   private Image itemImage;
   [SerializeField]
   private Text ItemName;
   [SerializeField]
   private Text ItemDescription;
   public void Awake(){
    ResetDescription();
   }

   public void ResetDescription(){
    this.itemImage.gameObject.SetActive(false);
    this.ItemName.text = "";
    this.ItemDescription.text = "";
   }

    public void SetDescription(Sprite sprite, string itemName, string ItemDesc){
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.ItemName.text = itemName;
        this.ItemDescription.text = ItemDesc;
    }

    
    
    }


}
