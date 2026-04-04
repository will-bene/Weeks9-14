using System.Collections.Generic;
using Unity.Multiplayer.Center.Common;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public GameObject planter; //get object to get list of spawned objects from
    public GameObject cursor; //get global cursor (for transform & methods)
    public bool selected = false;

    private Vector3 startingPosition;
    public float maxScale = 1.5f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ScaleOnMouseOver();
        CheckSelected();
        HandleMovement();
    }

    public void WaterFlower()
    {
        
        if (selected) //only if this tool is selected
        {
            //get list of spawned flowers from spawner object
            FlowerSpawner planterScript = planter.GetComponent<FlowerSpawner>();
            List<GameObject> spawnedFlowers = planterScript.spawnedFlowers;

            float waterRange = 1.4f;
            GameObject closestFlower = null;
            //loop through all flowers, checking the closest and storing it as the closest
            for (int i = 0; i < spawnedFlowers.Count; i++)
            {
                GameObject curFlower = spawnedFlowers[i]; //get current check
                float curDistance = Vector3.Distance(curFlower.transform.position, cursor.transform.position); //get current distance
                if (curDistance < waterRange)
                {//its closer than the last one
                    waterRange = curDistance; //lower range threshold
                    closestFlower = curFlower; //set current check as closest
                }
            }

            if (closestFlower != null)
            {//if there is a close flower
                Flower flowerScript = closestFlower.GetComponent<Flower>();
                flowerScript.WaterFlower(); //water it
            }
        }
    }
    public void HandleMovement()
    {//lerp to a distance around the cursor if currently selected
        Vector3 oldPosition = transform.position;
        if (selected) //only if this tool is selected
        {
            //offset cursor position to move to
            Vector3 goalPosition = cursor.transform.position;
            goalPosition.x += 1;
            goalPosition.y -= 1;

            //lerp to cursor position
            oldPosition = Vector3.Lerp(oldPosition, goalPosition, 0.05f);
        }
        else
        {//not selected, lerp back to starting position
            oldPosition = Vector3.Lerp(oldPosition, startingPosition, 0.05f);
        }
        transform.position = oldPosition;
    }

    public void CheckSelected()
    {//check if tool is selected
        Cursor cursorScript = cursor.GetComponent<Cursor>();
        if (cursorScript != null)
        {
            if (cursorScript.currentTool == gameObject)
            {//check if currently selected tool matches self
                selected = true;
            }
            else
            {
                selected = false;
            }
        }
    }

    public bool MouseOver()
    {
        float collisionRange = 0.8f;
        if (Vector3.Distance(transform.position, cursor.transform.position) < collisionRange)
        {//mouse is over this object (within range), indicate it
            return true;
        }
        else
        {//mouse is not over this object
            return false;
        }
    }

    public void ScaleOnMouseOver()
    {
        Vector3 oldScale = transform.localScale;
        if (MouseOver() && !selected)
        {//lerp scale to maxScale size if moused over, to show it's selectable
            oldScale = Vector3.Lerp(oldScale, Vector3.one * maxScale, 0.05f);
        }
        else
        {//return to initial scale
            oldScale = Vector3.Lerp(oldScale, Vector3.one, 0.05f);
        }
        transform.localScale = oldScale;
    }

    public void OnClick()
    {//when mouse is clicked
        if (MouseOver() && !selected)
        {//clicked on, select self as new tool
            Cursor cursorScript = cursor.GetComponent<Cursor>();
            if (cursorScript != null)
            {
                cursorScript.currentTool = gameObject;
            }
        }
        
        WaterFlower();
    }


}
