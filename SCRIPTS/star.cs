using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class star : MonoBehaviour
{
    public GameObject particleSys;
    public GameObject starImage;
    public AudioSource audio;

    public void Collecting()
    {
        Instantiate(particleSys, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
        starImage.GetComponent<Animator>().enabled = true;
        audio.PlayDelayed(0.1f);
        gun.getStar = true;

        
    }
}
