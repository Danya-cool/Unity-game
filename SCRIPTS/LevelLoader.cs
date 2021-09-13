using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
   

    
    
    public void loadLevels()
    {
        StartCoroutine(loadLevel(1));
    }
    public void loadStartMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(loadLevel(0));
       //SceneManager.LoadScene(0);
    }
    public void loadlevel_1()
    {
        StartCoroutine(loadLevel(2));
    }

    public IEnumerator loadLevel(int levelIndex)
    {
        Time.timeScale = 1;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
    public void loadNextLevel()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        if (levelIndex == SceneManager.sceneCountInBuildSettings)
        {
            loadStartMenu();
            return;
        }
        StartCoroutine(loadLevel(levelIndex + 1));
    }
    public void reloadLevel()
    {
        StartCoroutine(loadLevel(SceneManager.GetActiveScene().buildIndex));
    }
    public void loadOpenedLevelfromMenu()
    {
        Saver data = Save_and_load.loading();
        StartCoroutine(loadLevel(data.lastCompletedLevel + 2));
    }
}
