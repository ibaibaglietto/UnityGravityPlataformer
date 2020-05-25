using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundScrpit : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    [SerializeField] private bool repeat;

    private Transform cameraTransform;
    private Vector3 prevCameraPosition;
    private float textureUnitSize;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        prevCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSize = texture.width / sprite.pixelsPerUnit;
    }

    void LateUpdate()
    {
        Vector3 deltaMov = cameraTransform.position - prevCameraPosition;
        transform.position += new Vector3(deltaMov.x * parallaxEffectMultiplier.x, deltaMov.y * parallaxEffectMultiplier.y, 0);
        prevCameraPosition = cameraTransform.position;

        if ( (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSize) && repeat)
        {
            float offset = (cameraTransform.position.x - transform.position.x) % textureUnitSize;
            transform.position = new Vector3(cameraTransform.position.x + offset, transform.position.y);
        }
    }
}
