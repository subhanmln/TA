using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreText : MonoBehaviour
{
    public GameObject gameC;
    int maxScore;
    // Start is called before the first frame update
    void Start()
    {
        maxScore = gameC.GetComponent<GameController>().CountBlocks(special: false);
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = (maxScore - gameC.GetComponent<GameController>().CountBlocks(special: false)).ToString();
    }
}
