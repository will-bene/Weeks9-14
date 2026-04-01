using UnityEngine;
using System.Collections;
using UnityEditor;
public class Flower : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 10;
    private float growProgress = 0f;
    private int partsAmount = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float getPartProgress(int curPart)
    {//using the 'global' flower grow progress, get the individual progress based on which part youre on
        float curGlobalProg = growProgress * partsAmount; //gets global progress from 0-3
        float partProg = 1-Mathf.Clamp((curPart+1) -curGlobalProg, 0, 1); //gets current part's progress, ex if progress is at 0.5 -> 1.5, part 1 is at 1, part 2 at 0.5, part 3 at 0
        return partProg; //returns progress of part from 0-1
    }
    public IEnumerator GrowParts()
    {//coroutine for growing each part of flower based on grow progress
        for (int i = 0; i < partsAmount; i++)
        {//set grow scale of each child to current progress amount (only really matters when coroutine is restarted, otherwise will start at x0 scale)
            Transform curChild = transform.GetChild(i); //get child at i
            curChild.localScale = Vector3.one * (getPartProgress(i)); //set child's scale based on global prog
        }

        for (int i = 0; i < partsAmount; i++)
        {//loop through each child piece
            float progress = getPartProgress(i)*duration; //reset progress to match global grow progress

            Transform curChild = transform.GetChild(i); //get child at i
            while (progress < duration)
            {//until progress is maxxed out
                progress += Time.deltaTime; //increase progress
                curChild.localScale = (Vector3.one * curve.Evaluate(progress / duration)); //set child's scale based on prog
                yield return null; //point of coroutine pause
            }
        }

    }

}
