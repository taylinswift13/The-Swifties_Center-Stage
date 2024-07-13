using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    public GameObject CharacterPrefab; // Prefab for the character
    public GameObject CharactersParent; // Parent object for the characters

    [Range(0, 400)] // Slider to control the number of crowd members
    public int crowdAmount = 200;
    private int previousCrowdAmount = 200;

    [Range(0.1f, 10f)] // Slider to control the movement speed
    public float moveSpeed = 1f;

    public float amplitude = 5f; // Adjust as needed
    public float frequency = 1f; // Adjust as needed

    private List<GameObject> Characters = new List<GameObject>(); // List to store instantiated characters
    private List<Vector3> initialPositions = new List<Vector3>(); // List to store initial positions of characters

    void Start()
    {
        InitializeCrowd();
    }

    void InitializeCrowd()
    {
        // Clear any existing characters
        foreach (GameObject character in Characters)
        {
            Destroy(character);
        }
        Characters.Clear();
        initialPositions.Clear();

        // Instantiate characters
        for (int i = 0; i < crowdAmount; i++)
        {
            GameObject crowdLeft = Instantiate(CharacterPrefab, new Vector3(Random.Range(0, 220.0f), Random.Range(0f, 400.0f), 0), Quaternion.identity, CharactersParent.transform);
            GameObject crowdRight = Instantiate(CharacterPrefab, new Vector3(Random.Range(880f, 1200f), Random.Range(0f, 400.0f), 0), Quaternion.identity, CharactersParent.transform);
            Characters.Add(crowdLeft);
            Characters.Add(crowdRight);

            // Store initial positions
            initialPositions.Add(crowdLeft.transform.position);
            initialPositions.Add(crowdRight.transform.position);
        }
    }

    private void Update()
    {
        // Re-initialize the crowd if the crowd amount has changed
        if (crowdAmount != previousCrowdAmount)
        {
            previousCrowdAmount = crowdAmount;
            InitializeCrowd();
        }

        // Move characters around their initial positions to simulate crowd behavior
        for (int i = 0; i < Characters.Count; i++)
        {
            Vector3 initialPosition = initialPositions[i];
            float offsetX = Mathf.Sin(Time.time * frequency * moveSpeed + i) * amplitude;
            float offsetY = Mathf.Cos(Time.time * frequency * moveSpeed + i) * amplitude;
            Characters[i].transform.position = new Vector3(initialPosition.x + offsetX, initialPosition.y + offsetY, initialPosition.z);
        }
    }
}
