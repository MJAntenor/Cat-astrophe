using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Starts game once button is pressed
    public void OnMouseDown()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
}
