using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "Friendly":
                FriendlyCollision();
                break;
            case "Finish":
                FinishCollision();
                break;
            default:
                RestartLevel();
                break;
        }
    }

    void FriendlyCollision() {
        Debug.Log("This is friendly");
    }

    void FinishCollision() {
        Debug.Log("This is finish");
    }

    void RestartLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}   
