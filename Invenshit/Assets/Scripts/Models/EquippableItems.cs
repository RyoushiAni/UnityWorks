using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Object{
    [CreateAssetMenu(menuName = "Item/Equipable Item", fileName ="New Equipable")]
    public class EquippableItems : ItemScript, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";
        public void Awake(){
            Type = "Equipable";
        }
        public bool PerFormAction(GameObject character, List<ItemParameter> ItemState = null)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            if(weaponSystem != null){
                weaponSystem.SetWeapon(this, ItemState == null? DefaultParametersList : ItemState);
                return true;
            }
            return false;
        }
    }
}
