using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Text TextComponent;
    //check if game is currently playing
    public bool isPlaying = true;

    int NumberOfSpecialBlocksAtTheBeginning;

    public string NextLevelName;


	// Use this for initialization
	void Start () {

        FixLighting();


        TextComponent.enabled = false;
        //check how many special blocks are there at the beginning
        NumberOfSpecialBlocksAtTheBeginning = CountBlocks(special: true);
	}

    public void OnValidate()
    {
        FixLighting();
    }

    void FixLighting()
    {
        // light settings
        RenderSettings.ambientLight = Color.white;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
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

        yield return new WaitForSeconds(3f);

        // if there are no levels left - quit
        if (string.IsNullOrEmpty(NextLevelName))
        {
            Debug.Log("The end. You beat the game.");
            Application.Quit();
        }
        // else load level with given name
        else
        {
            SceneManager.LoadScene(NextLevelName);
        }
    }
}
