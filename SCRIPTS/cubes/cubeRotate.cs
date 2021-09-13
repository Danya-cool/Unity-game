using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeRotate : MonoBehaviour
{
    public float speed;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, speed);
    }
}
