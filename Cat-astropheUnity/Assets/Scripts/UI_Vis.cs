using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Vis : MonoBehaviour
{
    public static UI_Vis Visible;
    public void Awake()
    {
        UI_Vis.Visible = this;
    }
    void Update()
    {
        if (Duchess.Instance.isHidden)
        {
            Visible.transform.localScale = new Vector2(0, 0);
        }

        if (!Duchess.Instance.isHidden)
        {
            Visible.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            Debug.Log("VIS BROKEN");
        }
    }
}
