using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;
    public Inventory localPlayerData = new Inventory();
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        localPlayerData = GlobalControl.Instance.savedPlayerInv;
    }
    public void SaveInventory()
    {
        GlobalControl.Instance.savedPlayerInv = localPlayerData;
    }

}
