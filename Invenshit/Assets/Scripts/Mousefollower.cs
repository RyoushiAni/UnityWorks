using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Ui{
public class Mousefollower : MonoBehaviour
{
  [SerializeField]
  private Canvas canvas;
  [SerializeField]
  private UIInventoryItem Item;

  public void Awake(){
    canvas = transform.root.GetComponent<Canvas>();
    Item = GetComponent<UIInventoryItem>();
  }
  public void SetData(Sprite sprite, int Amount){
     Item.SetData(sprite, Amount);
  }
  void Update(){
    Vector2 position;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
        (RectTransform)canvas.transform, Input.mousePosition, canvas.worldCamera, out position);
    transform.position = canvas.transform.TransformPoint(position);
  }
  public void Toggle(bool Val){
    Debug.Log($"Item toggled {Val}");
    gameObject.SetActive(Val);

  }

}
}