using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    private Color tmp;


    void FixedUpdate()
    {
        if (gameObject.GetComponent<SpriteRenderer>().color.a > 0) gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.1f);
        else Destroy(gameObject);
    }
}
