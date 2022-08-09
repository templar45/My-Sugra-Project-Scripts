using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Christopher Cruz
//Creates a gravity orbit around objects.
public class GravityTrigger : MonoBehaviour
{
    public float gravity;
    public Collider box;
    public GravityCollider gravityCollider;

    //Once Player enters the trigger they are pulled and rotated toward the object until they touch the surface.
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hey Gravity");
        if(other.GetComponent<PlayerMovement>())
        {
            PlayerMovement myMovement = other.GetComponent<PlayerMovement>();
            //myMovement.gravityMech.GravityOff();
            myMovement.gravityOrbit = this.GetComponent<GravityTrigger>();
            other.GetComponent<PlayerMovement>().gravityCollider = this.GetComponent<GravityTrigger>().gravityCollider;
        }
    }
    //A debug safeguard if for any reason the player happend to exit the trigger without turning off the local gravity or without using world gravity;
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Bye Gravity");
        if(other.GetComponent<PlayerMovement>())
        {
            //other.GetComponent<PlayerMovement>().gravityOrbit = null;
            //box.enabled = false;
        }
    }
}
