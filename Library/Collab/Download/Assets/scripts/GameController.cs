using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text TextComponent;
    //check if game is currently playing
    public bool isPlaying = true;
    int NumberOfSpecialBlocksAtTheBeginning;

	// Use this for initialization
	void Start () {
        TextComponent.enabled = false;
        //check how many special blocks are there at the beginning
        NumberOfSpecialBlocksAtTheBeginning = CountBlocks(special: true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int CountBlocks(bool special)
    {
        return FindObjectsOfType<Block>()
            .Where(block => block.Enabled)
            .Where(block => block.IsSpecial == special)
            .Count();
    }

    public void OnTriggerExit(Collider other)
    {
        var block = other.GetComponent<Block>();

        if (block == null) return;
        block.Enabled = false;

        if (block.IsSpecial)
        {
            StartCoroutine(EndGameCoroutine(won: false));

        } else if(CountBlocks(special: false) == 0)
        {
            StartCoroutine(EndGameCoroutine(won: true));

        }
        else
        {
            Debug.Log("Keep playing!");

        }

        Debug.Log("Blocks remaining: " + CountBlocks(false));
        Destroy(block.gameObject);

    }

    IEnumerator EndGameCoroutine(bool won)
    {
        if (isPlaying == false) yield break;

        TextComponent.enabled = true;
        isPlaying = false;

        if(won)
        {
            for(int i=5; i>0; i--)
            {
                TextComponent.text = i.ToString();
                yield return new WaitForSeconds(1f);
            }

            if(NumberOfSpecialBlocksAtTheBeginning != CountBlocks(special: true))
            {
                won = false;
            }
        }
        TextComponent.text = won ? "You Won!" : "You Lost!";
    }
}
