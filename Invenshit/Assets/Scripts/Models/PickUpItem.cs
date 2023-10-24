using System.Collections;
using System.Collections.Generic;
using Inventory.Object;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [field:SerializeField]
    public ItemScript Item {get; private set;}
    [field:SerializeField]
    public int Amount {get; set;} = 1;
}
