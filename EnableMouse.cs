using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Christopher Cruz
//Script enables the mouse control movement on start to counter Unity's fps bug
public class EnableMouse : MonoBehaviour
{
    MouseControl mouse;
    //Enables Script on Start
    void Start()
    {
        mouse = GetComponent<MouseControl>();
        mouse.enabled = true;
    }

}
