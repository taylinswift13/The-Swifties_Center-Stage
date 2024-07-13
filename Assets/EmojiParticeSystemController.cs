using UnityEngine;

public class ParticleSpriteRandomizer : MonoBehaviour
{
    public Sprite[] sprites; // All sprites in the sprite sheet
    private ParticleSystem particleSystem;
    private ParticleSystem.TextureSheetAnimationModule textureSheetAnimation;
    private float lastTotalError; // Store the last recorded total_error

    // Adjust this threshold to control when sprites should update
    private float updateThreshold = 0.1f;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        textureSheetAnimation = particleSystem.textureSheetAnimation;
        textureSheetAnimation.mode = ParticleSystemAnimationMode.Sprites;

        // Initialize lastTotalError to an extreme value to ensure the sprites update initially
        lastTotalError = float.MaxValue;
        float total_error = (float)SoundManager.total_error;
        UpdateSprites(total_error);
    }

    void Update()
    {
        float total_error = (float)SoundManager.total_error; // Replace with the actual way to get total_error from SoundManager

        // Check if the change in total_error exceeds the threshold
        if (Mathf.Abs(total_error - lastTotalError) >= updateThreshold)
        {
            // Update sprites only if there's a significant change in total_error
            UpdateSprites(total_error);
            lastTotalError = total_error; // Update lastTotalError to current total_error
        }
    }

    void UpdateSprites(float total_error)
    {
        // Clear existing sprites by setting spriteCount to 0
        textureSheetAnimation.cycleCount = 0;

        if (total_error >= 7f)
        {
            // Show sprites 4-7
            for (int i = 4; i < 8; i++)
            {
                textureSheetAnimation.AddSprite(sprites[i]);
            }
        }
        else
        {
            // Show sprites 0-3
            for (int i = 0; i < 4; i++)
            {
                textureSheetAnimation.AddSprite(sprites[i]);
            }
        }

        // Set the start frame to random between the number of added sprites
        textureSheetAnimation.startFrame = new ParticleSystem.MinMaxCurve(0, textureSheetAnimation.spriteCount);
    }
}
