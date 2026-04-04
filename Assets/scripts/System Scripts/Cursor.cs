using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Cursor : MonoBehaviour
{
    public UnityEvent globalClick;
    public GameObject currentTool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        //-- set position to mouse position (in world space) --
        Vector3 newPos = Vector3.zero;
        newPos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        newPos.z = 0;
        transform.position = newPos;
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //call unity event to talk to either tool (watering can and planter)
            //(doesnt really need to be a unity event... or even two diff scripts at all...)//
                globalClick.Invoke(); //does click event for watering can and planter (if they're selected)
        }
    }

}
