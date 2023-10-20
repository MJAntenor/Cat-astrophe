using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Cam : MonoBehaviour
{
    // Randomly Positioned Furniture Generator
    public GameObject furniture;
    public GameObject box;
    public GameObject stool;
    public static Cam CAM_Instance;
    public int numFurniturePerRoom = 7;
    public int duration = 1;
    public int magnitude = 10;
    public int minDistance = 2;
    public float level1speed; //= .002f; //.03 
    public float level2speed; //= .005f; //.08
    public bool isBehind = false;
    float screenX;
    Vector2 pos;
    Vector2 safePos;

    private void Start()
    {
        // Depending on scene has different number of furniture per room
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Level1")
        {
            numFurniturePerRoom = 6;
        }
        else if (sceneName == "Level2")
        {
            numFurniturePerRoom = 4;
        }

        // Randomly generates position on x-axis and creates furniture; broken into rooms so its fair
        int xpos = -6;
        int xpos2 = 17;
        for (int i = 0; i < 4; i++)
        { 
          // "numFurn - i" makes it harder for later rooms!
            for (int u = 0; u < numFurniturePerRoom - i; u++)
            { 
                pos = ChoosePosition(xpos, xpos2);
                Instantiate(furniture, pos, furniture.transform.rotation);
            }
            //generates one furn closer to the start of new level so we cant get fully locked
            safePos = new Vector2(xpos + 6, -3.7f);
            Instantiate(furniture, safePos, furniture.transform.rotation);
            xpos += 30;
            xpos2 += 31;
        }

        // Instantiates two boxes and 1 stool randomly throughout level
        for (int i = 0; i < 3; i++)
        {
            screenX = Random.Range(-6, 105);
            pos = new Vector2(screenX, -3.5f);
            Instantiate(box, pos, box.transform.rotation);
        }
        screenX = Random.Range(-6, 105);
        pos = new Vector2(screenX, -2.9f);
        Instantiate(stool, pos, stool.transform.rotation);
    }

    private Vector2 ChoosePosition(int xpos, int xpos2)
    {
        screenX = Random.Range(xpos, xpos2);
        Vector2 returnValue = new Vector2 (screenX, -3.7f);
        for (int i = 0; i < 3; i++)
        {
            Collider[] array = Physics.OverlapBox(returnValue, Vector3.one);
            if (array.Length > 0)
            {
                screenX = Random.Range(xpos, xpos2);
                
            }
            else
            {

                returnValue = new Vector2 (screenX,-3.7f);

                return returnValue;
            }
            
        }

        return returnValue;

        
    }

    public void Awake()
    {
        Cam.CAM_Instance = this;
        Cam.CAM_Instance.transform.position = new Vector3 (Duchess.Instance.transform.position.x, 0.3f, -10);
    }
    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        // Makes camera sidescroll
        if (sceneName == "Level1")
        {
            this.transform.position = new Vector3(this.transform.position.x + level1speed, this.transform.position.y, this.transform.position.z);
        }
        else if (sceneName == "Level2")
        {
            this.transform.position = new Vector3(this.transform.position.x + level2speed, this.transform.position.y, this.transform.position.z);
        }
        // Pushes camera if Duchess gets too far ahead
        if (Duchess.Instance.transform.position.x > this.transform.position.x + 7)
        {
            this.transform.position = new Vector3(this.transform.position.x + 0.035f, this.transform.position.y, this.transform.position.z);
        }
        if (Duchess.Instance.transform.position.x < this.transform.position.x - 13)
        {
            Debug.Log("Game Over bc Wall");
            Duchess.Instance.Caught();
        }
        if (Duchess.Instance.passendingwall == true)
        {
            this.transform.position = new Vector3 (Duchess.Instance.transform.position.x, this.transform.position.y, -10);
            
        }
    }

   public IEnumerator Shake() //DONT WORK RN
   {
       Vector3 orignalPosition = transform.position;
       float elapsed = 0f;

       while (elapsed < duration)
       {
           float x = Random.Range(-1f, 1f) * magnitude;
           float y = Random.Range(-1f, 1f) * magnitude;

           transform.position = new Vector3(x, y, -10f);
           elapsed += Time.deltaTime;
           yield return 0;
            Debug.Log("SHAKE");
       }
       transform.position = orignalPosition;
   }
}
