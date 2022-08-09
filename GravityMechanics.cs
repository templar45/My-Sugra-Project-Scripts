using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Author:  Christopher Cruz
//First Gravity mechanic that flips gravity in the z,y,x axis and there negatives.
public class GravityMechanics : MonoBehaviour
{
    public float gravity;
    public bool isTop;
    public bool isRight;
    public bool isBottom;
    public bool isLeft;
    public bool isForward;
    public bool isBack;
    public bool wallGravity;
    [HideInInspector]
    public bool isRotate;
    [Header("Used to Control the rotation speed when using world gravity")]
    public float rotateTimeTwo;
    public float rotateTimeOne;

    public Camera cameraAtUse;
    public float range = 200f;
    private float radius = 0.1f;
    [HideInInspector]
    public UIChange cameraMouseControl;
    [HideInInspector]
    public PlayerMovement player;


    public void Start()
    {
        cameraMouseControl = cameraAtUse.GetComponent<UIChange>();
    }
    //Function used to turn gravity off if its already on
    public void GravityOff()
    {
        isBack = false;
        isForward = false;
        isLeft = false;
        isRight = false;
        isTop = false;
        isBottom = true;
        wallGravity = false;
        player.isFaux = false;
        

        StartCoroutine(RotateReset(this.transform, Quaternion.identity, rotateTimeOne));

        gravity = -9.81f;
        Physics.gravity = new Vector3(0, gravity, 0);
    }
    //this special version only gets called from gravity trigger's ontriggerenter.

    public void GravityUp()
    {
        isBottom = false;
        isBack = false;
        isForward = false;
        isLeft = false;
        isRight = false;
        isTop = true;
        wallGravity = true;
        //DeactivateLast();
        StartCoroutine(RotatePlayerTop());

        gravity = 9.81f;
        Physics.gravity = new Vector3(0, gravity, 0);
    }
    public void GravityLeft()
    {
        isBottom = false;
        isBack = false;
        isForward = false;
        isLeft = true;
        isRight = false;
        isTop = false;
        wallGravity = true;
        //DeactivateLast();
        StartCoroutine(RotatePlayerLeft());

        gravity = -9.81f;
        Physics.gravity = new Vector3(gravity, 0, 0);
    }

    public void GravityRight()
    {
        isBottom = false;
        isBack = false;
        isForward = false;
        isLeft = false;
        isRight = true;
        isTop = false;
        wallGravity = true;
        //DeactivateLast();
        StartCoroutine(RotatePlayerRight());

        gravity = 9.81f;
        Physics.gravity = new Vector3(gravity, 0, 0);
    }

    public void GravityForward()
    {
        isBottom = false;
        isBack = false;
        isForward = true;
        isLeft = false;
        isRight = false;
        isTop = false;
        wallGravity = true;
        //DeactivateLast();
        StartCoroutine(RotatePlayerForward());

        gravity = 9.81f;
        Physics.gravity = new Vector3(0, 0, gravity);
    }

    public void GravityBack()
    {
        isBottom = false;
        isBack = true;
        isForward = false;
        isLeft = false;
        isRight = false;
        isTop = false;
        wallGravity = true;
        //DeactivateLast();
        StartCoroutine(RotatePlayerBack());

        gravity = -9.81f;
        Physics.gravity = new Vector3(0, 0, gravity);
    }

    //Rotates player based on which way gravity is pulled using IEnumerator.
    //Rotate player to orientation is Enumerated first then rotates them based in gravity on axis.
    IEnumerator RotatePlayerRight()
    {
        yield return StartCoroutine(RotateReset(this.transform, Quaternion.identity, rotateTimeOne));

        isRotate = true;
        float timeElapsed = 0;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 0, 90);

        while(timeElapsed < rotateTimeTwo)
        {

            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed/rotateTimeTwo);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
        isRotate = false;
    }

    IEnumerator RotateReset(Transform target, Quaternion rot, float dur)
    {
        isRotate = true;
        float endTimer = 0f;
        Quaternion intialStart = target.rotation;

        while( endTimer < dur)
        {
            target.rotation = Quaternion.Slerp(intialStart, rot, endTimer / dur);
            yield return null;
            endTimer += Time.deltaTime;
        }
        target.rotation = rot;
        if(!wallGravity)
        {
            isRotate = false;
        }
    }

    IEnumerator RotatePlayerTop()
    {
        yield return StartCoroutine(RotateReset(this.transform, Quaternion.identity, rotateTimeOne));

        isRotate = true;
        float timeElapsed = 0;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 0, 180);

        while(timeElapsed < rotateTimeTwo)
        {

            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed/rotateTimeTwo);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
        isRotate = false;
    }

    IEnumerator RotatePlayerLeft()
    {
        yield return StartCoroutine(RotateReset(this.transform, Quaternion.identity, rotateTimeOne));

        isRotate = true;
        float timeElapsed = 0;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 0, -90);

        while(timeElapsed < rotateTimeTwo)
        {

            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed/rotateTimeTwo);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
        isRotate = false;
    }

    IEnumerator RotatePlayerForward()
    {
        yield return StartCoroutine(RotateReset(this.transform, Quaternion.identity, rotateTimeOne));
        isRotate = true;
        float timeElapsed = 0;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(-90f, 0, 0);

        while(timeElapsed < rotateTimeTwo)
        {

            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed/rotateTimeTwo);
            timeElapsed += Time.deltaTime;
            yield return null;
            
        }
        transform.rotation = targetRotation;
        isRotate = false;
    }

    IEnumerator RotatePlayerBack()
    {
        yield return StartCoroutine(RotateReset(this.transform, Quaternion.identity, rotateTimeOne));;

        isRotate = true;
        float timeElapsed = 0;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(90f, 0, 0);

        while(timeElapsed < rotateTimeTwo)
        {

            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed/rotateTimeTwo);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
        isRotate = false;
    }

    //First mechanic that changes global gravity
    public void ChangeGravity()
    {
        Vector3 direction = cameraAtUse.transform.forward;
        RaycastHit Hit;


        //raycast used for player shooting
        if(Physics.SphereCast(cameraAtUse.transform.position,radius,direction,out Hit, range))
        {
            print ("Objected: " + Hit.transform.gameObject.name);
            cameraMouseControl.currentString = Hit.collider.tag;
            // Hit = Blue Line
            Debug.DrawLine(cameraAtUse.transform.position, Hit.point, Color.blue, 0.5f);

            //condition used for second mechanic
            if (Hit.collider.gameObject.tag != "NoFaux" && Hit.collider.gameObject.tag != "FauxGravity" && !cameraMouseControl.wallTags.Contains<string>(Hit.collider.tag))
            {
                return;
            }
            else
            {
                cameraMouseControl.currentString = Hit.collider.tag;

            }
        }
    }
}
