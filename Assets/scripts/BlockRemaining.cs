using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockRemaining : MonoBehaviour
{
    public GameObject blockremaining;
    int maxScore;
    // Start is called before the first frame update
    void Start()
    {
        maxScore = blockremaining.GetComponent<GameController>().CountBlocks(special: false);
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = (blockremaining.GetComponent<GameController>().CountBlocks(special: false)).ToString();
    }
}
