using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    public static Furniture FURN_Instance;
    public void Awake()
    {
        Furniture.FURN_Instance = this;
    }
}
