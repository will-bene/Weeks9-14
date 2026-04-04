using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.ComponentModel;
public class Flower : MonoBehaviour
{
    //gorCurve
    public AnimationCurve curve;

    //growing
    public float duration = 10;
    public float growProgress = 0f; //from 0-1
    public List<float> partsProgress; //contains progress of parts
    private int partsAmount = 3;

    //water
    private float waterDuration = 10f;
    public float waterValue = 5f;
    public Slider waterSlider;

    //grow coroutine storage
    public Coroutine growCoroutine;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waterSlider.maxValue = waterDuration; //initialize water slider maximum

    }

    // Update is called once per frame
    void Update()
    {
        HandleWater(); //decrease water value, stopping grow coroutine if its low enough
    }

    public void Initialize()
    {
        for (int i = 0; i < partsAmount; i++)
        {//initialize part progress
            partsProgress.Add(0);
            //Debug.Log(partsProgress[0]);
        }
    }

    public void updateGlobalProgress()
    {//updates global progress (combining all part progress into one value) from 0-1
        float curAmount = partsProgress.Sum(); //from 0-3
        //Debug.Log(curAmount);
        growProgress = (curAmount / partsAmount) / duration;
    }

    public IEnumerator GrowParts()
    {//coroutine for growing each part of flower based on grow progress
        for (int i = 0; i < partsAmount; i++)
        {//set grow scale of each child to current progress amount (only really matters when coroutine is restarted, otherwise will start at x0 scale)
            Transform curChild = transform.GetChild(i); //get child at i

            curChild.localScale = (Vector3.one * curve.Evaluate(partsProgress[i] / duration)); //set child's scale based on global prog
        }

        for (int i = 0; i < partsAmount; i++)
        {//loop through each child piece
            //float progress = partsProgress[i]; //reset progress to match global grow progress

            Transform curChild = transform.GetChild(i); //get child at i
            while (partsProgress[i] < duration)
            {//until progress is maxxed out
                partsProgress[i] += Time.deltaTime; //increase progress

                updateGlobalProgress(); //update global grow progress

                curChild.localScale = (Vector3.one * curve.Evaluate(partsProgress[i] / duration)); //set child's scale based on prog
                yield return null; //point of coroutine pause
            }
        }

    }

    public void HandleWater()
    {//always active, doesn't need to be in a coroutine
        if (growProgress < 1)
        {//not finished growing yet
            if (waterValue > 0)
            {//when water is above 0
                waterValue -= Time.deltaTime; //decrease water value
                waterSlider.value = waterValue;//change slider to match value
            }
            else
            {//stop growing coroutine when out of water
                if (growCoroutine != null)
                {//check if it exists first
                    StopCoroutine(growCoroutine); //stop growing
                }
            }
        }
        else
        {//finished growing, hide water slider
            waterSlider.gameObject.SetActive(false);
        }
    }

    public void WaterFlower()
    {//water flower, reset its value to duration
        waterValue = waterDuration;
        waterSlider.value = waterValue;

        if (growCoroutine != null) 
        {//if grow routine is already active, restart it
            StopCoroutine(growCoroutine); //stop growing
            growCoroutine = StartCoroutine(GrowParts());
        }
    }

}
