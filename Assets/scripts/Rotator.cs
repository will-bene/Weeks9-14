using UnityEngine;

public class Rotator : MonoBehaviour
{
    public SpriteRenderer renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnRotate()
    {
        renderer.color = Color.blue;

    }

}
