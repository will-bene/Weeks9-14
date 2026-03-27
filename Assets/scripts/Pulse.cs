using UnityEngine;

public class Pulse : MonoBehaviour
{
    public float speed = 2;
    public AnimationCurve curve;
    public float pulseProgress=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(speed, 0, 0)*Time.deltaTime;
        pulseProgress += speed * Time.deltaTime;
        float xValue = pulseProgress;
        float yValue = curve.Evaluate(pulseProgress);

        if (pulseProgress>1)
        {
            pulseProgress = 0;
        }



    }
}
