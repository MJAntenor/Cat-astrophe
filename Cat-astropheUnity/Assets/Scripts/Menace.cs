using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menace : MonoBehaviour
{
    public Rigidbody2D rigidbodyComponent;
    public float moveSpeed;
    public bool isFacingRight = true;

    void Update()
    {
        rigidbodyComponent.velocity = new Vector2(moveSpeed, 0f);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log(collision.gameObject.name);
            if (isFacingRight)
            {
                moveSpeed = -5f;
                isFacingRight = false;
            }
            else
            {
                moveSpeed = 5f;
                isFacingRight = true;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DuchessCollider") // && !isHidden
        {
            Debug.Log("GameOver!");
        }
    }

}

