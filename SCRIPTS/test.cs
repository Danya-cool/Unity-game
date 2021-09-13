using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Image background;
    public Color color1 = new Color(201, 243, 171);
    public Color color2 = new Color(243, 232, 171);
    public Color color3 = new Color(144, 255, 187);
    public Color color4 = new Color(201, 255, 204);
    public Color normal = new Color(0.5f, 0.5f, 0.5f);

    public void SetColor(Color color)
    {
        background.color = color;
    }
    public void s1()
    {
        SetColor(color1);
    }
    public void s2()
    {
        SetColor(color2);
    }
    public void s3()
    {
        SetColor(color3);
    }
    public void s4()
    {
        SetColor(color4);
    }
    public void snormal()
    {
        SetColor(normal);
    }
}
