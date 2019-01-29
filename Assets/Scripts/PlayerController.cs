using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    [Header ("Input")]
    public string moveInputAxis = "Vertical";
    public string turnInputAxis = "Horizontal";
    public string turretInputAxis = "TurretRotation";
    public KeyCode shootKey;
    public KeyCode changeBullet;
    [Header("Movement variables")]
    public float moveSpeed = 15f;
    public float rotationSpeed = 100f;
    public float turretRotationSpeed = 50f;
    [Header("Extra")]
    public GameObject[] Bullets;
    public Transform barrel;
    public Transform turret;
    public float shootForce;
    public float recoilForce;
    public int chosenBullet;


    void Start () {
        rb = GetComponent<Rigidbody>();		
	}

	void Update () {
        Inputs();

	}

    private void Inputs()
    {
        Move();
        Turn();
        TurretRotate();
        if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }
        else if (Input.GetKeyDown(changeBullet))
        {
            ChangeBullet();
        }

    }

    private void ChangeBullet()
    {
        // safety check whether its safe to traverse forward in array
       if(chosenBullet<Bullets.Length - 1)
        {
            chosenBullet += 1;
        }
       // if not, reset to beginning
        else
        {
            chosenBullet = 0;
        }

    }

    private void TurretRotate()
   {
        float turretAxis = Input.GetAxis(turretInputAxis);
        turret.Rotate(0, turretAxis * turretRotationSpeed * Time.deltaTime, 0);
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(Bullets[chosenBullet], barrel.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(barrel.forward * shootForce);
        // rotate the bullet to match up to the turret. No more sideways bullets
        Quaternion q = Quaternion.FromToRotation(Vector3.up, barrel.forward);
        bullet.transform.rotation = q * bullet.transform.rotation;
        // add recoil force to whatever direction the barrel is currently pointing
        rb.AddForce(barrel.forward * -recoilForce, ForceMode.Force);
    }

    private void Move()
    {
        float moveAxis = Input.GetAxis(moveInputAxis);
        rb.AddForce(transform.forward * moveAxis * moveSpeed, ForceMode.Force);

    }
    private void Turn()
    {
        float turnAxis = Input.GetAxis(turnInputAxis);
        transform.Rotate(0, turnAxis * rotationSpeed * Time.deltaTime, 0); // rotate on Y axis only

    }
}
