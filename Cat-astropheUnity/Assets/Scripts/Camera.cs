using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera : MonoBehaviour
{
    // Randomly Positioned Furniture Generator
    public GameObject furniture;
    public int numFurniture = 15;
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
    }

}
