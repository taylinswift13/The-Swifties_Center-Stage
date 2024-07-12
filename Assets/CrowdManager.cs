using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    public GameObject CharacterPrefab; // Prefab for the character
    public List<GameObject> Characters = new List<GameObject>(); // List to store instantiated characters
    public Canvas canvas;
    public float amplitude = 5f; // Adjust as needed
    public float frequency = 1f; // Adjust as needed

    void Start()
    {
        // Instantiate characters
        for (int i = 0; i < 500; i++)
        {
            Instantiate(CharacterPrefab, new Vector3(Random.Range(0, 150.0f), Random.Range(0f, 400.0f), 0), Quaternion.identity, canvas.transform);
            Instantiate(CharacterPrefab, new Vector3(Random.Range(750f, 900f), Random.Range(0f, 400.0f), 0), Quaternion.identity, canvas.transform);

        }
    }
}
