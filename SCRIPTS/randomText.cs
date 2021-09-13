using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class randomText : MonoBehaviour
{
    public string[] strings;

    void Start()
    {
        gameObject.GetComponent<Text>().text = strings[Random.Range(0, strings.Length)];
        
    }

}
