using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointButton : MonoBehaviour
{
    public static CheckpointButton checkpoint;
    public bool hasPassedL2;
    // Starts game once button is pressed
    public void Awake()
    {
        CheckpointButton.checkpoint = this;
    }
    public void OnMouseDown()
    {
        if (Menace.MIN_Instance.moveSpeed == 10f || Menace.MIN_Instance.moveSpeed == -10f)
        {
            Debug.Log("Checkpoint 2");
            SceneManager.LoadScene("Level2", LoadSceneMode.Single);
        }
        {
            Debug.Log("Checkpoint 1");
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
    }
}
