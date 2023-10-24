using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Object{
public abstract class ItemScript : ScriptableObject
{
  [field: SerializeField]
  public bool IsStackable{get;set;}

  public int ID => GetInstanceID();
   [field: SerializeField]
  public string Type{get; set;}
  public int MaxStackSize{get;set;} = 1;
  [field: SerializeField]
  public string Name{get;set;}
  [field: SerializeField]
  [field: TextArea]
  public string Description{get;set;}
  [field: SerializeField]
  public Sprite ItemImage{get;set;}
  [field: SerializeField]
  public List<ItemParameter> DefaultParametersList {get;set;}

  

}
 [Serializable]
 public struct ItemParameter : IEquatable<ItemParameter>{
    public ItemParameters parameters;
    public float value;
    public bool Equals(ItemParameter other){
      return other.parameters == parameters;
    }
 }
}