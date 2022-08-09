using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Auhtor:Christopher Cruz
//Script that controls the gravity tool effects
public class GravityTool : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject projectile;
    public Transform attackpoint;
    public float bulletForce;
    public float spread;
    //public AudioSource gunFire;
    //public AudioSource gunReload;
    //public SoundClips soundClips;
    public float flashtime;

    //Author:Christopher Cruz
    //Controls the waepons use: Spawns prefabs and addforce to them, plays audio, plays animation, and other weapon mechanics
    public void ShootTool()
    {
        //ParticleSystem MuzzleFlash = GameObject.Find("GravityTool").GetComponent<ParticleSystem>();
        //gunFire.clip = soundClips.fireGun;
        //gunFire.Play();
        //MuzzleFlash.Play();

        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint;
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(500);
        }

        Vector3 direction = targetPoint - attackpoint.position;

        GameObject currentProjectile = Instantiate(projectile, attackpoint.position, Quaternion.identity);
        currentProjectile.transform.forward = direction.normalized;

        currentProjectile.GetComponent<Rigidbody>().AddForce(direction.normalized * bulletForce, ForceMode.Impulse);
    }
    public IEnumerator muzzleFlash()
    {
        Light muzzleFlash = GameObject.Find("muzzleFlash").GetComponent<Light>();
        muzzleFlash.enabled = true;

        yield return new WaitForSeconds(flashtime);

        muzzleFlash.enabled = false;
    }
}
