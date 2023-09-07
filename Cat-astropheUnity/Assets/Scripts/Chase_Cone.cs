using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Cone : MonoBehaviour
{
    public static Chase_Cone CONE_Instance;
    public void Awake()
    {
        Chase_Cone.CONE_Instance = this;
    }
}
