//WEEK THIRTEEN
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultiPlayer : MonoBehaviour
{
    public Vector2 moveDirection;
    public float moveSpeed;
    public LocalMultiplayerManager manager;

    private Coroutine squeeze;
    public float squeezeDuration = 1f;
    public AnimationCurve AnimationCurve;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerInput selfInput = GetComponent<PlayerInput>();
            manager.TryAttack(selfInput);


            if (squeeze!=null)
            {
                StopCoroutine(squeeze);
            }
            squeeze = StartCoroutine(SqueezePlayer());

        }
        
    }

    private IEnumerator SqueezePlayer()
    {
        float progress = 0;
        transform.localScale = Vector3.one;

        while (progress < 1)
        {
            progress += Time.deltaTime / squeezeDuration;
            Vector3 newScale = Vector3.one;

            newScale.x=AnimationCurve.Evaluate(progress);
            transform.localScale=newScale;
            yield return null;
        }
    }

}
