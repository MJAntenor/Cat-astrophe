using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointButton : MonoBehaviour
{
    public static CheckpointButton checkpoint;
    public bool transition = false;
    public float xposHALF = 49.5f;
    public float inputTime = 1.0f;

    // Starts game once button is pressed
    public void Awake()
    {
        CheckpointButton.checkpoint = this;
    }
    public void Update()
    {
        inputTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && inputTime <= 0)
        {
            if (Duchess.Instance.Level2 == false && Duchess.Instance.passhalfLV == false)
            {
                Debug.Log("Checkpoint 1");
                SceneManager.LoadScene("Level1", LoadSceneMode.Single);
            }
            else if (Duchess.Instance.Level2 == false && Duchess.Instance.passhalfLV == true)
            {
                Debug.Log("Checkpoint 1 HALF");
                SceneManager.LoadScene("Level1", LoadSceneMode.Single);
                Debug.Log(Duchess.Instance.passhalfLV);
            }
            else if (Duchess.Instance.Level2 == true && Duchess.Instance.passhalfLV == false)
            {
                Debug.Log("Checkpoint 2");
                SceneManager.LoadScene("Level2", LoadSceneMode.Single);
            }
            else if (Duchess.Instance.Level2 == true && Duchess.Instance.passhalfLV == true)
            {
                Debug.Log("Checkpoint 2 HALF");
                SceneManager.LoadScene("Level2", LoadSceneMode.Single);
            }

            
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && inputTime <= 0)
        {
            Debug.Log("Back to Start");
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
            PlayerPrefs.SetInt("passhalfLV", 0);
        }
    }
}
