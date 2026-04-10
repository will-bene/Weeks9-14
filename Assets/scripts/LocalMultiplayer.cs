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
    private Coroutine dash;
    public float squeezeDuration = 1f;
    public AnimationCurve AnimationCurve;
    public TrailRenderer trailRenderer;



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

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (dash != null)
            {
                StopCoroutine(dash);
            }
            Debug.Log("Dash1");
            dash = StartCoroutine(Dash());
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

    private IEnumerator Dash()
    {
        float progress = 0;
        //enable trailrenderer
        trailRenderer.emitting = true;

        Debug.Log("Dash2");
        while (progress < 1)
        {
            progress += Time.deltaTime / squeezeDuration;

            moveSpeed = 6f;

            yield return null;
        }

        trailRenderer.emitting = false;
        moveSpeed = 3f;
    }

}
