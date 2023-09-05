using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera : MonoBehaviour
{
    public GameObject furniture;
    public int numFurniture = 10;
    float screenX;
    Vector2 pos;
    private void Start()
    {
        for(int i = 0; i < numFurniture; i++)
        {
            screenX = Random.Range(-12, 60);
            pos = new Vector2(screenX, furniture.transform.position.y);

            Instantiate(furniture, pos, furniture.transform.rotation);

        }
    }

    private void Update()
    { 
        //makes camera sidescroll
        this.transform.position = new Vector3(this.transform.position.x + 0.001f, this.transform.position.y, this.transform.position.z); 
    }

}
