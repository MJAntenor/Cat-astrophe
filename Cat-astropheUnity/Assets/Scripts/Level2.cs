using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2 : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Duchess")
        {
            Debug.Log("Transition");
            SceneManager.LoadScene("Level2", LoadSceneMode.Single);
        }
    }
}
