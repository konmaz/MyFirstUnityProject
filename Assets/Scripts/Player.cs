using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    private bool JumpKeyWasPressed;
    private float HorizontalInput;
    private Rigidbody RigidBodyComponent;
    private int superJumpRemaining = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        RigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //EVENT Check/ Space Key Pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpKeyWasPressed = true;
        }

        HorizontalInput = Input.GetAxis("Horizontal");

    }

    //FixedUpdate is called every physic update (Every 100Hz)
    private void FixedUpdate()
    {
        RigidBodyComponent.velocity = new Vector3(HorizontalInput, RigidBodyComponent.velocity.y, RigidBodyComponent.velocity.z);
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
            return;


        if (JumpKeyWasPressed)
        {
            float jumpPower = 5f;
            if (superJumpRemaining >0)
            {
                jumpPower *= 2;
                superJumpRemaining--;
            }
            RigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            JumpKeyWasPressed = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJumpRemaining++;
        }
    }

}
