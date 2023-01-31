using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Set backgrounds to tiled. Stretch to cover at least 3 "screens" worth of space.
public class ParallaxAlt : MonoBehaviour
{
    
    // Vector 2 so we can apply to y axis if needed
    [SerializeField] Vector2 parallaxEffectMultiplier = new Vector2(1, 1);

    // Camera coordinates
    Transform cameraTransform;
    Vector3 lastCameraPosition;
    float textureUnitSizeX;

    private void Start() {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;

        // get texture size to enable infinite backgroundss
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;

    }


    // in late update to ensure camera coordinates have been calculated
    private void LateUpdate() {
        
        // Calculate change in camera position from start
        Vector3 deltaCameraMovement = cameraTransform.position - lastCameraPosition;

        // Move background sprites, apply parallax
        transform.position += new Vector3(deltaCameraMovement.x * parallaxEffectMultiplier.x,
            deltaCameraMovement.y * parallaxEffectMultiplier.y, transform.position.z);

        // update last position of camera
        lastCameraPosition = cameraTransform.position;


        //Infinite scrolling of background
        /* if the distance between the camera's position and the backgrounds position is greater than 
            the texture unit size, move background to new location. ABS takes care of both fore and back movement.
        */
        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX) {
            float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector2( cameraTransform.position.x + offsetPositionX, transform.position.y);
        }

    }






















}
