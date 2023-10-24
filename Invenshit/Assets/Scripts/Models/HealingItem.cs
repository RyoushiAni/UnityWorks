using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Object{
    [CreateAssetMenu(menuName = "Item/Consumable Item", fileName ="New Consumable")]
    public class HealingItem : ItemScript, IDestroyableItem, IItemAction
    {
        [SerializeField]
        private List<DataModifier> modifier = new List<DataModifier>();

        public string ActionName => "Use";
        public void Awake(){
            Type = "Healing";
        }

        public bool PerFormAction(GameObject character, List<ItemParameter> ItemState = null)
        {
            foreach (DataModifier data in modifier)
            {
                data.StatModifier.AffectCharacter(character, data.Value);
            }
            return true;
        }
    }
    public interface IDestroyableItem{

    }
    public interface IItemAction{
        public string ActionName{get;}
        bool PerFormAction(GameObject character, List<ItemParameter> ItemState);
    }
    [Serializable]
    public class DataModifier{
        public CharacterStatModifier StatModifier;
        public float Value;
    }
}

