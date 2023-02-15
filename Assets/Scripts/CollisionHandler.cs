using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    // params
    [SerializeField] float levelStartDelay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    // cache
    AudioSource audioSource;

    // state
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        RespondToDebugKeys();
    }

    void RespondToDebugKeys() {
        if (Input.GetKey(KeyCode.L)) {
            LoadNextLevel();
        } else if (Input.GetKey(KeyCode.C)) {
            collisionDisabled = !collisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision other) {

        if (isTransitioning || collisionDisabled) {
            return;
        }

        switch (other.gameObject.tag) {
            case "Friendly":
                FriendlyCollision();
                break;
            case "Finish":
                FinishLevel();
                break;
            default:
                FailLevel();
                break;
        }
    }

    void FailLevel() {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);

        crashParticles.Play();

        GetComponent<Movement>().enabled = false;

        Invoke("RestartLevel", levelStartDelay);
    }

    void FriendlyCollision() {
        Debug.Log("This is friendly");
    }

    void FinishLevel() {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(successSound);

        successParticles.Play();

        GetComponent<Movement>().enabled = false;
        
        Invoke("LoadNextLevel", levelStartDelay);
    }

    void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    
    void RestartLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}   
