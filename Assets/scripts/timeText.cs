using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeText : MonoBehaviour
{
    
    public GameController parentScript;
    public int time;
    int timenow;
    bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        timenow = time;
    }

    void Update()
    {
        this.GetComponent<Text>().text = System.TimeSpan.FromSeconds((int)timenow).ToString();
        if (!flag && timenow > 0)
        {
            StartCoroutine(clock());
        }
        if(timenow == 0)
        {
            StartCoroutine(parentScript.EndGameCoroutine(won: false));
        }
    }

    IEnumerator clock()
    {
        flag = true;

        yield return new WaitForSeconds(1.0f);
        timenow--;
        flag = false;
    }
}
