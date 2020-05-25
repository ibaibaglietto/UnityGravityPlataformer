using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashScript : MonoBehaviour
{
    //The boss
    private GameObject king;
    //The player
    private GameObject player;
    //The time the enemy parried
    public float parryTime;

    private void Start()
    {
        king = GameObject.Find("King");
        player = GameObject.Find("Player");
        parryTime = Time.fixedTime - 0.6f;
    }

    public void endSlash()
    {
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == king.GetComponent<Collider2D>())
        {
            king.GetComponent<KingScript>().damage = 20.0f;
        }
        if (collision.tag == "HeavyBandit")
        {
            if(Time.fixedTime - collision.GetComponent<HeavyBanditScript>().firstDamage < 3.0f)
            {
                collision.GetComponent<HeavyBanditScript>().combo += 1;
            }
            else
            {
                collision.GetComponent<HeavyBanditScript>().firstDamage = Time.fixedTime;
                collision.GetComponent<HeavyBanditScript>().combo = 1;
            }
            if(!collision.GetComponent<Animator>().GetBool("IsJumping")) collision.GetComponent<Animator>().SetBool("TakeDamage", true);
            collision.GetComponent<HeavyBanditScript>().damage = 20.0f;
        }
        if (collision.tag == "Knight")
        {
            if (!collision.GetComponent<Animator>().GetBool("isShielding") && !collision.GetComponent<Animator>().GetBool("isRolling") && Time.fixedTime - parryTime > 0.5f)
            {
                collision.GetComponent<Animator>().SetBool("isTakingDamage", true);
                collision.GetComponent<KnightScript>().damage = 20.0f;
                if (Time.fixedTime - collision.GetComponent<KnightScript>().firstDamage < 3.0f)
                {
                    collision.GetComponent<KnightScript>().combo += 1;
                }
                else
                {
                    collision.GetComponent<KnightScript>().firstDamage = Time.fixedTime;
                    collision.GetComponent<KnightScript>().combo = 1;
                }
            }
            else if (collision.GetComponent<Animator>().GetBool("isShielding") && !collision.GetComponent<KnightScript>().lookingRight && player.transform.position.x > collision.transform.position.x && !collision.GetComponent<Animator>().GetBool("isRolling"))
            {
                collision.GetComponent<Animator>().SetBool("isTakingDamage", true);
                collision.GetComponent<Animator>().SetBool("isShielding", false);
                collision.GetComponent<KnightScript>().damage = 20.0f;
                if (Time.fixedTime - collision.GetComponent<KnightScript>().firstDamage < 3.0f)
                {
                    collision.GetComponent<KnightScript>().combo += 1;
                }
                else
                {
                    collision.GetComponent<KnightScript>().firstDamage = Time.fixedTime;
                    collision.GetComponent<KnightScript>().combo = 1;
                }
            }
            else if (collision.GetComponent<Animator>().GetBool("isShielding") && collision.GetComponent<KnightScript>().lookingRight && player.transform.position.x < collision.transform.position.x && !collision.GetComponent<Animator>().GetBool("isRolling"))
            {
                collision.GetComponent<Animator>().SetBool("isTakingDamage", true);
                collision.GetComponent<Animator>().SetBool("isShielding", false);
                collision.GetComponent<KnightScript>().damage = 20.0f;
                if (Time.fixedTime - collision.GetComponent<KnightScript>().firstDamage < 3.0f)
                {
                    collision.GetComponent<KnightScript>().combo += 1;
                }
                else
                {
                    collision.GetComponent<KnightScript>().firstDamage = Time.fixedTime;
                    collision.GetComponent<KnightScript>().combo = 1;
                }
            }
            else if (collision.GetComponent<Animator>().GetBool("isShielding") && !collision.GetComponent<Animator>().GetBool("isRolling"))
            {
                parryTime = Time.fixedTime;
                collision.GetComponent<Animator>().SetBool("isTakingDamage", false);
                collision.GetComponent<KnightScript>().damage = 0.0f;
                collision.GetComponent<Animator>().SetBool("isShielding", false);
                collision.GetComponent<Animator>().SetBool("isAttacking", true);                
            }
        }
    }
}
