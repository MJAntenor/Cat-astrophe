using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public float inputTime = 2.0f;
    
    // Starts game once button is pressed
    public void Update()
    {
        inputTime -= Time.deltaTime;

        if (Input.anyKey && inputTime <= 0)
        {
            PlayerPrefs.SetInt("passhalfLV", 0);
            Debug.Log("Start Game");
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }
    }
}