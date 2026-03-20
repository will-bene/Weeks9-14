using System;
using System.Collections;
using UnityEngine;

public class TreeGrower : MonoBehaviour
{
    public AnimationCurve growCurve;
    public Transform branchesTransform;

    public float maxSpawnDistance;


    public float duration;

    public GameObject applePrefab;
    public float appleGrowDuration;

    private Coroutine treeGrowCoroutine;
    private Coroutine appleCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator treeGrowUpdate()
    {

        float progress = 0f;
        //the contents of the while loop run while the contion is true
        while (progress < duration)
        {
            progress += Time.deltaTime;
            transform.localScale = (growCurve.Evaluate(progress/ duration)) * Vector3.one;
            //relinqueshes control of unity so everything else can run for this frame
            //pauses code here on this frame, comes back at this point on the next
            yield return null;
        }
        //yield return new WaitForSeconds(appleGrowDuration); //relinquish control of unity until apple has finished growing
        appleCoroutine = StartCoroutine(appleGrowUpdate());

        //this will relinquish control of unity until the corooutine (apple) has finished executing
        yield return appleCoroutine;

        appleCoroutine = StartCoroutine(appleGrowUpdate());
        yield return appleCoroutine;

        StartCoroutine(appleGrowUpdate());

    }

    private IEnumerator appleGrowUpdate()
    {
        float progress = 0f;
        Vector3 spawnPosition = branchesTransform.position;
        spawnPosition += (Vector3) (UnityEngine.Random.insideUnitCircle * maxSpawnDistance);

        GameObject spawnedApple = Instantiate(applePrefab, spawnPosition, Quaternion.identity);
        spawnedApple.transform.localScale = Vector3.zero;

        while (progress < appleGrowDuration)
        {
            progress += Time.deltaTime;
            spawnedApple.transform.localScale = (growCurve.Evaluate(progress / appleGrowDuration)) * Vector3.one;
            //relinqueshes control of unity so everything else can run for this frame
            //pauses code here on this frame, comes back at this point on the next
            yield return null;
        }
    }

    public void onGrowPress()
    {
        treeGrowCoroutine = StartCoroutine(treeGrowUpdate()); 
    }

    public void onStopPress()
    {
        if (treeGrowCoroutine != null)
        {
            StopCoroutine(treeGrowCoroutine);
        }
        if (appleCoroutine != null)
        {
            StopCoroutine(appleCoroutine);
        }
    }

}
