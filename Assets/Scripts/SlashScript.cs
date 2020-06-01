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
    //The damage of the player
    private float damageDealt;

    private void Start()
    {
        king = GameObject.Find("King");
        player = GameObject.Find("Player");
        parryTime = Time.fixedTime - 0.6f;
        damageDealt = Mathf.Sqrt(100 * PlayerPrefs.GetInt("dealtDamageLevel")) + 10;
    }

    public void endSlash()
    {
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "King")
        {
            king.GetComponent<KingScript>().damage = damageDealt;
        }
        if (collision.tag == "HeavyBandit")
        {
            Debug.Log(damageDealt);
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
            collision.GetComponent<HeavyBanditScript>().damage = damageDealt;
        }
        if (collision.tag == "Knight")
        {
            Debug.Log(damageDealt);
            if (!collision.GetComponent<Animator>().GetBool("isShielding") && !collision.GetComponent<Animator>().GetBool("isRolling") && Time.fixedTime - parryTime > 0.5f)
            {
                collision.GetComponent<Animator>().SetBool("isTakingDamage", true);
                collision.GetComponent<KnightScript>().damage = damageDealt;
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
                collision.GetComponent<KnightScript>().damage = damageDealt;
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
                collision.GetComponent<KnightScript>().damage = damageDealt;
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
