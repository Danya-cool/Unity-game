using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attentionObj : MonoBehaviour
{
    private float firstScale;
    public float step;
    private bool increas = true;
    public float maxDifference;

    private void Start()
    {
        firstScale = transform.localScale.x;
    }
    private void FixedUpdate()
    {
        if (increas)
        {
            transform.localScale *= 1 + step;
            if (transform.localScale.x >= firstScale + maxDifference)
            {
                increas = false;
            }
        }
        else
        {
            transform.localScale *= 1 - step;
            if (transform.localScale.x <= firstScale - maxDifference)
            {
                increas = true;
            }
        }
    }
}
