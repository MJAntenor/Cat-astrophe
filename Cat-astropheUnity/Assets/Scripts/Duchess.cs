using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.SceneManagement;

public class Duchess : MonoBehaviour
{
    public static Duchess Instance;
    public Rigidbody2D rigidbodyComponent;
    public SpriteRenderer anim;
    public AudioSource stomp;
    public Animator anim_dutchess;
    private bool direction;
    public bool isHidden = false;
    public bool canHide = false;
    public bool canUnHide = true;
    public bool passCheckpoint1 = false;
    public bool passCheckpoint2 = false;
    public bool passCheckpoint3 = false;
    public float hideTime = 1.0f;
    public float moveSpeed;
    //creates states for animations, when one state is on, it will play an animation.
    public enum PlayerStates
    {
        WALK,
        HIDE,
    }
    PlayerStates m_currentState;
    PlayerStates CurrentStates
    {
        set 
        { 
            m_currentState = value;
            switch (m_currentState)
            {
                case PlayerStates.WALK:
                   anim_dutchess.Play("DutchessWalk");
                    break;
                case PlayerStates.HIDE:
                   anim_dutchess.Play("DutchessHide");
                    break;
            }
        }
    }

    // Singleton
    public void Awake()
    {
        Duchess.Instance = this;
    }

    // Controls Duchess Movement & Hide Mechanics
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow) && !isHidden && canHide)
            {
                Hide();
                canUnHide = false;
            }
            else if (canUnHide)
            {
                isHidden = false;
                direction = false;
                Movement(direction);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) && canUnHide)
        {

            isHidden = false;
            direction = true;
            Movement(direction);
          
        }
        else if (isHidden && !canUnHide)
        {
            if (hideTime <= 0)
            {
                canUnHide = true;
                Debug.Log("the time has come.");
                hideTime = 1.0f;
            }
            else
            {
                hideTime -= Time.deltaTime;
                Debug.Log(hideTime);
            }
        }
    }    
    // Duchess movement based on direction
    public void Movement(bool direction)
    {
        // changes play anim state to walk
        CurrentStates = PlayerStates.WALK;
        if (direction)
        {
            anim.flipX = false;
            rigidbodyComponent.velocity = new Vector2(moveSpeed, 0f);
            print("Duchess Moved Right!");
        }
        else
        {
            anim.flipX = true;
            rigidbodyComponent.velocity = new Vector2(-moveSpeed, 0f);
            print("Duchess Moved Left!");
        }
    }

    // Marks that you've successfully hidden
    public void Hide()
    {
        // changes play anim state to hide
        CurrentStates = PlayerStates.HIDE;
        print("Hiding!!!!");
        isHidden = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if Overlapped w/ Furniture to see if Duchess can hide
        if (collision.gameObject.name == "Furniture(Clone)")
        {
            canHide = true;
            Debug.Log("CanHide");
        }

        // Menace Teleports when reaching a certain wall
        // Will change to loop/array situation also make menace faster each checkpoint
        else if (collision.gameObject.name == "Back_Wall_1_Collider" && !passCheckpoint1)
        {
            Menace.MIN_Instance.transform.position = new Vector3( 45, (float)-0.71, this.transform.position.z);
            Debug.Log("TP Menace LVL 2");
            passCheckpoint1 = true;
        }
        else if (collision.gameObject.name == "Back_Wall_2_Collider" && !passCheckpoint2)
        {
            Menace.MIN_Instance.transform.position = new Vector3(75, (float)-0.71, this.transform.position.z);
            Debug.Log("TP Menace LVL 3");
            passCheckpoint2 = true;
        }
        else if (collision.gameObject.name == "Back_Wall_3_Collider" && !passCheckpoint3)
        {
            Menace.MIN_Instance.transform.position = new Vector3(105, (float)-0.71, this.transform.position.z);
            Debug.Log("TP Menace LVL 4");
            passCheckpoint3 = true;
        }
        // Increase Menace's Speed & plays stomp if Duchess is in POV Cone
        else if (collision.gameObject.name == "POV_Cone" && !isHidden)
        {
            Camera.CAM_Instance.Shake();
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
        // Game Over if Duchess and Menace collide
        else if (collision.gameObject.name == "Menace_Collider" && !Duchess.Instance.isHidden)
        {
            Debug.Log("GameOver!");
            Duchess.Instance.Caught();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        // Makes sure you can't hide if you're not overlapped with furniture
        if (collision.gameObject.name == "Furniture(Clone)")
        {
            canHide = false;
            Debug.Log("Cannot Hide");
        }
        // Reduce Menace Speed if Duchess hides
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

    public void Caught()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

}