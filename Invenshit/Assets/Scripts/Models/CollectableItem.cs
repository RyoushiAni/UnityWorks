using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inventory.Object{
    [CreateAssetMenu(menuName = "Item/Collectable Item", fileName ="New Collectable")]
public class CollectableItem : ItemScript{
        public void Awake(){
            Type = "Collectable";
        }

}
}