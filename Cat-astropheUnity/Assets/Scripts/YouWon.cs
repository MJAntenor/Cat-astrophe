using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWo : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Duchess")
        {
            Debug.Log("Transition");
            SceneManager.LoadScene("YouWon", LoadSceneMode.Single);
        }
    }
}
