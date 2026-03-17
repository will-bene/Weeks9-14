using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EToInteract : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public int spriteInd = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSprite(InputAction.CallbackContext context)
    {
        //Debug.Log(context);
        if (context.started == true)
        {
            if (spriteInd < spriteArray.Length-1)
            {
                spriteInd++;
            }
            else
            {
                spriteInd = 0;
            }
            spriteRenderer.sprite = spriteArray[spriteInd];
        }
    }
}
