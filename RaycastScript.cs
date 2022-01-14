using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastScript : MonoBehaviour
{
    public Camera cam; //camera object
    public List<GameObject> surrounds = new List<GameObject>(); //list to be used to add all cubes of same same too
    public Color selectedColor; //colour of teh cube clicked
    public bool hitObject;//to be used to say if raycast hit the cubes, stops errent mouse clicks using a turn

    public void CastRaycast()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            surrounds.Add(objectHit.gameObject);
            selectedColor = objectHit.GetComponent<MeshRenderer>().material.color;
            if(objectHit.gameObject.tag == "GridCube")
            {
                hitObject = true;
            }

            //send out a raycast in a direction from the cube clicked and if cube above is same colour adds it to the surronds list 
            RaycastHit rayUp;
            if (Physics.Raycast(objectHit.position, transform.TransformDirection(Vector3.up), out rayUp, 1f))
            {
                if (rayUp.transform.GetComponent<MeshRenderer>().material.color == selectedColor)
                {
                    surrounds.Add(rayUp.transform.gameObject);
                }
            }
            RaycastHit rayDown;
            if (Physics.Raycast(objectHit.position, transform.TransformDirection(Vector3.down), out rayDown, 1f))
            {
                if (rayDown.transform.GetComponent<MeshRenderer>().material.color == selectedColor)
                {
                    surrounds.Add(rayDown.transform.gameObject);
                }
            }

            RaycastHit rayLeft;
            if (Physics.Raycast(objectHit.position, transform.TransformDirection(Vector3.left), out rayLeft, 1f))
            {
                if (rayLeft.transform.GetComponent<MeshRenderer>().material.color == selectedColor)
                {
                    surrounds.Add(rayLeft.transform.gameObject);
                }
            }
            RaycastHit rayRight;
            if (Physics.Raycast(objectHit.position, transform.TransformDirection(Vector3.right), out rayRight, 1f))
            {
                if (rayRight.transform.GetComponent<MeshRenderer>().material.color == selectedColor)
                {
                    surrounds.Add(rayRight.transform.gameObject);
                }
            }

            //goes through the surronds list and cast new raycasts adding cubes of the same colour to the list
            for (int i = 0; i < surrounds.Count; i++)
            {
                RaycastHit recastRayUp;
                if (Physics.Raycast(surrounds[i].transform.position, transform.TransformDirection(Vector3.up), out recastRayUp, 1f))
                {
                    if (recastRayUp.transform.GetComponent<MeshRenderer>().material.color == selectedColor)
                    {
                        //!surronds.contains stops objects already in the list being added again and causing the raycasts to be cast infinately
                        if (!surrounds.Contains(recastRayUp.transform.gameObject))
                        {
                            surrounds.Add(recastRayUp.transform.gameObject);
                        }
                    }
                }
                RaycastHit recastRayDown;
                if (Physics.Raycast(surrounds[i].transform.position, transform.TransformDirection(Vector3.down), out recastRayDown, 1f))
                {

                    if (recastRayDown.transform.GetComponent<MeshRenderer>().material.color == selectedColor)
                    {
                        if (!surrounds.Contains(recastRayDown.transform.gameObject))
                        {
                            surrounds.Add(recastRayDown.transform.gameObject);
                        }
                    }
                }

                RaycastHit recastRayRight;
                if (Physics.Raycast(surrounds[i].transform.position, transform.TransformDirection(Vector3.right), out recastRayRight, 1f))
                {
                    if (recastRayRight.transform.GetComponent<MeshRenderer>().material.color == selectedColor)
                    {
                        if (!surrounds.Contains(recastRayRight.transform.gameObject))
                        {
                            surrounds.Add(recastRayRight.transform.gameObject);
                        }
                    }
                }
                RaycastHit recastRayLeft;
                if (Physics.Raycast(surrounds[i].transform.position, transform.TransformDirection(Vector3.left), out recastRayLeft, 1f))
                {

                    if (recastRayLeft.transform.GetComponent<MeshRenderer>().material.color == selectedColor)
                    {
                        if (!surrounds.Contains(recastRayLeft.transform.gameObject))
                        {
                            surrounds.Add(recastRayLeft.transform.gameObject);
                        }
                    }
                }
            }
        }
    }
}
