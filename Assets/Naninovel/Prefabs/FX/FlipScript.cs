using System.Collections;
using UnityEngine;

public class CoinFlip : MonoBehaviour
{
    public float totalFlipDuration = 2.0f;  // Total duration of the flip animation in seconds
    public int rotations = 4;               // Number of full rotations in each flip
    public Sprite headsSprite;
    public Sprite tailsSprite;

    private SpriteRenderer spriteRenderer;
    private bool showHeads;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        showHeads = true;  // Start with heads
        StartCoroutine(ContinuousFlipAnimation());  // Start flipping continuously
    }

    private IEnumerator ContinuousFlipAnimation()
    {
        while (true) // Infinite loop for continuous flipping
        {
            float elapsedTime = 0f;
            float rotationAnglePerSecond = 360f * rotations / totalFlipDuration; // Rotation speed to achieve 4 full rotations in 2 seconds

            // Toggle heads/tails side
            showHeads = !showHeads;
            Sprite finalSprite = showHeads ? headsSprite : tailsSprite;

            // Flip animation loop
            while (elapsedTime < totalFlipDuration)
            {
                float rotationAngle = rotationAnglePerSecond * Time.deltaTime;
                transform.Rotate(rotationAngle, 0, 0); // Rotate around x-axis for flip effect
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Set final orientation and sprite based on heads/tails
            transform.rotation = Quaternion.identity; // Reset rotation
            spriteRenderer.sprite = finalSprite;

            yield return new WaitForSeconds(0.1f); // Small delay between flips (optional)
        }
    }
}
