using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Christopher Cruz
//Script used for second mechanic.  Gives the illusion fo the gravity gun giving graivty to objects.  Players can choose how many they want on and turned off.
public class GravityCollider : MonoBehaviour
{
    [SerializeField]
    public bool isGravity;
    PlayerMovement player;
    GravityMechanics mechanics;
    public Collider isOrbit;
    public Collider isTrigger;
    //GravityTrigger trigger;
    public ParticleSystem particle;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        mechanics = GameObject.Find("Player").GetComponent<GravityMechanics>();

        particle = GetComponentInChildren<ParticleSystem>();

    }
    void Update()
    {
        //Condition needed to ensure the gravity reset key doesnt break the game and as well reset the gameobjects tags 
       if(mechanics.isBack || mechanics.isForward || mechanics.isTop || mechanics.isLeft || mechanics.isRight && isGravity)
        {
            isGravity = false;
            if(particle != null)
            {
                transform.gameObject.tag = "NoFaux";
                particle.Stop();
                isTrigger.enabled = false;
                isOrbit.enabled = false;
                player.gravityOrbit = null;
            }
        }
    }

    //if raycast hits the gameobject and changes there tag based on the if condition.
    //this illustrates a gravity on and off scenario.
    public void HitByRay () 
    {
        if(!isGravity && mechanics.wallGravity)
        {
            isGravity = true;
            particle.Play();
            isOrbit.enabled = true;
            isTrigger.enabled = true;
            mechanics.GravityOff();

            transform.gameObject.tag = "FauxGravity";
        }
        else if(!isGravity && !mechanics.wallGravity)
        {
            isGravity = true;
            particle.Play();
            isOrbit.enabled = true;
            isTrigger.enabled = true;

            transform.gameObject.tag = "FauxGravity";
        }
        else if(isGravity)
        {
            isOrbit.enabled = false;
            isTrigger.enabled = false;
            player.gravityOrbit = null;
            player.isFaux = false;
            isGravity = false;
           
            particle.Stop();
            transform.gameObject.tag = "NoFaux";
        }
    }
    public void GravDeactivate()
    {
        isOrbit.enabled = false;
        isTrigger.enabled = false;
        player.gravityOrbit = null;
        isGravity = false;
        mechanics.gravity = -9.81f;
        Physics.gravity = new Vector3(0, mechanics.gravity, 0);
        particle.Stop();
        transform.gameObject.tag = "NoFaux";
    }
}
