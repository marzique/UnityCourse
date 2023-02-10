using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class Movement : MonoBehaviour
{   
    // params
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem thrustParticles1;
    [SerializeField] ParticleSystem thrustParticles2;
    [SerializeField] ParticleSystem thrustParticles3;

    // cache
    Rigidbody rocketRigidBody;
    AudioSource audioSource;

    void Start(){
        rocketRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        ProcessInput();
    }

    void ProcessInput(){
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust(){
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

        void ProcessRotation(){
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StartThrusting()
    {
        rocketRigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!thrustParticles2.isPlaying && !thrustParticles3.isPlaying)
        {
            thrustParticles3.Play();
            thrustParticles2.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!thrustParticles2.isPlaying && !thrustParticles1.isPlaying)
        {
            thrustParticles1.Play();
            thrustParticles2.Play();
        }
    }

    private void StopRotating()
    {
        thrustParticles1.Stop();
        thrustParticles2.Stop();
        thrustParticles3.Stop();
    }

    void ApplyRotation(float rotationThisFrame){
        rocketRigidBody.freezeRotation = true; // take manual control of rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRigidBody.freezeRotation = false; // resume physics control of rotation
    }
}
