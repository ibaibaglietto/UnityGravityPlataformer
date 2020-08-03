using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgroundScrpit : MonoBehaviour
{
    //Multipier for the parallax efect
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    //boolean to save if we want to repeat the background
    [SerializeField] private bool repeat;

    //The camera transform
    private Transform cameraTransform;
    //The previous camera position
    private Vector3 prevCameraPosition;
    //The size of the texture unit
    private float textureUnitSize;

    void Awake()
    {
        //We find everything and save it
        cameraTransform = Camera.main.transform;
        prevCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSize = texture.width / sprite.pixelsPerUnit;
    }

    //We move the background image depending on the camera movement. The movement will depend on the multipier.
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
