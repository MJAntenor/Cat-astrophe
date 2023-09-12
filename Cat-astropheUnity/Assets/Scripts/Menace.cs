using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menace : MonoBehaviour
{
    public Rigidbody2D rigidbodyComponent;
    public static Menace MIN_Instance;
    public float moveSpeed;
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

    // Changes direction of Menace if hits wall
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log(collision.gameObject.name);
            if (isFacingRight)
            {
                moveSpeed = -5f;
                isFacingRight = false;
                //Flip Cone Position
                Chase_Cone.CONE_Instance.transform.position = new Vector3 (this.transform.position.x - 4, this.transform.position.y, this.transform.position.z);
            }
            else
            {
                moveSpeed = 5f;
                isFacingRight = true;
                //Flip Cone Position
                Chase_Cone.CONE_Instance.transform.position = new Vector3(this.transform.position.x + 4, this.transform.position.y, this.transform.position.z);
            }
        }
    }

    // Game Over if Duchess and Menace collide
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DuchessCollider" && !Duchess.Instance.isHidden)
        {
            Debug.Log("GameOver!");
        }
    }

}

