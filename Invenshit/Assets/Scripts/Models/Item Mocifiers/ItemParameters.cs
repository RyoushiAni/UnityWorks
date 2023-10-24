using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Object{
[CreateAssetMenu(menuName = "Stat Modifiers/Item Parameters", fileName ="New Parameter")]
public class ItemParameters : ScriptableObject
{
    [field: SerializeField]
    public string ParameterName {get; private set;}
}
}