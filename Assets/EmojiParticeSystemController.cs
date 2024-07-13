using UnityEngine;

public class ParticleSpriteRandomizer : MonoBehaviour
{
    public Sprite[] sprites; // All sprites in the sprite sheet
    private ParticleSystem particleSystem;
    private ParticleSystem.TextureSheetAnimationModule textureSheetAnimation;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        textureSheetAnimation = particleSystem.textureSheetAnimation;
        textureSheetAnimation.mode = ParticleSystemAnimationMode.Sprites;

        UpdateSprites();
    }

    void Update()
    {
        // Call UpdateSprites to adjust based on total_error
        UpdateSprites();
    }

    void UpdateSprites()
    {
        float total_error = (float)SoundManager.total_error; // Replace with the actual way to get total_error from SoundManager

        // Calculate the probability weight for worse (0-3) and best (4-7) sprites
        float weightWorse = Mathf.Clamp01(total_error / 8f);
        float weightBest = 1 - weightWorse;

        // Clear existing sprites
        for (int i = 0; i < textureSheetAnimation.spriteCount; i++)
        {
            textureSheetAnimation.RemoveSprite(i);
        }

        // Add sprites with adjusted probabilities
        for (int i = 0; i < 4; i++)
        {
            if (Random.value < weightWorse)
            {
                textureSheetAnimation.AddSprite(sprites[i]);
            }
        }

        for (int i = 4; i < 8; i++)
        {
            if (Random.value < weightBest)
            {
                textureSheetAnimation.AddSprite(sprites[i]);
            }
        }

        // Ensure at least one sprite is added from each range
        if (textureSheetAnimation.spriteCount == 0)
        {
            if (weightWorse > weightBest)
            {
                textureSheetAnimation.AddSprite(sprites[Random.Range(0, 4)]);
            }
            else
            {
                textureSheetAnimation.AddSprite(sprites[Random.Range(4, 8)]);
            }
        }

        // Set the start frame to random between the number of added sprites
        textureSheetAnimation.startFrame = new ParticleSystem.MinMaxCurve(0, textureSheetAnimation.spriteCount);
    }
}
