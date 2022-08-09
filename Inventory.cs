using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    
    public GameObject inventoryMenu;
    public GameManager gameManager;
    public bool isOn;
    // Start is called before the first frame update
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        isOn = false;
        //inventoryMenu = GameObject.Find("InventoryMenu").GetComponent<Canvas>();
        inventoryMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && !isOn)
        {
           isOn = true;
           inventoryMenu.SetActive(true);
           Time.timeScale = 0;
           Cursor.lockState = CursorLockMode.None;
           Cursor.visible = true;
           gameManager.playerInput = false;
           DontDestroyOnLoad(this);

        }
        else if(Input.GetKeyDown(KeyCode.I) && isOn)
        {
            inventoryMenu.SetActive(false);
            isOn = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            gameManager.playerInput = true;
        }
    }
}
