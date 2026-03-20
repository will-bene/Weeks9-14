using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnBasedManager : MonoBehaviour
{
    public Button growButtonA;
    public Button growButtonB;
    public GameObject playerA;
    public GameObject playerB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        growButtonA.interactable = false;
        growButtonB.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator manage()
    {
        
        float progress = 0f;
        //the contents of the while loop run while the contion is true
        yield return null;

    }
}
