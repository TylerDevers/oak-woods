using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    // length and start position of sprite/background image.
    float length, startpos;
    // How much parallax will we apply?
    public float parallaxEffect;
    public new GameObject camera;




    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x; // gets width of individual sprite
    }

    void Update()
    {   
        // get distance moved relative to the camera
        float temp = camera.transform.position.x * ( 1 - parallaxEffect);
        // get distance camera has moved from start pos.
         float distance = camera.transform.position.x * parallaxEffect;
        // move camera
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        // repeat background as camera moves.
        // if temp distance is greater than startpos + the length of the sprite, 
        if (temp > startpos + length) startpos += length; 
        else if (temp < startpos -1) startpos -= length;

     
             
    }
}
