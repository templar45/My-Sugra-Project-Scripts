using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class letterInteract : MonoBehaviour
{
    public Image letter;
    public Image Button;
    public GameObject inventoryMenu;
    void Start()
    {
        letter = GameObject.FindGameObjectWithTag("Letter").GetComponent<Image>();
        Button = GameObject.FindGameObjectWithTag("Close1").GetComponent<Image>();
        inventoryMenu = GameObject.FindGameObjectWithTag("InventoryUI");
    }
        
    public void OpenButton()
    {
        letter.enabled = true;
        Button.enabled = true;
        inventoryMenu.SetActive(false);
    }
}
