using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Duchess : MonoBehaviour
{
    public static Duchess Instance;
    public Rigidbody2D rigidbodyComponent;
    public AudioSource stomp;
    public bool direction;
    public bool isHidden = false;
    public bool canHide = false;
    public bool passCheckpoint1 = false;
    public bool passCheckpoint2 = false;
    public bool passCheckpoint3 = false;
    public float moveSpeed;
    public float maxPauseTime = 150;
    public float time;

    public void Awake()
    {
        Duchess.Instance = this;
    }

    //Slightly Improved Code (still need a pause)
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow) && !isHidden && canHide)
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
       // this.transform.localScale = new Vector3(this.transform.localScale.x - 0.0001f, this.transform.localScale.y - 0.0001f, this.transform.localScale.z);
        print("YOU'VE HIDDEN!!!");
        isHidden = true;
    }

    // Overlapped w/ Furniture
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Furniture(Clone)")
        {
            canHide = true;
            Debug.Log("CanHide");
        }
        //Menace Teleports when reaching a certain wall
        else if (collision.gameObject.name == "Back_Wall_1_Collider" && !passCheckpoint1)
        {
            Menace.MIN_Instance.transform.position = new Vector3( 45, (float)-0.8, this.transform.position.z);
            Debug.Log("TP Menace LVL 2");
            passCheckpoint1 = true;
        }
        else if (collision.gameObject.name == "Back_Wall_2_Collider" && !passCheckpoint2)
        {
            Menace.MIN_Instance.transform.position = new Vector3(75, (float)-0.8, this.transform.position.z);
            Debug.Log("TP Menace LVL 3");
            passCheckpoint2 = true;
        }
        else if (collision.gameObject.name == "Back_Wall_3_Collider" && !passCheckpoint3)
        {
            Menace.MIN_Instance.transform.position = new Vector3(105, (float)-0.8, this.transform.position.z);
            Debug.Log("TP Menace LVL 4");
            passCheckpoint3 = true;
        }
        //Increase Menace's Speed & plays stomp if Duchess is in POV Cone
        else if (collision.gameObject.name == "POV_Cone" && !isHidden)
        {
            stomp.Play();
            if (Menace.MIN_Instance.isFacingRight)
            {
                Menace.MIN_Instance.moveSpeed = Menace.MIN_Instance.moveChaseSpeed;
                Debug.Log("RUNNN!");
            }
            else if (!Menace.MIN_Instance.isFacingRight )
            {
                Menace.MIN_Instance.moveSpeed = -Menace.MIN_Instance.moveChaseSpeed;
                Debug.Log("RUNNN!");
            }

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Furniture(Clone)")
        {
            canHide = false;
            Debug.Log("Cannot Hide");
        }
        //Reduce Menace Speed if Duchess hides
        else if (collision.gameObject.name == "POV_Cone" || isHidden)
        {
            stomp.Stop();
            if (Menace.MIN_Instance.isFacingRight)
            {
                Menace.MIN_Instance.moveSpeed = moveSpeed;
                Debug.Log("you good!");
            }
            else if (!Menace.MIN_Instance.isFacingRight)
            {
                Menace.MIN_Instance.moveSpeed = -moveSpeed;
                Debug.Log("you good!");
            }
        }
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
