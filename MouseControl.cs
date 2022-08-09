using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author: Christopher Cruz
//Help by Stephen Barr on Youtube Tutorial for reference
//Camera movement/aim for FP controls
public class MouseControl : MonoBehaviour
{
    public enum RotationAxis
    {
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxis axes = RotationAxis.MouseX;
    public float minVert = -45.0f;
    public float maxVert = 45.0f;
    public int sensHorizontal;
    public int sensVertical;
    public float _rotationX = 0;
    GravityMechanics  gravMech;
    public GameManager GameManager;
    //Locks mouse cursor to screen
    void Start()
    {
        gravMech = GetComponentInParent<GravityMechanics>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Basic FP camera movement

   public void Update()
    {
        if(!gravMech.isRotate && GameManager.playerInput)
        {
            if(axes == RotationAxis.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensHorizontal, 0);
            }
            else if(axes == RotationAxis.MouseY)
            {
                _rotationX -= Input.GetAxis("Mouse Y") * sensVertical;
                _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

                float rotationY = transform.localEulerAngles.y;

                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
        }
    }
}
