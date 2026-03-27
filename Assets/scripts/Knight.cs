using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class Knight : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip[] audioClips;
    public float speed;
    private float xValue;
    public Animator knightAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(xValue, 0, 0) * speed * Time.deltaTime;
    }

    public void OnFootstep()
    {
        Debug.Log("footstep");
        //audiosource.generator = audioClips[Random.Range(0, audioClips.Length - 1)];
        audiosource.clip = audioClips[Random.Range(0, audioClips.Length - 1)];
        audiosource.Play();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
       Vector2 moveDir = context.ReadValue<Vector2>();
       xValue = moveDir.x;

        bool isRunning = false ;
        if (xValue > 0) {
            isRunning = true;
        }

        knightAnimator.SetBool("isRunning", isRunning);
       
    }
}
