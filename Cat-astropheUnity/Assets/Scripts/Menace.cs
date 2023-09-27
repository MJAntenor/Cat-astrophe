using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menace : MonoBehaviour
{
    public Rigidbody2D rigidbodyComponent;
    public Animator anim_menace;
    public SpriteRenderer anim;
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
                anim.flipX = false;
                //Flip Cone Position
                Chase_Cone.CONE_Instance.transform.position = new Vector3(Chase_Cone.CONE_Instance.transform.position.x - 6f, Chase_Cone.CONE_Instance.transform.position.y, Chase_Cone.CONE_Instance.transform.position.z);
            }
            else
            {
                moveSpeed = 5f;
                isFacingRight = true;
                anim.flipX = true;
                //Flip Cone Position
                Chase_Cone.CONE_Instance.transform.position = new Vector3(Chase_Cone.CONE_Instance.transform.position.x + 6f, Chase_Cone.CONE_Instance.transform.position.y, Chase_Cone.CONE_Instance.transform.position.z);
            }
        }
    }

}

