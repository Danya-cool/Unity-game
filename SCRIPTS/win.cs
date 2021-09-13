using UnityEngine.SceneManagement;
using UnityEngine;

public class win : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Saver data = Save_and_load.loading();
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        if (gun.getStar)
            data.haveLevelStar[activeScene - 2] = true;
        data.lastCompletedLevel = activeScene - 1;
        
        Save_and_load.saving(data);
    }

   
}
