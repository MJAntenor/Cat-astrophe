using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menace : MonoBehaviour
{
    public Rigidbody2D rigidbodyComponent;
    public Animator anim_menace;
    public SpriteRenderer anim;
    public static Menace MIN_Instance;
    public float moveSpeed = 5f;
    public float moveChaseSpeed;
    public bool isFacingRight = true;

    // Singleton
    public void Awake()
    {
        Menace.MIN_Instance = this;
    }

    // Set Menace Movement
    void Update()
    {
        rigidbodyComponent.velocity = new Vector2(moveSpeed, 0f);
    }

    private void Start()
    {
    }

    // Changes direction of Menace if hits wall
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (collision.gameObject.tag == "Wall")
        {
            if (isFacingRight)
            {
                if (sceneName == "Level1")
                {
                    moveSpeed = -5f;
                }
                if (sceneName == "Level2" || sceneName == "GameOver")
                {
                    moveSpeed = -10f;
                }
                isFacingRight = false;
                anim.flipX = false;
                //Flip Cone Position
                Chase_Cone.CONE_Instance.transform.position = new Vector3(Chase_Cone.CONE_Instance.transform.position.x - 6f, Chase_Cone.CONE_Instance.transform.position.y, Chase_Cone.CONE_Instance.transform.position.z);
            }
            else
            {
                if (sceneName == "Level1")
                {
                    moveSpeed = 5f;
                }
                else if (sceneName == "Level2")
                {
                    moveSpeed = 10f;
                }
                isFacingRight = true;
                anim.flipX = true;
                //Flip Cone Position
                Chase_Cone.CONE_Instance.transform.position = new Vector3(Chase_Cone.CONE_Instance.transform.position.x + 6f, Chase_Cone.CONE_Instance.transform.position.y, Chase_Cone.CONE_Instance.transform.position.z);
            }
        }
    }

}

