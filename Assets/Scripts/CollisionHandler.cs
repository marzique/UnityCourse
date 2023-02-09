using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    // params
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] float levelStartDelay = 1f;

    // cache
    AudioSource audioSource;

    // state
    bool playedFinalSound = false;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {

        if (playedFinalSound) {
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
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        playedFinalSound = true;

        GetComponent<Movement>().enabled = false;

        Invoke("RestartLevel", levelStartDelay);
    }

    void FriendlyCollision() {
        Debug.Log("This is friendly");
    }

    void FinishLevel() {
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        playedFinalSound = true;

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
