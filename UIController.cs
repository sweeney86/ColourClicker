using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text gameOverText, scoreText;

    public Image nextColorObject, nextColorObject1, nextColorObject2, nextColorObject3, nextColorObject4;

    public List<Color> colours = new List<Color>();
    public List<Color> nextColours = new List<Color>();

    public int randomNumber;


    // Start is called before the first frame update
    void Start()
    {
        randomNumber = Random.Range(0, colours.Count);
        nextColours.Add(colours[randomNumber]);

        randomNumber = Random.Range(0, colours.Count);
        nextColours.Add(colours[randomNumber]);

        randomNumber = Random.Range(0, colours.Count);
        nextColours.Add(colours[randomNumber]);

        randomNumber = Random.Range(0, colours.Count);
        nextColours.Add(colours[randomNumber]);

        randomNumber = Random.Range(0, colours.Count);
        nextColours.Add(colours[randomNumber]);

        nextColorObject.GetComponent<Image>().color = nextColours[0];
        nextColorObject1.GetComponent<Image>().color = nextColours[1];
        nextColorObject2.GetComponent<Image>().color = nextColours[2];
        nextColorObject3.GetComponent<Image>().color = nextColours[3];
        nextColorObject4.GetComponent<Image>().color = nextColours[4];
    }


    public void AddColours(string diff)
    {
        if (diff == "Easy")
        {
            colours.Add(Color.red);
            colours.Add(Color.green);
            colours.Add(Color.blue);
        }
        else if (diff == "Normal")
        {
            colours.Add(Color.red);
            colours.Add(Color.green);
            colours.Add(Color.blue);
            colours.Add(Color.yellow);
        }
        else
        {
            colours.Add(Color.red);
            colours.Add(Color.green);
            colours.Add(Color.blue);
            colours.Add(Color.yellow);
            colours.Add(Color.cyan);
        }
    }

    public void UpdateScore(int clicks)
    {
        scoreText.text = "Turns Remaining: " + clicks.ToString();
    }

    public void UpdateColours()
    {
        if (nextColours.Count < 5)
        {
            randomNumber = Random.Range(0, colours.Count);
            nextColours.Add(colours[randomNumber]);
        }
        nextColorObject.GetComponent<Image>().color = nextColours[0];
        nextColorObject1.GetComponent<Image>().color = nextColours[1];
        nextColorObject2.GetComponent<Image>().color = nextColours[2];
        nextColorObject3.GetComponent<Image>().color = nextColours[3];
        nextColorObject4.GetComponent<Image>().color = nextColours[4];
    }

    public void GameOverDisplay(string gameovermeassage)
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = (gameovermeassage);
    }
}
