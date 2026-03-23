using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    //public IEnumerator GrowUp()
    //{
    //    while (progress < duration)
    //    {
    //        progress += Time.deltaTime;
    //        transform.localScale = (Vector3.one * curve.Evaluate(progress / duration));
    //        yield return null;
    //    }
    //}

    public IEnumerator GrowParts()
    {
        for (int i = 0; i < 3; i++)
        {
            float progress = 0;

            Transform curChild = transform.GetChild(i);
            while (progress < duration)
            {
                progress += Time.deltaTime;
                curChild.localScale = (Vector3.one * curve.Evaluate(progress / duration));
                yield return null;
            }
        }

    }

}
