using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Christopher Cruz
//Script that destroys prefab on impact when instantiated and calls function based on tag
public class projectile : MonoBehaviour
{
    [Range(5, 100)]
	public float destroyAfter;
	public bool destroyOnImpact = false;
	public float minDestroyTime;
	public float maxDestroyTime;
	GravityMechanics mechanics;
    public GameObject[] ignoreBox;
    public GameObject[] ignoreSphere;

    void Start()
    {
        ignoreTheseSphere();
        ignoreTheseBox();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
		mechanics = GameObject.FindGameObjectWithTag("Player").GetComponent<GravityMechanics>();
        StartCoroutine (DestroyTimer ());
        Physics.IgnoreCollision(player.GetComponent<CapsuleCollider>(), GetComponent<SphereCollider>());
    }
    private void OnCollisionEnter (Collision collision) 
	{
        Debug.Log(collision.transform.name);
		if (!destroyOnImpact) 
		{
            StartCoroutine (DestroyTimer ());
		}
		//Otherwise, destroy prefab on impact
		else 
		{
			Destroy (gameObject);
		}
        if (collision.transform.gameObject.GetComponent<EventTriggerScript>())
        {
            EventTriggerScript myTrigger = collision.transform.gameObject.GetComponent<EventTriggerScript>();
            if(Array.Exists(myTrigger.validTags, element => element == gameObject.tag))
            {
                myTrigger.TriggerActivate(gameObject);
            
            }
        }
		//checks if it hit tags related to local gravity
		if(collision.gameObject.tag == "NoFaux")
		{

			collision.transform.SendMessage("HitByRay");
		}

		else if(collision.gameObject.tag == "FauxGravity")
		{

			collision.transform.SendMessage("HitByRay");
		}

		//checks if it collided with tags related to world gravity
        else if (collision.gameObject.tag == "RightWall" && !mechanics.isRight)
        {
			mechanics.GravityRight();
            Debug.Log("Hit right wall");
        }
        else if (collision.gameObject.tag == "RightWall" && mechanics.isRight)
        {
			mechanics.GravityOff();
            Debug.Log("Hit right wall");
        }
        else if (collision.gameObject.tag == "LeftWall" && !mechanics.isLeft)
        {
			mechanics.GravityLeft();
            Debug.Log("Hit Left wall");
        }
        else if (collision.gameObject.tag == "LeftWall" && mechanics.isLeft)
        {
			mechanics.GravityOff();
            Debug.Log("Hit Left wall");
        }
        else if (collision.gameObject.tag == "TopWall" && !mechanics.isTop)
        {
			mechanics.GravityUp();
            Debug.Log("Hit top wall");
        }
        else if (collision.gameObject.tag == "TopWall" && mechanics.isTop)
        {
			mechanics.GravityOff();
            Debug.Log("Hit top wall");
        }
        else if (collision.gameObject.tag == "FrontWall" && !mechanics.isForward)
        {
			mechanics.GravityForward();
            Debug.Log("Hit front wall");
        }
        else if (collision.gameObject.tag == "FrontWall" && mechanics.isForward)
        {
			mechanics.GravityOff();
            Debug.Log("Hit front wall");
        }
        else if (collision.gameObject.tag == "BackWall" && !mechanics.isBack)
        {
			mechanics.GravityBack();
            Debug.Log("Hit back wall");
        }
        else if (collision.gameObject.tag == "BackWall" && mechanics.isBack)
        {
			mechanics.GravityOff();
            Debug.Log("Hit back wall");
        }
        else if (collision.gameObject.tag == "Ground" && !mechanics.isBottom)
        {
            mechanics.GravityOff();
			Debug.Log("Hit Ground");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RightWall" && !mechanics.isRight)
        {
			mechanics.GravityRight();
            Debug.Log("Hit right wall");
        }
        else if (other.gameObject.tag == "RightWall" && mechanics.isRight)
        {
			mechanics.GravityOff();
            Debug.Log("Hit right wall");
        }
        else if (other.gameObject.tag == "LeftWall" && !mechanics.isLeft)
        {
			mechanics.GravityLeft();
            Debug.Log("Hit Left wall");
        }
        else if (other.gameObject.tag == "LeftWall" && mechanics.isLeft)
        {
			mechanics.GravityOff();
            Debug.Log("Hit Left wall");
        }
        else if (other.gameObject.tag == "TopWall" && !mechanics.isTop)
        {
			mechanics.GravityUp();
            Debug.Log("Hit top wall");
        }
        else if (other.gameObject.tag == "TopWall" && mechanics.isTop)
        {
			mechanics.GravityOff();
            Debug.Log("Hit top wall");
        }
        else if (other.gameObject.tag == "FrontWall" && !mechanics.isForward)
        {
			mechanics.GravityForward();
            Debug.Log("Hit front wall");
        }
        else if (other.gameObject.tag == "FrontWall" && mechanics.isForward)
        {
			mechanics.GravityOff();
            Debug.Log("Hit front wall");
        }
        else if (other.gameObject.tag == "BackWall" && !mechanics.isBack)
        {
			mechanics.GravityBack();
            //Destroy(gameObject);
            Debug.Log("Hit back wall");
        }
        else if (other.gameObject.tag == "BackWall" && mechanics.isBack)
        {
			mechanics.GravityOff();
            Debug.Log("Hit back wall");
        }
        else if (other.gameObject.tag == "Ground" && !mechanics.isBottom)
        {
            mechanics.GravityOff();
			Debug.Log("Hit Ground");
        } 
    }

    private IEnumerator DestroyTimer () 
	{
		//Wait random time based on min and max values
		yield return new WaitForSeconds
			(UnityEngine.Random.Range(minDestroyTime, maxDestroyTime));
		//Destroy prefab
		Destroy(gameObject);
	}
    void ignoreTheseBox()
    {
        GameObject[] ignoreBox = GameObject.FindGameObjectsWithTag("IgnoreBox");

        for(int i = 0; i < ignoreBox.Length; i++)
        {
            {
                Physics.IgnoreCollision(ignoreBox[i].GetComponent<BoxCollider>(), GetComponent<SphereCollider>());
            }
        }
    }
    void ignoreTheseSphere()
    {
        GameObject[] ignoreSphere = GameObject.FindGameObjectsWithTag("IgnoreSphere");

        for(int i = 0; i < ignoreSphere.Length; i++)
        {
            {
                Physics.IgnoreCollision(ignoreSphere[i].GetComponent<SphereCollider>(), GetComponent<SphereCollider>());
            }
        }
    }
}
