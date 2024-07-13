using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    public GameObject CharacterPrefab; // Prefab for the character
    public GameObject CharactersParent; // Parent object for the characters
    public float crowdChangeSpeed = 0.05f;
    public float leftBound;
    public float rightBound;
    public float height;
    public int maxCrowdAmount = 400; // Maximum crowd amount
    private int currentCrowdAmountLeft = 100; // Initial crowd amount for left side
    private int currentCrowdAmountRight = 100; // Initial crowd amount for right side
    private List<GameObject> CharactersLeft = new List<GameObject>(); // List to store instantiated characters on left
    private List<GameObject> CharactersRight = new List<GameObject>(); // List to store instantiated characters on right

    private float targetCrowdAmountLeft;
    private float targetCrowdAmountRight;

    void Start()
    {
        InitializeCrowd();
        targetCrowdAmountLeft = currentCrowdAmountLeft;
        targetCrowdAmountRight = currentCrowdAmountRight;
    }

    void InitializeCrowd()
    {
        // Clear any existing characters on left
        foreach (GameObject character in CharactersLeft)
        {
            Destroy(character);
        }
        CharactersLeft.Clear();

        // Clear any existing characters on right
        foreach (GameObject character in CharactersRight)
        {
            Destroy(character);
        }
        CharactersRight.Clear();

        // Instantiate characters on left side up to the current crowd amount
        for (int i = 0; i < currentCrowdAmountLeft; i++)
        {
            GameObject crowdLeft = Instantiate(CharacterPrefab, new Vector3(Random.Range(0, leftBound), Random.Range(0f, height), 0), Quaternion.identity, CharactersParent.transform);
            CharactersLeft.Add(crowdLeft);
        }

        // Instantiate characters on right side up to the current crowd amount
        for (int i = 0; i < currentCrowdAmountRight; i++)
        {
            GameObject crowdRight = Instantiate(CharacterPrefab, new Vector3(Random.Range(rightBound, 1920f), Random.Range(0f, height), 0), Quaternion.identity, CharactersParent.transform);
            CharactersRight.Add(crowdRight);
        }
    }

    void Update()
    {
        // Adjust crowd properties based on total_error
        CrowdController();

        // Move characters around their initial positions to simulate crowd behavior
        MoveCharacters(CharactersLeft);
        MoveCharacters(CharactersRight);
    }

    void MoveCharacters(List<GameObject> characters)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            // Simulate movement behavior
            Vector3 newPosition = characters[i].transform.position;
            newPosition.x += Mathf.Sin(Time.time * 0.5f + i) * 0.1f; // Example movement based on index
            characters[i].transform.position = newPosition;
        }
    }

    void CrowdController()
    {
        // Assume total_error ranges from 0 to 5 (adjust as needed)
        float totalError = (float)SoundManager.total_error; // Replace with actual total_error from SoundManager

        // Calculate target crowd amount based on total_error for left side
        if (totalError > 6f)
        {
            // Decrease crowd amount faster as total_error increases beyond 2.8 for left side
            float decreaseSpeedLeft = Mathf.Lerp(crowdChangeSpeed, 0.2f, (totalError - 4f) / 4f);
            targetCrowdAmountLeft -= decreaseSpeedLeft * Time.deltaTime * 100; // Adjust coefficient for speed

            // Clamp target crowd amount to avoid going below 0
            targetCrowdAmountLeft = Mathf.Max(targetCrowdAmountLeft, 0);
        }
        else
        {
            // Increase crowd amount smoothly as total_error decreases from 2.8 for left side
            float increaseSpeedLeft = Mathf.Lerp(crowdChangeSpeed, 0.2f, totalError / 4f);
            targetCrowdAmountLeft += increaseSpeedLeft * Time.deltaTime * 100; // Adjust coefficient for speed

            // Clamp target crowd amount to maxCrowdAmount
            targetCrowdAmountLeft = Mathf.Min(targetCrowdAmountLeft, maxCrowdAmount);
        }

        // Calculate target crowd amount based on total_error for right side (inverted logic)
        if (totalError >= 4f)
        {
            // Decrease crowd amount faster as total_error increases beyond 2.8 for right side
            float decreaseSpeedRight = Mathf.Lerp(crowdChangeSpeed, 0.2f, (totalError - 4f) / 4f);
            targetCrowdAmountRight -= decreaseSpeedRight * Time.deltaTime * 100; // Adjust coefficient for speed

            // Clamp target crowd amount to avoid going below 0
            targetCrowdAmountRight = Mathf.Max(targetCrowdAmountRight, 0);
        }
        else
        {
            // Increase crowd amount smoothly as total_error decreases from 2.8 for right side
            float increaseSpeedRight = Mathf.Lerp(crowdChangeSpeed, 0.2f, totalError / 4f);
            targetCrowdAmountRight += increaseSpeedRight * Time.deltaTime * 100; // Adjust coefficient for speed

            // Clamp target crowd amount to maxCrowdAmount
            targetCrowdAmountRight = Mathf.Min(targetCrowdAmountRight, maxCrowdAmount);
        }

        // Round target crowd amount to nearest integer
        currentCrowdAmountLeft = Mathf.RoundToInt(targetCrowdAmountLeft);
        currentCrowdAmountRight = Mathf.RoundToInt(targetCrowdAmountRight);

        // Update crowd on left side if there's a change in crowd amount
        if (currentCrowdAmountLeft != CharactersLeft.Count)
        {
            UpdateCrowd(CharactersLeft, currentCrowdAmountLeft);
        }

        // Update crowd on right side if there's a change in crowd amount
        if (currentCrowdAmountRight != CharactersRight.Count)
        {
            UpdateCrowd(CharactersRight, currentCrowdAmountRight);
        }
    }

    void UpdateCrowd(List<GameObject> characters, int targetCrowdAmount)
    {
        // If current crowd amount is less than the number of active characters, deactivate excess characters
        if (targetCrowdAmount < characters.Count)
        {
            for (int i = characters.Count - 1; i >= targetCrowdAmount; i--)
            {
                GameObject characterToRemove = characters[i];
                characters.RemoveAt(i);
                Destroy(characterToRemove);
            }
        }
        else if (targetCrowdAmount > characters.Count)
        {
            // If current crowd amount is greater than the number of active characters, instantiate additional characters
            int charactersToAdd = targetCrowdAmount - characters.Count;
            for (int i = 0; i < charactersToAdd; i++)
            {
                GameObject crowdCharacter = Instantiate(CharacterPrefab, GetRandomSpawnPosition(), Quaternion.identity, CharactersParent.transform);
                characters.Add(crowdCharacter);
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Randomly determine a spawn position within the designated areas for both sides
        float xSpawn = Random.Range(0f, leftBound); // Adjust as needed for left side
        float ySpawn = Random.Range(0f, height); // Adjust as needed for both sides
        if (Random.Range(0, 2) == 0)
        {
            xSpawn = Random.Range(rightBound, 1920f); // Adjust as needed for right side
        }
        return new Vector3(xSpawn, ySpawn, 0);
    }
}
