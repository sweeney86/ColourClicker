using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public UIController uic; //ref to UI script
    public cubespawn cs; //ref to cubespawn script
    public RaycastScript rs; //ref to raycast script

    public int clicksRemaining;
    public List<GameObject> allCubes = new List<GameObject>();
    public List<GameObject> allGreen = new List<GameObject>();
    public List<GameObject> allBlue = new List<GameObject>();
    public List<GameObject> allRed = new List<GameObject>();
    public List<GameObject> allYellow = new List<GameObject>();
    public List<GameObject> allCyan = new List<GameObject>();

    public string diff;

    public bool gameStarted;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Awake()
    {
        // get components to scripts, also the scene name
        diff = SceneManager.GetActiveScene().name;
        uic = GetComponent<UIController>();
        cs = GetComponent<cubespawn>();
        rs = GetComponent<RaycastScript>();

        //set bool to true
        gameStarted = true;

        //feeds diff var to the AddColours funcution in the UI script
        uic.AddColours(diff);

        //if both gamestarted is true and gameOver is false runs the logic for the game setup from each script in one place.
        if (gameStarted && !gameOver)
        {
            uic.UpdateScore(clicksRemaining);
            cs.SpawnCubes(diff);

            //finds all cubes and adds the to allCubes list
            foreach (GameObject cube in GameObject.FindGameObjectsWithTag("GridCube"))
            {
                allCubes.Add(cube);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for ever click while gameOver is false runs game logic.
        if(Input.GetMouseButtonDown(0) && !gameOver)
        {
            rs.CastRaycast();
            if (rs.hitObject == true)
            {
                clicksRemaining--;
                uic.UpdateScore(clicksRemaining);
                UpdateGrid();
                UpdateLists();
                uic.UpdateColours();
                rs.hitObject = false;
            }
        }

        //sets gameOver to true and displays gameover message for running out of clicks
        if (clicksRemaining <= 0 && allBlue.Count != allCubes.Count && allGreen.Count != allCubes.Count && allRed.Count != allCubes.Count && allYellow.Count != allCubes.Count && allCyan.Count != allCubes.Count)
        {
            gameOver = true;
            uic.GameOverDisplay("You lost! Press R to try again with a new layout or press Q to go back to the Menu");
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Menu");
            }
        }
        //sets gameOver to true and displays gameover message for winning
        if (clicksRemaining >= 0 && allBlue.Count == allCubes.Count || allGreen.Count == allCubes.Count || allRed.Count == allCubes.Count || allYellow.Count == allCubes.Count || allCyan.Count == allCubes.Count)
        {
            gameOver = true;
            uic.GameOverDisplay("You Won! Press R to try again with a new layout or press Q to go back to the Menu");
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    //function that changes all cubes in surronds list to the same colour
    public void UpdateGrid()
    {
        for (int i = 0; i < rs.surrounds.Count; i++)
        {
            rs.surrounds[i].gameObject.GetComponent<MeshRenderer>().material.color = uic.nextColours[0];
        }
    }

    void UpdateLists()
    {
        uic.nextColours.RemoveAt(0);
        rs.surrounds.Clear();

        if(diff == "Easy")
        {
            UpdateListEasy();
        }
        else if (diff == "Normal")
        {
            UpdateListNormal();
        }
        else
        {
            UpdateListHard();
        }
    }


    /*clears the colour cube lists and then adds in the new cubes of each colour.
    *UpdateListNormal and Hard work the same way so not commented*/
    void UpdateListEasy()
    {
        //clears lists
        allGreen.Clear();
        allBlue.Clear();
        allRed.Clear();

        //runs through the allCubes list and add gameobjects to list for each colour
        for (int i = 0; i < allCubes.Count; i++)
        {
            if (allCubes[i].GetComponent<MeshRenderer>().material.color == Color.green)
            {
                allGreen.Add(gameObject);
            }
            if (allCubes[i].GetComponent<MeshRenderer>().material.color == Color.blue)
            {
                allBlue.Add(gameObject);
            }
            if (allCubes[i].GetComponent<MeshRenderer>().material.color == Color.red)
            {
                allRed.Add(gameObject);
            }
        }
        /*if no cubes of this colour exists, updates the colours list 
         * in the ui script to remove empty colour from the list so it 
         * doesnt show in the upcoming colors for the next turns.*/
        if (allGreen.Count == 0)
        {
            //If colour still exist is colours list, if colour not in list will show as -1
            if (uic.colours.IndexOf(Color.green) != -1)
            {
                //get and remove the colour from the list using its index number
                int colorToRemove = uic.colours.IndexOf(Color.green);
                uic.colours.RemoveAt(colorToRemove);

                //removes and replaces any upcoming colour of this colour from the nextColours list in the UI script
                for (int i = 0; i < uic.nextColours.Count; i++)
                {
                    if (uic.nextColours[i] == Color.green)
                    {
                        uic.nextColours.RemoveAt(i);
                        uic.randomNumber = Random.Range(0, uic.colours.Count);
                        uic.nextColours.Add(uic.colours[uic.randomNumber]);
                        //moves i back one so it doesnt miss any doubles in the list 
                        i--;
                    }
                }
            }
        }
        if (allBlue.Count == 0)
        {
            if (uic.colours.IndexOf(Color.blue) != -1)
            {
                int colorToRemove = uic.colours.IndexOf(Color.blue);
                uic.colours.RemoveAt(colorToRemove);

                for (int i = 0; i < uic.nextColours.Count; i++)
                {
                    if (uic.nextColours[i] == Color.blue)
                    {
                        uic.nextColours.RemoveAt(i);
                        uic.randomNumber = Random.Range(0, uic.colours.Count);
                        uic.nextColours.Add(uic.colours[uic.randomNumber]);
                        i--;
                    }
                }
            }
        }
        if (allRed.Count == 0)
        {
            if (uic.colours.IndexOf(Color.red) != -1)
            {
                int colorToRemove = uic.colours.IndexOf(Color.red);
                uic.colours.RemoveAt(colorToRemove);

                for (int i = 0; i < uic.nextColours.Count; i++)
                {
                    if (uic.nextColours[i] == Color.red)
                    {
                        uic.nextColours.RemoveAt(i);
                        uic.randomNumber = Random.Range(0, uic.colours.Count);
                        uic.nextColours.Add(uic.colours[uic.randomNumber]);
                        i--;
                    }
                }
            }
        }
    }

    void UpdateListNormal()
    {
        allGreen.Clear();
        allBlue.Clear();
        allRed.Clear();
        allYellow.Clear();

        for (int i = 0; i < allCubes.Count; i++)
        {
            if (allCubes[i].GetComponent<MeshRenderer>().material.color == Color.green)
            {
                allGreen.Add(gameObject);
            }
            if (allCubes[i].GetComponent<MeshRenderer>().material.color == Color.blue)
            {
                allBlue.Add(gameObject);
            }
            if (allCubes[i].GetComponent<MeshRenderer>().material.color == Color.red)
            {
                allRed.Add(gameObject);
            }
            if (allCubes[i].GetComponent<MeshRenderer>().material.color == Color.yellow)
            {
                allYellow.Add(gameObject);
            }
        }
        if (allGreen.Count == 0)
        {
            if (uic.colours.IndexOf(Color.green) != -1)
            {
                int colorToRemove = uic.colours.IndexOf(Color.green);
                uic.colours.RemoveAt(colorToRemove);
                for (int i = 0; i < uic.nextColours.Count; i++)
                {
                    if (uic.nextColours[i] == Color.green)
                    {
                        uic.nextColours.RemoveAt(i);
                        uic.randomNumber = Random.Range(0, uic.colours.Count);
                        uic.nextColours.Add(uic.colours[uic.randomNumber]);
                        i--;
                    }
                }
            }
        }
        if (allBlue.Count == 0)
        {
            if (uic.colours.IndexOf(Color.blue) != -1)
            {
                int colorToRemove = uic.colours.IndexOf(Color.blue);
                uic.colours.RemoveAt(colorToRemove);

                for (int i = 0; i < uic.nextColours.Count; i++)
                {
                    if (uic.nextColours[i] == Color.blue)
                    {
                        uic.nextColours.RemoveAt(i);
                        uic.randomNumber = Random.Range(0, uic.colours.Count);
                        uic.nextColours.Add(uic.colours[uic.randomNumber]);
                        i--;
                    }
                }
            }
        }
        if (allRed.Count == 0)
        {
            if (uic.colours.IndexOf(Color.red) != -1)
            {
                int colorToRemove = uic.colours.IndexOf(Color.red);
                uic.colours.RemoveAt(colorToRemove);

                for (int i = 0; i < uic.nextColours.Count; i++)
                {
                    if (uic.nextColours[i] == Color.red)
                    {
                        uic.nextColours.RemoveAt(i);
                        uic.randomNumber = Random.Range(0, uic.colours.Count);
                        uic.nextColours.Add(uic.colours[uic.randomNumber]);
                        i--;
                    }
                }
            }
        }
        if (allYellow.Count == 0)
        {
            if (uic.colours.IndexOf(Color.yellow) != -1)
            {
                int colorToRemove = uic.colours.IndexOf(Color.yellow);
                uic.colours.RemoveAt(colorToRemove);

                for (int i = 0; i < uic.nextColours.Count; i++)
                {
                    if (uic.nextColours[i] == Color.yellow)
                    {
                        uic.nextColours.RemoveAt(i);
                        uic.randomNumber = Random.Range(0, uic.colours.Count);
                        uic.nextColours.Add(uic.colours[uic.randomNumber]);
                        i--;
                    }
                }
            }
        }
    }

    void UpdateListHard()
    {
        allGreen.Clear();
        allBlue.Clear();
        allRed.Clear();
        allYellow.Clear();
        allCyan.Clear();

        for (int i = 0; i < allCubes.Count; i++)
        {
            if (allCubes[i].GetComponent<MeshRenderer>().material.color == Color.green)
            {
                allGreen.Add(gameObject);
            }
            if (allCubes[i].GetComponent<MeshRenderer>().material.color == Color.blue)
            {
                allBlue.Add(gameObject);
            }
            if (allCubes[i].GetComponent<MeshRenderer>().material.color == Color.red)
            {
                allRed.Add(gameObject);
            }
            if (allCubes[i].GetComponent<MeshRenderer>().material.color == Color.yellow)
            {
                allYellow.Add(gameObject);
            }
            if (allCubes[i].GetComponent<MeshRenderer>().material.color == Color.cyan)
            {
                allCyan.Add(gameObject);
            }
        }
        if (allGreen.Count == 0)
        {
            if (uic.colours.IndexOf(Color.green) != -1)
            {
                int colorToRemove = uic.colours.IndexOf(Color.green);
                uic.colours.RemoveAt(colorToRemove);
                for (int i = 0; i < uic.nextColours.Count; i++)
                {
                    if (uic.nextColours[i] == Color.green)
                    {
                        uic.nextColours.RemoveAt(i);
                        uic.randomNumber = Random.Range(0, uic.colours.Count);
                        uic.nextColours.Add(uic.colours[uic.randomNumber]);
                        i--;
                    }
                }
            }
        }
        if (allBlue.Count == 0)
        {
            if (uic.colours.IndexOf(Color.blue) != -1)
            {
                int colorToRemove = uic.colours.IndexOf(Color.blue);
                uic.colours.RemoveAt(colorToRemove);

                for (int i = 0; i < uic.nextColours.Count; i++)
                {
                    if (uic.nextColours[i] == Color.blue)
                    {
                        uic.nextColours.RemoveAt(i);
                        uic.randomNumber = Random.Range(0, uic.colours.Count);
                        uic.nextColours.Add(uic.colours[uic.randomNumber]);
                        i--;
                    }
                }
            }
        }
        if (allRed.Count == 0)
        {
            if (uic.colours.IndexOf(Color.red) != -1)
            {
                int colorToRemove = uic.colours.IndexOf(Color.red);
                uic.colours.RemoveAt(colorToRemove);

                for (int i = 0; i < uic.nextColours.Count; i++)
                {
                    if (uic.nextColours[i] == Color.red)
                    {
                        uic.nextColours.RemoveAt(i);
                        uic.randomNumber = Random.Range(0, uic.colours.Count);
                        uic.nextColours.Add(uic.colours[uic.randomNumber]);
                        i--;
                    }
                }
            }
        }
        if (allYellow.Count == 0)
        {
            if (uic.colours.IndexOf(Color.yellow) != -1)
            {
                int colorToRemove = uic.colours.IndexOf(Color.yellow);
                uic.colours.RemoveAt(colorToRemove);

                for (int i = 0; i < uic.nextColours.Count; i++)
                {
                    if (uic.nextColours[i] == Color.yellow)
                    {
                        uic.nextColours.RemoveAt(i);
                        uic.randomNumber = Random.Range(0, uic.colours.Count);
                        uic.nextColours.Add(uic.colours[uic.randomNumber]);
                        i--;
                    }
                }
            }
        }
        if (allCyan.Count == 0)
        {
            if (uic.colours.IndexOf(Color.cyan) != -1)
            {
                int colorToRemove = uic.colours.IndexOf(Color.cyan);
                uic.colours.RemoveAt(colorToRemove);

                for (int i = 0; i < uic.nextColours.Count; i++)
                {
                    if (uic.nextColours[i] == Color.cyan)
                    {
                        uic.nextColours.RemoveAt(i);
                        uic.randomNumber = Random.Range(0, uic.colours.Count);
                        uic.nextColours.Add(uic.colours[uic.randomNumber]);
                        i--;
                    }
                }
            }
        }
    }
}
