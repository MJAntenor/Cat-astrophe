using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Camera : MonoBehaviour
{
    // Randomly Positioned Furniture Generator
    public GameObject furniture;
    public GameObject box;
    public GameObject stool;
    public static Camera CAM_Instance;
    public int numFurniturePerRoom = 7;
    public int duration = 1;
    public int magnitude = 10;
    public int minDistance = 2;
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
                screenX = Random.Range(xpos, xpos2);  //-6 17
                pos = new Vector2(screenX, -3.7f);
                Instantiate(furniture, pos, furniture.transform.rotation);
            }
            //generates one furn closer to the start of new level so we cant get fully locked
            safePos = new Vector2(xpos + 6, -3.7f);
            Instantiate(furniture, safePos, furniture.transform.rotation);
            xpos += 30;
            xpos2 += 31;
        }

        // Instantiates two boxes and 1 stool randomly throughout level
        for (int i = 0; i < 2; i++)
        {
            screenX = Random.Range(-6, 105);
            pos = new Vector2(screenX, -3.4f);
            Instantiate(box, pos, box.transform.rotation);
        }
        screenX = Random.Range(-6, 105);
        pos = new Vector2(screenX, -2.6f);
        Instantiate(stool, pos, stool.transform.rotation);
    }
    public void Awake()
    {
        Camera.CAM_Instance = this;
    }
    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        // Makes camera sidescroll
        if (sceneName == "Level1")
        {
            this.transform.position = new Vector3(this.transform.position.x + 0.001f, this.transform.position.y, this.transform.position.z);
        }
        else if (sceneName == "Level2")
        {
            this.transform.position = new Vector3(this.transform.position.x + 0.005f, this.transform.position.y, this.transform.position.z);
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
