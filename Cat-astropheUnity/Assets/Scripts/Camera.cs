using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera : MonoBehaviour
{
    // Randomly Positioned Furniture Generator
    public GameObject furniture;
    public static Camera CAM_Instance;
    public int numFurniture = 15;
    public int duration = 1;
    public int magnitude = 10;
    public int minDistance = 2;
    public bool isBehind = false;
    float screenX;
    Vector2 pos;
    private void Start()
    {
        // Randomly generates position on x-axis and creates furniture
        for(int i = 0; i < numFurniture; i++)
        {
            screenX = Random.Range(-6, 105);
            pos = new Vector2(screenX, -3.7f);
            
            Instantiate(furniture, pos, furniture.transform.rotation);
            if (IsTooClose(pos))
            {
                Destroy(furniture); // Destroy the object if it's too close
                i--; // Retry the spawn
                Debug.Log("Too Close!");
            }
        }
    }
    public void Awake()
    {
        Camera.CAM_Instance = this;
    }
    private void Update()
    {
        // Makes camera sidescroll
        this.transform.position = new Vector3(this.transform.position.x + 0.001f, this.transform.position.y, this.transform.position.z);
        // Pushes camera if Duchess gets too far ahead
        if (Duchess.Instance.transform.position.x > this.transform.position.x + 7)
        {
            this.transform.position = new Vector3(this.transform.position.x + 0.010f, this.transform.position.y, this.transform.position.z);
        }
        if (Duchess.Instance.transform.position.x < this.transform.position.x - 13)
        {
            Debug.Log("Game Over bc Wall");
            Duchess.Instance.Caught();
        }
    }
    public bool IsTooClose(Vector2 pos) //trying to figure out how to respawn furniture if its too close to another one
    {
        Collider[] colliders = Physics.OverlapSphere(pos, minDistance);

        foreach (Collider collider in colliders)
        {
            return true; // Object is too close to another prefab
        }

        return false; // Object is not too close
    }

   public IEnumerator Shake()
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
