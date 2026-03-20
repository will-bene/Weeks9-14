using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnBased1 : MonoBehaviour
{
    private Coroutine growCoroutine;
    public Button growButton;
    public AnimationCurve growCurve;
    public float duration = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator growUpdate()
    {
        growButton.interactable = false;
        float progress = 0f;
        //the contents of the while loop run while the contion is true
        while (progress < duration)
        {
            progress += Time.deltaTime;
            transform.localScale = (growCurve.Evaluate(progress / duration)) * Vector3.one;
            //relinqueshes control of unity so everything else can run for this frame
            //pauses code here on this frame, comes back at this point on the next
            yield return null;
        }
        growButton.interactable = true;

    }

    public void onGrowPress()
    {
        growCoroutine = StartCoroutine(growUpdate());
    }
}
