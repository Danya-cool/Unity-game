using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gun : MonoBehaviour
{
    [Header("Rotation")]
    public Transform firePoint;
    public Rigidbody2D rbCenterRotation;
    public GameObject CenterRotation;
    private Vector2 targetPosition;
    private Camera camera;
    private float angle;
    private float endAngle;
    public float step;
    public float stepByArrow;

    
    

    [Header("Shooting")]
    public GameObject bulletEffect;
    public Material materialBlock;
    public GameObject startEffect;
    public GameObject endEffect;
    public float cooldown = 0.02f;


    public bool needToShowTRJ { get; set; }


    public trajectoryRenderer trajectory;

    [Header("Selecting")]
    public GameObject selectedCircle;
    public bool canBeSelected = false;
    public bool isFirstGun = false;

    static gun selectedGun;
    static List<gun> allGuns = new List<gun>();

    [Header("Other")]
    static public bool getStar;
    public AudioSource shootAudio;

    private LevelSettings level;

    void Start()
    {
        print(stepByArrow);
        level = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelSettings>();

        needToShowTRJ = true; //пока так потом для красоты исправим
        allGuns.Add(gameObject.GetComponent<gun>());

        camera = Camera.main;
        endAngle = rbCenterRotation.rotation;

        if (isFirstGun)
        {
            StartCoroutine(showingTrajectory());
            selectedGun = gameObject.GetComponent<gun>();
            selectedCircle.SetActive(true);
        }

    }


    private void rotateAtMousePos()
    {
        targetPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = targetPosition - rbCenterRotation.position;

        
            angle = Vector2.Angle(transform.right, direction);
            transform.Rotate(new Vector3(0, 0, 1));
            float new_angle = Vector2.Angle(transform.right, direction);
            transform.Rotate(new Vector3(0, 0, -1));
            if (new_angle > angle)
            {
                angle = -angle;
            }
       
        
        

        endAngle = rbCenterRotation.rotation + angle;
        //print(endAngle);
        //float remainder = endAngle % 11.25f;
        //if (remainder > 5.625f)
        //{
        //    endAngle = endAngle - remainder;
        //}
        //else
        //{
        //    endAngle = endAngle + (11.25f - remainder);
        //}
        //print(endAngle);

        StartCoroutine(rotateGun());
    }

    IEnumerator rotateGun()
    {
        
        while (true)
        {
            rbCenterRotation.rotation = Mathf.Lerp(rbCenterRotation.rotation, endAngle, step);
            //if (Mathf.RoundToInt(rbCenterRotation.rotation) == Mathf.RoundToInt(endAngle))
            //{
            //    yield break;
            //}
            if (Mathf.Abs((Mathf.Abs(rbCenterRotation.rotation) - Mathf.Abs(endAngle))) < 0.01f)
                yield break;

            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator showingTrajectory()
    {
        

        while (true)
        {
            if (needToShowTRJ)
            {
                trajectory.showTRJ(endEffect);
                startEffect.SetActive(true);
                endEffect.SetActive(true);
            }
            else
            {
                trajectory.removeAllLines();
                startEffect.SetActive(false);
                endEffect.SetActive(false);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }


    public void shoot()
    {
        Collider2D collider2D;
        if (trajectory.lastPoint.collider)
        {
            collider2D = trajectory.lastPoint.collider;
        }
        else //для первой линии
        {
            collider2D = Physics2D.Raycast(firePoint.position, firePoint.right).collider;
        }

        if (collider2D.CompareTag("Block"))
        {
            //collider2D.GetComponent<cubeController>().isReflectable = true - это делает анимаця
            Animator animator = collider2D.GetComponent<Animator>();
            if (animator.isActiveAndEnabled)
                return;
            animator.enabled = true;
        }
        else if (collider2D.CompareTag("Finish"))
        {
            Animator animator = collider2D.GetComponent<Animator>();
            if (animator.isActiveAndEnabled)
                return;
            animator.enabled = true;
            level.spendFinishBlock();
            
        }
        else if (collider2D.CompareTag("Gun"))
        {
            gun _gun = collider2D.GetComponent<gun>();
            if (!_gun.canBeSelected)
            {
                _gun.canBeSelected = true;
                StartCoroutine(_gun.showingTrajectory());
            }
            else
                return;
        }
        else if (collider2D.CompareTag("Star"))
        {
            collider2D.GetComponent<star>().Collecting();
        }
        else
        {
            return;
        }
        level.spendEnergy();
        shootAudio.Play();
    }


    public void onClick()
    {
        if (!canBeSelected)
            return;
        foreach (var _gun in allGuns)
        {
            _gun.selectedCircle.SetActive(false);
        }
        selectedCircle.SetActive(true);
        selectedGun = gameObject.GetComponent<gun>();
    }
   

    //вызываетя только у одного gun в иерархии
    public void rotateSelectedGun()
    {
        if (selectedGun)
        {
            selectedGun.rotateAtMousePos();
        }
    }
    public void shootSelectedGun()
    {
        if (selectedGun)
        {
            selectedGun.shoot();
        }
    }

    private bool needRotate;
    public void rotateSelectedGunByArrowAtRight()
    {
        if (selectedGun)
        {
            StartCoroutine(rotating(-1));
        }
    }
    public void rotateSelectedGunByArrowAtLeft()
    {
        if (selectedGun)
        {
            StartCoroutine(rotating(1));
        }
    }
    public void ArrowUp()
    {
        needRotate = false;
    }
    IEnumerator rotating(int sign)
    {

        GameObject obgToRotate = selectedGun.GetComponent<gun>().CenterRotation;
        needRotate = true;
        float time = 1;
        while (needRotate)
        {
            if (time < 6)
                time += Time.deltaTime;

            float forse = 0.05f * sign;

            if (time > 1.4f)
                forse = forse * time * time * time;

            if (forse > 2)
                forse = 2;
            else if (forse < -2)
                forse = -2;
            obgToRotate.transform.Rotate(0, 0, forse);
           
            print($"forse {forse}: stepByArrow {stepByArrow} * sign {sign} * tome {time}");
            yield return new WaitForSeconds(0.01f) ;
        }
    }

}  


