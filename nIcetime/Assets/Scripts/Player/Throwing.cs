using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public GameObject altobjectToThrow;

    [Header("Settings")]
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    [Header("AltThrowing")]
    public KeyCode altthrowKey = KeyCode.Mouse1;
    public float altthrowForce;
    public float altthrowUpwardForce;

    [Header("Audio")]
    public AudioSource Throw1;
    public AudioSource Throw2;
    public AudioSource Throw3;

    bool readyToThrow;

    // Start is called before the first frame update
    void Start()
    {
        readyToThrow = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(throwKey) && readyToThrow && !PauseMenu.GameIsPaused)
        {
            Throw();
        }
    }

    public void Throw()
    {
        readyToThrow = false;

        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0) Throw1.Play();
        else if (randomNumber == 1) Throw2.Play();
        else Throw3.Play();

        //Object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        //Calculate Direction
        Vector3 forceDirection = cam.transform.forward;
        RaycastHit hit;
        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f)){
            forceDirection = (hit.point - attackPoint.position).normalized;


        }

        //add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);


        //CoolDown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    // Explosive snowballs
    public void altThrow()
    {
        readyToThrow = false;

        //Object to throw
        GameObject projectile = Instantiate(altobjectToThrow, attackPoint.position, cam.rotation);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        //Calculate Direction
        Vector3 forceDirection = cam.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        //add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        //CoolDown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }


}
