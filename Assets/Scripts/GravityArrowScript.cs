using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityArrowScript : MonoBehaviour
{
    private GameObject player;
    private GameObject gravityDown;
    private GameObject gravityUp;
    private GameObject gravityLeft;
    private GameObject gravityRight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gravityDown = GameObject.Find("Side0");
        gravityUp = GameObject.Find("Side1");
        gravityLeft = GameObject.Find("Side2");
        gravityRight = GameObject.Find("Side3");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerMovement>().changingGravity && !player.GetComponent<PlayerMovement>().rotating)
        {
            gameObject.transform.position = player.transform.position;
            if (player.GetComponent<PlayerMovement>().prevGravityDown > 0.0f)
            {
                gravityDown.SetActive(true);
            }
            else gravityDown.SetActive(false);
            if (player.GetComponent<PlayerMovement>().prevGravityUp > 0.0f)
            {
                gravityUp.SetActive(true);
            }
            else gravityUp.SetActive(false);
            if (player.GetComponent<PlayerMovement>().prevGravityLeft > 0.0f)
            {
                gravityLeft.SetActive(true);
            }
            else gravityLeft.SetActive(false);
            if (player.GetComponent<PlayerMovement>().prevGravityRight > 0.0f)
            {
                gravityRight.SetActive(true);
            }
            else gravityRight.SetActive(false);
        }
        else
        {
            gravityDown.SetActive(false);
            gravityUp.SetActive(false);
            gravityLeft.SetActive(false);
            gravityRight.SetActive(false);
        }

    }
}
