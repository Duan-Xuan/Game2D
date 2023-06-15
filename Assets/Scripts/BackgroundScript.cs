using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float speed = 0.1f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        spriteRenderer.material.mainTextureOffset = new Vector2(Time.time * speed, 0f);
    }
}