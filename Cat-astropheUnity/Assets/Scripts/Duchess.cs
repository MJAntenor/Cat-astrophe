using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Duchess : MonoBehaviour
{
    public Rigidbody2D rigidbodyComponent;
    public bool direction;
    public bool isHidden = false;
    public float moveSpeed;
    public float maxPauseTime = 150;
    public float time;

    
    //Slightly Improved Code (still need a pause)
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow) && !isHidden)
            {
                Hide();
            }
            else if (!isHidden)
            {
                direction = false;
                Movement();
            }

            else if (Input.GetKey(KeyCode.RightArrow) && isHidden)
            {
                isHidden = false;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!isHidden)
            {
                direction = true;
                Movement();
            }
        }
    }
    public void Movement()
    {
        if (direction)
        {
            //rigidbodyComponent.MovePosition(new Vector2(moveSpeed.x * Time.deltaTime, 0));
            rigidbodyComponent.velocity = new Vector2(moveSpeed, 0f);
            print("Duchess Moved Right!");
        }
        else
        {
            //rigidbodyComponent.MovePosition(new Vector2(-moveSpeed.x * Time.deltaTime, 0));
            rigidbodyComponent.velocity = new Vector2(-moveSpeed, 0f);
            print("Duchess Moved Left!");
        }
    }
    public void Hide()
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x - 0.0001f, this.transform.localScale.y - 0.0001f, this.transform.localScale.z);
        print("YOU'VE HIDDEN!!!");
        isHidden = true;
    }
}
// Past Update Code (just for backup)
/*    
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow) && !isHidden)
            {
                Hide();
            }
            else if (!isHidden)
            {
                direction = false;
                Movement();
            }

            else if (Input.GetKey(KeyCode.RightArrow) && isHidden)
            {
                isHidden = false;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!isHidden)
            {
                direction = true;
                Movement();
            }
        }
    }
*/
