using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubespawn : MonoBehaviour
{
    public GameObject cubePrefab; //prefab of the cube
    public Vector3 start;
    public float maxX, maxY; //ints to be used for the max size of the grid

    //overload diff is set in the gamecontroller script and used to select the ColouPick function
    public void SpawnCubes(string diff)
    {
        //lays out number of cubes based on maxX and maxY values
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                start = new Vector3(x, y, 0);
                GameObject newcube = Instantiate(cubePrefab, start, transform.rotation);
                if (diff == "Easy")
                {
                    ColourPickEasy(newcube);
                }
                else if (diff == "Normal")
                {
                    ColourPickNormal(newcube);
                }
                else
                {
                    ColourPickHard(newcube);
                }
            }
        }
    }

    /*Selects the color the instantiated cube will be based on a random range,
     * Overload lets newcube be used as teh gameobect the colour is applied to.
     * Normal and Hard Colour pick work same way so not commented*/
    void ColourPickEasy(GameObject objectToColour)
    {
        int colorPicker = Random.Range(0, 3);
        if (colorPicker == 0)
        {
            objectToColour.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if (colorPicker == 1)
        {
            objectToColour.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (colorPicker == 2)
        {
            objectToColour.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    void ColourPickNormal(GameObject objectToColour)
    {
        int colorPicker = Random.Range(0, 4);
        if (colorPicker == 0)
        {
            objectToColour.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if (colorPicker == 1)
        {
            objectToColour.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (colorPicker == 2)
        {
            objectToColour.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            objectToColour.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
    }

    void ColourPickHard(GameObject objectToColour)
    {
        int colorPicker = Random.Range(0, 5);
        if (colorPicker == 0)
        {
            objectToColour.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if (colorPicker == 1)
        {
            objectToColour.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (colorPicker == 2)
        {
            objectToColour.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else if (colorPicker == 3)
        {
            objectToColour.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        else
        {
            objectToColour.GetComponent<MeshRenderer>().material.color = Color.cyan;
        }
    }
}
