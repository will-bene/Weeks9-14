using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class FlowerSpawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameObject newFlower = Instantiate(spawnPrefab, mousePos, Quaternion.identity);
            Flower flowerScript = newFlower.GetComponent<Flower>();
            flowerScript.StartCoroutine(flowerScript.GrowParts());
        }
    }
}
