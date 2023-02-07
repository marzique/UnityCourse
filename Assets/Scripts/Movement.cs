using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rocketRigidBody;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;

    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rocketRigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rocketRigidBody.freezeRotation = true; // take manual control of rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRigidBody.freezeRotation = false; // resume physics control of rotation
    }
}
