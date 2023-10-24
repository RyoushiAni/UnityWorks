using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Ui{
public class ItemActionMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;

    public void AddButton(string name, Action onClickAction){
        GameObject button = Instantiate(buttonPrefab, transform);
        button.GetComponent<Button>().onClick.AddListener(() => onClickAction());
        button.GetComponentInChildren<Text>().text=name;
    }
    public void Toggle(bool val){
        if(val == true)
            RemoveOldButtons();
        gameObject.SetActive(val);
        
    }

        private void RemoveOldButtons()
        {
            foreach (Transform ChildObjects in transform)
            {
                Destroy(ChildObjects.gameObject);
            }
        }
    }
}