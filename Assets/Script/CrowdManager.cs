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

    private float targetCrowdAmount;
    private float targetMoveSpeed;

    void Start()
    {
        InitializeCrowd();
        targetCrowdAmount = crowdAmount;
        targetMoveSpeed = moveSpeed;
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
            GameObject crowdLeft = Instantiate(CharacterPrefab, new Vector3(Random.Range(0, 220.0f), Random.Range(0f, 700.0f), 0), Quaternion.identity, CharactersParent.transform);
            GameObject crowdRight = Instantiate(CharacterPrefab, new Vector3(Random.Range(1700f, 1920f), Random.Range(0f, 700.0f), 0), Quaternion.identity, CharactersParent.transform);
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

        // Adjust crowd properties based on total error
        CrowdController();
    }

    void CrowdController()
    {
        // Assume total_error ranges from 0 to 5
        float totalError = (float)SoundManager.total_error; // Replace with the actual way to get total_error from SoundManager

        // Calculate target values based on total_error
        float targetCrowdAmountRange = Mathf.Lerp(50, 400, totalError / 5f);
        float targetMoveSpeedRange = Mathf.Lerp(0.1f, 10f, totalError / 5f);

        // Smoothly transition to the target values
        targetCrowdAmount = Mathf.Lerp(targetCrowdAmount, targetCrowdAmountRange, Time.deltaTime * 0.1f); // Adjust the coefficient (0.5f) for desired transition speed
        targetMoveSpeed = Mathf.Lerp(targetMoveSpeed, targetMoveSpeedRange, Time.deltaTime * 0.1f); // Adjust the coefficient (0.5f) for desired transition speed

        // Apply the target values with a smoother transition
        //crowdAmount = Mathf.RoundToInt(targetCrowdAmount);
        moveSpeed = targetMoveSpeed;
    }
}
