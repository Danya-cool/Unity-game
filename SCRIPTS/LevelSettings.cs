using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettings : MonoBehaviour
{
    public int countEnergy;
    public int countFinishBlocks;
    public Text currentCountEnegy;
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject defeatPanel;
    public GameObject preDefeatPanel;
    public Slider slider;
    public bool gameIsOver = true;


    private void Start()
    {
        UpdateTextEnergy();
    }
    public void spendEnergy()
    {
        countEnergy -= 1;
        UpdateTextEnergy();

        if (countEnergy <= 0)
        {
            StartCoroutine(defeat());
        }
    }
    public void spendFinishBlock()
    {
        countFinishBlocks -= 1;
        if (countFinishBlocks <= 0)
        {
            StartCoroutine(win());
        }
    }

    private void UpdateTextEnergy()
    {
        currentCountEnegy.text = countEnergy.ToString();
        currentCountEnegy.GetComponent<Animator>().SetTrigger("Spend");
    }

    public void pauseDown()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void pauseUp()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public IEnumerator defeat()
    {
        defeatPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        if (countFinishBlocks == 0)
            yield break;

        defeatPanel.SetActive(true);
        preDefeatPanel.SetActive(true);

        float time = 0f;
        while (time < 5)
        {
            slider.value = time / 5;
            time += Time.deltaTime;
            yield return 0;
        }
        preDefeatPanel.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        if (gameIsOver)
        {
            defeatPanel.GetComponent<Animator>().enabled = true;
        }
        else
        {
            defeatPanel.SetActive(false);
        }
    }
    public IEnumerator win()
    {
        yield return new WaitForSeconds(1);
        winPanel.SetActive(true);
    }
}

