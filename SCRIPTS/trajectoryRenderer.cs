using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trajectoryRenderer : MonoBehaviour
{

    public GameObject linePref;
    public Transform firePoint;
    public GameObject dottedLine;

    private List<GameObject> allLines = new List<GameObject>();
    
    public RaycastHit2D lastPoint { get; set; }

    public Color blockColor = Color.white;
    public float laserLengthOffset = 1.01f;

    public void removeAllLines()
    {
        if (allLines != null)
        {
            foreach (var item in allLines)
            {
                Destroy(item);

            }
        }
    }
    public void showTRJ(GameObject endEffect)
    {
        //удаление всех прежних линий
        removeAllLines();
        allLines.Clear();
        //coхранение всех точек лазера


        GameObject newLine = Instantiate(linePref);
        allLines.Add(newLine);
        LineRenderer newLineRenderer = newLine.GetComponent<LineRenderer>();
        newLineRenderer.SetPosition(0, firePoint.position); //1-я точка
        Ray2D ray2D = new Ray2D(firePoint.position, firePoint.right); //луч от первой точке, чтобы найти вторую
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right); //ищем во всём встречающемся блок

        newLineRenderer.SetPosition(1, hitInfo.point*laserLengthOffset); //устанавливаем 2-ю точку
        endEffect.transform.position = hitInfo.point * laserLengthOffset;

        bool laserMustReflect = true;

        

        if (!hitInfo.collider.CompareTag("Block"))
        {
            laserMustReflect = false;
            lastPoint = hitInfo;
        }
        else if (!hitInfo.collider.gameObject.GetComponent<cubeController>().isReflectable)
        {
            laserMustReflect = false;
            lastPoint = hitInfo;
        }
        

        //смотрим на последующие отражения
        while (laserMustReflect)
        {
            //в каком направлении пускать следующий луч
            Vector2 inDirection = Vector2.Reflect(ray2D.direction, hitInfo.normal);

            RaycastHit2D newHitInfo = Physics2D.Raycast(hitInfo.point + inDirection * 0.1f, inDirection);
            

            newLine = Instantiate(linePref);
            allLines.Add(newLine);
            newLineRenderer = newLine.GetComponent<LineRenderer>();

            newLineRenderer.SetPosition(0, hitInfo.point);
            newLineRenderer.SetPosition(1, newHitInfo.point*laserLengthOffset);
            endEffect.transform.position = newHitInfo.point * laserLengthOffset;

            ray2D = new Ray2D(hitInfo.point + inDirection * 0.1f, inDirection);
            hitInfo = newHitInfo;



            if (!hitInfo.collider.CompareTag("Block"))
            {
                laserMustReflect = false;
                lastPoint = hitInfo;
                break;
            }

            if (!hitInfo.collider.gameObject.GetComponent<cubeController>().isReflectable)
            {
                laserMustReflect = false;
                lastPoint = hitInfo;
                break;
            }
            if (allLines.Count >= 13)
            {
                laserMustReflect = false;
                lastPoint = hitInfo;
                Debug.Log(allLines.Count);
                break;
            }

            

        }

        //пунктир
        if (hitInfo.collider.CompareTag("Block"))
        {
            Vector2 inDirection = Vector2.Reflect(ray2D.direction, hitInfo.normal);
            RaycastHit2D newHitInfo = Physics2D.Raycast(hitInfo.point + inDirection * 0.1f, inDirection);
            newLine = Instantiate(dottedLine);
            allLines.Add(newLine);
            newLineRenderer = newLine.GetComponent<LineRenderer>();
            newLineRenderer.SetPosition(0, hitInfo.point);
            newLineRenderer.SetPosition(1, newHitInfo.point);
        }



    }

    


    
}
