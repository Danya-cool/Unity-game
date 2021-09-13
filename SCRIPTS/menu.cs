using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] buttons;
    public Text[] texts;
    public LevelLoader levelLoader;
    //загрузить последний доступный уровень
    public int lastCompleted;

    void Start()
    {
        Saver data = Save_and_load.loading();
        lastCompleted = data.lastCompletedLevel;


        int levelOffset = 0; //если уровень больше 21
        if (lastCompleted >= 21)
            levelOffset += 21;

        for (int i = 0; i < buttons.Length; i++)
        {
            int level = i + 1 + levelOffset;
            texts[i].text = level.ToString();
            if (level > lastCompleted + 1)
            {
                buttons[i].interactable = false;
            }
            if (!data.haveLevelStar[i + levelOffset])
            {
                buttons[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
    public void loadLevelAtMenu()
    {
        int Indexlevel = int.Parse(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text) + 1;
        print(Indexlevel);
        StartCoroutine(levelLoader.loadLevel(Indexlevel));
    }
    
}
