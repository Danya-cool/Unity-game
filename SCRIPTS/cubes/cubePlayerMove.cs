using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubePlayerMove : MonoBehaviour
{
    public float maxDistance;
    
    private bool canMove;
    private float leftBroad;
    private float rightBroad;
    private Camera camera;
    public GameObject scroller;
    public bool horizontal;


    private void Start()
    {
        camera = Camera.main;
        if (horizontal)
        {
            leftBroad = transform.position.x - maxDistance;
            rightBroad = transform.position.x + maxDistance;
            scroller.transform.localScale = new Vector2(0.5f + maxDistance * 2, 0.2f);
        }
        else
        {
            leftBroad = transform.position.y - maxDistance;
            rightBroad = transform.position.y + maxDistance;
            scroller.transform.localScale = new Vector2(0.2f, 0.5f + maxDistance * 2);
        }
    }
    public void Select()
    {
        canMove = true;
        StartCoroutine(move());
    }
    public void UnSelect()
    {
        canMove = false;
    }

    IEnumerator move()
    {
        while (canMove)
        {
            Vector3 tr = transform.position;
            if (horizontal)
            {
                float x_MousePos = Mathf.Clamp(camera.ScreenToWorldPoint(Input.mousePosition).x, leftBroad, rightBroad);
                transform.position = new Vector3(x_MousePos, tr.y, tr.z);
            }
            else
            {
                float y_MousePos = Mathf.Clamp(camera.ScreenToWorldPoint(Input.mousePosition).y, leftBroad, rightBroad);
                transform.position = new Vector3(tr.x, y_MousePos, tr.z);
            }
            yield return 0;
        }
    }
}
