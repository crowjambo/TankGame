using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    [Header ("Input axis names")]
    public string moveInputAxis;
    public string turnInputAxis;
    [Header("Movement variables")]
    public float moveSpeed = 15f;
    public float rotationSpeed = 100f;

    private void Awake()  {
        if (moveInputAxis == null)
        {
            moveInputAxis = "Vertical";
        }
        else if(turnInputAxis == null)
        {
            turnInputAxis = "Horizontal";
        }
    }

    void Start () {
        rb = GetComponent<Rigidbody>();		
	}

	void Update () {
        Move();
        Turn();
   
	}

    private void Move() {
        float moveAxis = Input.GetAxis(moveInputAxis);
        print(moveAxis);
        rb.AddForce(transform.forward * moveAxis * moveSpeed, ForceMode.Force);

    }
    private void Turn()  {
        float turnAxis = Input.GetAxis(turnInputAxis);
        print(turnAxis);
        transform.Rotate(0, turnAxis * rotationSpeed * Time.deltaTime, 0); // rotate on Y axis only

    }
}
