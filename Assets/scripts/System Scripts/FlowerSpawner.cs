using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class FlowerSpawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    public List<GameObject> spawnedFlowers;
    //public Vector3 mousePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        //-- set position to mouse position (in world space) --
        Vector3 newPos = Vector3.zero;
        newPos= Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        newPos.z = 0;
        transform.position = newPos;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //-- spawn a new flower prefab in current position, starting its coroutine --
            SpawnFlower();
        }
    }

    public void SpawnFlower()
    {
        GameObject newFlower = Instantiate(spawnPrefab, transform.position, Quaternion.identity); //spawn a flower
        spawnedFlowers.Add(newFlower); //add new flower to list

        Flower flowerScript = newFlower.GetComponent<Flower>();
        flowerScript.Initialize(); //must be initialized before starting coroutine
        flowerScript.growCoroutine = flowerScript.StartCoroutine(flowerScript.GrowParts()); //start coroutine
        
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //-- test 
            WaterFlower();
        }
    }
    public void WaterFlower()
    {
        float waterRange = 1f;
        GameObject closestFlower = null;
        //loop through all flowers, checking the closest and storing it as the closest
        for (int i = 0; i < spawnedFlowers.Count; i++)
        {
            GameObject curFlower = spawnedFlowers[i]; //get current check
            float curDistance = Vector3.Distance( curFlower.transform.position, transform.position ); //get current distance
            if (curDistance < waterRange)
            {//its closer than the last one
                waterRange = curDistance; //lower range threshold
                closestFlower = curFlower; //set current check as closest
            }
        }

        if (closestFlower != null)
        {//if there is a close flower
            Flower flowerScript = closestFlower.GetComponent<Flower>();
            flowerScript.WaterFlower();
        }
    }

}
