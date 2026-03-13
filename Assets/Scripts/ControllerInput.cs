using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    public float speed;
    public Vector2 directionalInput;
    public Vector2 lookInput;
    public Vector2 pointInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)directionalInput * speed * Time.deltaTime;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(pointInput);
        mousePosition.z = 0;
        transform.up = mousePosition-transform.position;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        directionalInput = context.ReadValue<Vector2>();
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack Time! ( " + context.phase + " )");
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        Debug.Log("On point ( " + context.ReadValue<Vector2>() + " )");
        pointInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        //lookInput = context.ReadValue<Vector2>();
    }
}
