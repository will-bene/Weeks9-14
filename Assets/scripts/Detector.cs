using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Detector : MonoBehaviour
{
    public UnityEvent Uevent;
    public SpriteRenderer renderer;

    public Vector2 mousePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer.bounds.Contains(mousePos))
        {
            Uevent.Invoke();
        }
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        mousePos = Camera.main.ScreenToWorldPoint( context.ReadValue<Vector2>());

    }
}
