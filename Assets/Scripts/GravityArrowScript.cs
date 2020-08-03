using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityArrowScript : MonoBehaviour
{
    //The player
    private GameObject player;
    //The gravity arrows
    private GameObject gravityDown;
    private GameObject gravityUp;
    private GameObject gravityLeft;
    private GameObject gravityRight;
    //The white circle around the player
    private GameObject white;

    void Start()
    {
        //We find everything
        player = GameObject.Find("Player");
        gravityDown = GameObject.Find("Side0");
        gravityUp = GameObject.Find("Side1");
        gravityLeft = GameObject.Find("Side2");
        gravityRight = GameObject.Find("Side3");
        white = GameObject.Find("ChangeGravity");
    }


    void Update()
    {
        //We activate the arrows and the white circle when the player is changing gravity
        if (player.GetComponent<PlayerMovement>().changingGravity && !player.GetComponent<PlayerMovement>().rotating)
        {
            white.SetActive(true);
            gameObject.transform.position = player.transform.position;
            //We put the arrows different lengths depending on the gravity in that direction and if it is 0 the arrow disappears
            if (player.GetComponent<PlayerMovement>().prevGravityDown > 0.0f)
            {
                gravityDown.transform.GetComponent<RectTransform>().anchorMin = new Vector2(0.46925f, 0.4f - 0.0125f* player.GetComponent<PlayerMovement>().prevGravityDown);
                gravityDown.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                if (player.GetComponent<PlayerMovement>().prevGravityDown <= 1.0f) gravityDown.GetComponent<Image>().color = new Color(1.0f - player.GetComponent<PlayerMovement>().prevGravityDown, 1.0f, 1.0f - player.GetComponent<PlayerMovement>().prevGravityDown);
                else if (player.GetComponent<PlayerMovement>().prevGravityDown <= 3.05f) gravityDown.GetComponent<Image>().color = new Color((player.GetComponent<PlayerMovement>().prevGravityDown - 1.0f) / 2.0f, 1.0f, 0.0f);
                else gravityDown.GetComponent<Image>().color = new Color(1.0f - ((player.GetComponent<PlayerMovement>().prevGravityDown - 3.1f) / 10.0f), 0.0f, 0.0f);
                gravityDown.SetActive(true);
            }
            else gravityDown.SetActive(false);
            if (player.GetComponent<PlayerMovement>().prevGravityUp > 0.0f)
            {
                gravityUp.transform.GetComponent<RectTransform>().anchorMax = new Vector2(0.53075f, 0.6f + 0.0125f * player.GetComponent<PlayerMovement>().prevGravityUp);
                gravityUp.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                if (player.GetComponent<PlayerMovement>().prevGravityUp <= 1.0f) gravityUp.GetComponent<Image>().color = new Color(1.0f - player.GetComponent<PlayerMovement>().prevGravityUp, 1.0f, 1.0f - player.GetComponent<PlayerMovement>().prevGravityUp);
                else if (player.GetComponent<PlayerMovement>().prevGravityUp <= 3.05f) gravityUp.GetComponent<Image>().color = new Color((player.GetComponent<PlayerMovement>().prevGravityUp - 1.0f) / 2.0f, 1.0f, 0.0f);
                else gravityUp.GetComponent<Image>().color = new Color(1.0f - ((player.GetComponent<PlayerMovement>().prevGravityUp - 3.1f) / 10.0f), 0.0f, 0.0f);
                gravityUp.SetActive(true);
            }
            else gravityUp.SetActive(false);
            if (player.GetComponent<PlayerMovement>().prevGravityLeft > 0.0f)
            {
                gravityLeft.transform.GetComponent<RectTransform>().anchorMin = new Vector2(0.4f - 0.0125f * player.GetComponent<PlayerMovement>().prevGravityLeft, 0.46925f);
                gravityLeft.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                if (player.GetComponent<PlayerMovement>().prevGravityLeft <= 1.0f) gravityLeft.GetComponent<Image>().color = new Color(1.0f - player.GetComponent<PlayerMovement>().prevGravityLeft, 1.0f, 1.0f - player.GetComponent<PlayerMovement>().prevGravityLeft);
                else if (player.GetComponent<PlayerMovement>().prevGravityLeft <= 3.05f) gravityLeft.GetComponent<Image>().color = new Color((player.GetComponent<PlayerMovement>().prevGravityLeft - 1.0f) / 2.0f, 1.0f, 0.0f);
                else gravityLeft.GetComponent<Image>().color = new Color(1.0f - ((player.GetComponent<PlayerMovement>().prevGravityLeft - 3.1f) / 10.0f), 0.0f, 0.0f);
                gravityLeft.SetActive(true);
            }
            else gravityLeft.SetActive(false);
            if (player.GetComponent<PlayerMovement>().prevGravityRight > 0.0f)
            {
                gravityRight.transform.GetComponent<RectTransform>().anchorMax = new Vector2(0.6f + 0.0125f * player.GetComponent<PlayerMovement>().prevGravityRight, 0.53075f);
                gravityRight.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                if (player.GetComponent<PlayerMovement>().prevGravityRight <= 1.0f) gravityRight.GetComponent<Image>().color = new Color(1.0f - player.GetComponent<PlayerMovement>().prevGravityRight, 1.0f, 1.0f - player.GetComponent<PlayerMovement>().prevGravityRight);
                else if (player.GetComponent<PlayerMovement>().prevGravityRight <= 3.05f) gravityRight.GetComponent<Image>().color = new Color((player.GetComponent<PlayerMovement>().prevGravityRight - 1.0f) / 2.0f, 1.0f, 0.0f);
                else gravityRight.GetComponent<Image>().color = new Color(1.0f - ((player.GetComponent<PlayerMovement>().prevGravityRight - 3.1f) / 10.0f), 0.0f, 0.0f);
                gravityRight.SetActive(true);
            }
            else gravityRight.SetActive(false);
        }
        else
        {
            white.SetActive(false);
            gravityDown.SetActive(false);
            gravityUp.SetActive(false);
            gravityLeft.SetActive(false);
            gravityRight.SetActive(false);
        }

    }
}
