using UnityEngine;

public class Pulse : MonoBehaviour
{
    public float speed = 2;
    public AnimationCurve curve;
    public float pulseProgress=0;
    public TrailRenderer trail;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(speed, 0, 0)*Time.deltaTime;
        pulseProgress += speed * Time.deltaTime;
        float xProgValue = pulseProgress;
        float yProgValue = curve.Evaluate(pulseProgress);

        if (pulseProgress > 1)
        {
            trail.Clear();
            trail.emitting = false;
            pulseProgress = 0;
        }
        else
        {
            trail.emitting = true;
        }

        float xValue = Screen.width * xProgValue;
        float yValue = Screen.height*yProgValue;

        Vector3 newPosition = Camera.main.ScreenToWorldPoint( new Vector3(xValue, yValue, 0));
        newPosition.z = 0;
        transform.position = newPosition;
    }
}
