using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //The player
    private GameObject player;    
    //The offset we are going to apply
    private Vector3 offset;
    //The shake duration
    private float shakeDuration = 0f;

    //The magnitude of the shake
    private float shakeMagnitude = 0.025f;

    //The speed the shaking effect will dissapear
    private float dampingSpeed = 1.0f;

    private float xObj, yObj, xAct, yAct;

    //The initial position of the gameobject
    Vector3 initialPosition;

    //The side of the gravity before the player started to change it
    private int lastGravity;




   //We initialize the offset
    void Start()
    {
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;
    }

    
    void Update()
    {
        xAct = transform.position.x;
        yAct = transform.position.y;
        //We make the camera shake
        if (shakeDuration > 0)
        {
            transform.localPosition = new Vector3(transform.position.x, player.transform.position.y + offset.y, transform.position.z) + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        //If it doesn't have to shake we put the camera on one position depending on the gravity
        else
        {
            shakeDuration = 0f;
            //We want the camera to follow the player all the time, but it changes its position depending on the gravity
            //To achieve this, we make 4 diferent positions for the camera
            //the camera follows the player without any change if we aren't changing the gravity
            //if the player starts to change the gravity it keeps following without changes
            //when the player confirms the changes the camera finds its new position, but we will move it smoothly on the fixed update
            if (((!player.GetComponent<PlayerMovement>().changingGravity || (player.GetComponent<PlayerMovement>().changingGravity && player.GetComponent<PlayerMovement>().rotating)) && player.GetComponent<PlayerMovement>().gravity == 0) || (player.GetComponent<PlayerMovement>().changingGravity && !player.GetComponent<PlayerMovement>().rotating && lastGravity == 0))
            {
                xObj = player.transform.position.x + offset.x;
                yObj = player.transform.position.y + offset.y;
            }
            else if (((!player.GetComponent<PlayerMovement>().changingGravity || (player.GetComponent<PlayerMovement>().changingGravity && player.GetComponent<PlayerMovement>().rotating)) && player.GetComponent<PlayerMovement>().gravity == 1) || (player.GetComponent<PlayerMovement>().changingGravity && !player.GetComponent<PlayerMovement>().rotating && lastGravity == 1))
            {
                xObj = player.transform.position.x + offset.x;
                yObj = player.transform.position.y - offset.y;
            }
            else if (((!player.GetComponent<PlayerMovement>().changingGravity || (player.GetComponent<PlayerMovement>().changingGravity && player.GetComponent<PlayerMovement>().rotating)) && player.GetComponent<PlayerMovement>().gravity == 2) || (player.GetComponent<PlayerMovement>().changingGravity && !player.GetComponent<PlayerMovement>().rotating && lastGravity == 2))
            {
                xObj = player.transform.position.x + offset.y * 3.3f;
                yObj = player.transform.position.y + offset.x;
            }
            else if (((!player.GetComponent<PlayerMovement>().changingGravity || (player.GetComponent<PlayerMovement>().changingGravity && player.GetComponent<PlayerMovement>().rotating)) && player.GetComponent<PlayerMovement>().gravity == 3) || (player.GetComponent<PlayerMovement>().changingGravity && !player.GetComponent<PlayerMovement>().rotating && lastGravity == 3))
            {
                xObj = player.transform.position.x - offset.y * 3.3f;
                yObj = player.transform.position.y + offset.x;
            } 
        }

        if (!player.GetComponent<PlayerMovement>().changingGravity) lastGravity = player.GetComponent<PlayerMovement>().gravity;
        if (!player.GetComponent<PlayerMovement>().rotating && !player.GetComponent<PlayerMovement>().rotated) transform.position = new Vector3(xObj, yObj, transform.position.z);

    }

    //We move the camera smoothly to the new position
    private void FixedUpdate()
    {
        if (player.GetComponent<PlayerMovement>().rotating)
        {
            if (xAct > (xObj + 0.25f)) xAct -= 0.25f;
            else if (xAct < ((xObj - 0.25f))) xAct += 0.25f;
            else xAct = xObj;

            if (yAct > (yObj + 0.25f)) yAct -= 0.25f;
            else if (yAct < ((yObj - 0.25f))) yAct += 0.25f;
            else yAct = yObj;

            transform.position = new Vector3(xAct, yAct, transform.position.z);
            if (Mathf.Abs(player.GetComponent<PlayerMovement>().rotation - player.GetComponent<Rigidbody2D>().rotation)<1.0f && xAct == xObj && yAct == yObj) player.GetComponent<PlayerMovement>().rotated = true;
        }
    }

    public void TriggerShake(float d)
    {
        shakeDuration = d;
    }

}
