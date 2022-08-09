using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class closeButton : MonoBehaviour
{
    public Image letter;
    public Image Button;
    public GameObject inventoryMenu;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Close Button");
            letter.enabled = false;
            Button.enabled = false;
            inventoryMenu.SetActive(true);
        }
    }

    public void Close()
    {
        Debug.Log("Close Button");
        letter.enabled = false;
        Button.enabled = false;
        inventoryMenu.SetActive(true);
    }
}
