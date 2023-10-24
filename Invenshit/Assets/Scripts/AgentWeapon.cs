using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Object;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private EquippableItems weapon;
    [SerializeField]
    private InventoryObject inventoryData;
    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;

    public void SetWeapon(EquippableItems weaponItem, List<ItemParameter> itemState){
        if(weapon != null){
            inventoryData.AddItem(weapon, 1, itemCurrentState);
        }
        this.weapon = weaponItem;
        this.itemCurrentState = new List<ItemParameter> (itemState);
        ModifyParameters();
    }

    private void ModifyParameters()
    {
        foreach (var parameter in parametersToModify)
        {
            if(itemCurrentState.Contains(parameter)){
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter{
                    parameters = parameter.parameters,
                    value = newValue
                };
            }
        }
    }
}
