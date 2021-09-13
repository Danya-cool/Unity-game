using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeController : MonoBehaviour
{
    public bool isReflectable;
    
    public void setReflectable()
    {
        isReflectable = true;
    }

    private void Start()
    {
        if (isReflectable)
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }
}
