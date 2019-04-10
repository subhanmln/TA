using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialBlockDetector : MonoBehaviour
{
    GameObject gamecontroller;
    GameController parentScript;
    bool flag = false;
    void Start()
    {
        gamecontroller = transform.parent.gameObject;
        parentScript = gamecontroller.GetComponent<GameController>();
    }

    void Update()
    {
        flag = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (flag)
        {
            var block = other.GetComponent<Block>();
            if (block != null)
            {

                if (block.IsSpecial)
                {
                    block.Enabled = false;
                    StartCoroutine(parentScript.EndGameCoroutine(won: false));
                    Destroy(block.gameObject);

                }
                else // not win 
                {

                }
            }
        }
    }
}
