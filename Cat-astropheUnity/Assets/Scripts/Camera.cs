using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera : MonoBehaviour
{
    // Randomly Positioned Furniture Generator
    public GameObject furniture;
    public int numFurniture = 15;
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
            pos = new Vector2(screenX, furniture.transform.position.y);
            
            Instantiate(furniture, pos, furniture.transform.rotation);
            if (IsTooClose(pos))
            {
                Destroy(furniture); // Destroy the object if it's too close
                i--; // Retry the spawn
                Debug.Log("Too Close!");
            }
        }
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

}
