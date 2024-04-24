using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Invis : MonoBehaviour
{
    public static UI_Invis Invisible;
    public void Awake()
    {
        UI_Invis.Invisible = this;
    }
    void Update()
    {
        if (Duchess.Instance.isHidden)
        {
            Invisible.transform.localScale = new Vector2(1, 1);
        }
         
        if (!Duchess.Instance.isHidden) 
        {
            Invisible.transform.localScale = new Vector2(0, 0);
        }
        else
        {
            Debug.Log("INVIS BROKEN");
        }
    }
}
