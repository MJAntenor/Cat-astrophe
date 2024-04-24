using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

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
    public bool passendingwall = false;
    public bool Level2;
    public bool passhalfLV;
    private float noscaleX = 0, noscaleY = 0, noscaleZ = 0, scaleX = 1, scaleY = 1, scaleZ = 1;
    public float hideTime = 1.0f;
    public float moveSpeed;
    //creates states for animations, when one state is on, it will play an animation.
    public enum PlayerStates
    {
        WALK,
        HIDE,
        IDLE,
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
                case PlayerStates.IDLE:
                    anim_dutchess.Play("DuchessIdle");
                    break;
            }
        }
    }

    // Singleton
    public void Awake()
    {
        Duchess.Instance = this;
        if (PlayerPrefs.GetInt("passhalfLV") == 1)
        { 
            TransportCheckpoint();
        }

        //Debug.Log("d:"+PlayerPrefs.GetInt("passhalfLV");
    }

    // Controls Duchess Movement & Hide Mechanics
    void Update() 
    {
        
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && canUnHide)
        {
           isHidden = false;
           direction = false;
           Movement(direction);
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Hide();
            canUnHide = false;
        }

        else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && canUnHide)
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
                hideTime = 1.0f;
            }
            else
            {
                hideTime -= Time.deltaTime;
            }
        }

        else if (!isHidden)
        {

            //Plays Idle Anim when no input
            CurrentStates = PlayerStates.IDLE;
        }

    }
    // Duchess movement based on direction
    public void Movement(bool direction)
    {
        Scene currentScene = SceneManager.GetActiveScene ();
        string sceneName = currentScene.name;

        if (sceneName == "Level1")
        {
            //moveSpeed = 5f;
            moveSpeed = 12f;
        }
        else if (sceneName == "Level2" && passendingwall != true)
        {
            moveSpeed = 10f;
        }


        // changes play anim state to walk
        CurrentStates = PlayerStates.WALK;
        if (direction)
        {
            anim.flipX = false;
            rigidbodyComponent.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            anim.flipX = true;
            rigidbodyComponent.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    // Marks that you've successfully hidden
    public void Hide()
    {
        // changes play anim state to hide
        CurrentStates = PlayerStates.HIDE;
        isHidden = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if Overlapped w/ Furniture to see if Duchess can hide
        if (collision.gameObject.name == "Furniture(Clone)" || collision.gameObject.name == "Stool(Clone)" || collision.gameObject.name == "Box(Clone)")
        {
            canHide = true;
            Debug.Log("CanHide");
        }
        else if (collision.gameObject.name == "Back_Wall_0_Collider")
        {
            Duchess.Instance.Level2 = true;
            Duchess.Instance.passhalfLV = false;
        }
        // Menace Teleports when reaching a certain wall
        // Will change to loop/array situation also make menace faster each checkpoint
        else if (collision.gameObject.name == "Back_Wall_1_Collider" && !passCheckpoint1)
        {
            Menace.MIN_Instance.transform.position = new Vector3( 45, (float)-3.640281, this.transform.position.z);
            //Debug.Log("TP Menace LVL 2");
            passCheckpoint1 = true;
        }
        else if (collision.gameObject.name == "Back_Wall_2_Collider" && !passCheckpoint2)
        {
            Menace.MIN_Instance.transform.position = new Vector3(75, (float)-3.640281, this.transform.position.z);
            //Debug.Log("TP Menace LVL 3");
            passCheckpoint2 = true;
        }
        else if (collision.gameObject.name == "Back_Wall_3_Collider" && !passCheckpoint3)
        {
            Menace.MIN_Instance.transform.position = new Vector3(105, (float)-3.640281, this.transform.position.z);
            //Debug.Log("TP Menace LVL 4");
            passCheckpoint3 = true;
        }
        else if (collision.gameObject.name == "CheckLV1")
        {
            Debug.Log("Passed Half");
            passhalfLV = true;
            PlayerPrefs.SetInt("passhalfLV", 1);
        }
        else if (collision.gameObject.name == "ending wall collider")
        {
            moveSpeed = 4f;
            passendingwall = true;
        }
        // Increase Menace's Speed & plays stomp if Duchess is in POV Cone
        else if (collision.gameObject.name == "POV_Cone" && !isHidden)
        {
            Cam.CAM_Instance.Shake();
            stomp.Play();
            if (Menace.MIN_Instance.isFacingRight)
            {
                Menace.MIN_Instance.moveSpeed = Menace.MIN_Instance.moveChaseSpeed;
                Debug.Log("RUNNN!");
            }
            else if (!Menace.MIN_Instance.isFacingRight)
            {
                Menace.MIN_Instance.moveSpeed = -Menace.MIN_Instance.moveChaseSpeed;
                Debug.Log("RUNNN!");
            }
        }
        // Game Over if Duchess and Menace collide
        else if (collision.gameObject.name == "Menace_Collider" && !Duchess.Instance.isHidden)
        {
            Duchess.Instance.Caught();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        // Makes sure you can't hide if you're not overlapped with furniture
        if (collision.gameObject.name == "Furniture(Clone)")
        {
            canHide = false;
        }
        // Reduce Menace Speed if Duchess hides
        else if (isHidden || collision.gameObject.name == "POV_Cone")
        {
            stomp.Stop();
            if (Menace.MIN_Instance.isFacingRight)
            {
                Menace.MIN_Instance.moveSpeed = moveSpeed;
                //Debug.Log("you good!");
            }
            else if (!Menace.MIN_Instance.isFacingRight)
            {
                Menace.MIN_Instance.moveSpeed = -moveSpeed;
                //Debug.Log("you good!");
            }
        }
    }

    public void Caught()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    public void TransportCheckpoint()
    {
        Duchess.Instance.transform.position = new Vector2 (49.714f, -3.72f);
    }    
}