using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBanditScript : MonoBehaviour
{

    //The player
    private GameObject player;
    //The side the king is looking
    public bool lookingLeft;
    //A boolean to see if the enemy is moving
    public bool moving;
    //A vector to see the position of the enemy the previous frame
    private Vector2 prevPos;
    // How much to smooth out the movement
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private Vector3 m_Velocity = Vector3.zero;
    //the velocity we want the enemy to move
    private Vector3 targetVelocity;
    //A boolean to see if the enemy is attacking
    private bool attacking;
    //A float to see when was the last attack of the enemy
    private float lastAttack;
    //float to see the damage the enemy is receiving
    public float damage;
    //The prefab of the attack
    public GameObject attackPrefab;
    //The variable that we are going to use for the attack
    private GameObject attack;
    //A float to see the health of the enemy
    private float health;
    //A float to see when was the last damage of the combo dealt
    public float lastDamage;
    //An int to see the number of the strikes of the combo (player)
    public int combo;
    //A vector3 to save the starting pos
    public Vector3 startPos;
    //A boolean to save where is looking the bandit when spawned. 0->left, 1-> right
    public int looking;
    //A float to save the time that the bandit jumped
    private float jumpTime;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        moving = false;
        prevPos = gameObject.transform.position;
        lastAttack = -1.0f;
        damage = 0.0f;
        health = 120.0f;
        lastDamage = Time.fixedTime - 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerMovement>().sleeping && gameObject.transform.position != startPos)
        {
            health = 120.0f;
            gameObject.GetComponent<Animator>().SetBool("IsDead", false);
            gameObject.transform.position = startPos;
            prevPos = gameObject.transform.position;
            attacking = false;
            gameObject.GetComponent<Animator>().SetBool("TakeDamage", false);
            gameObject.GetComponent<Animator>().SetBool("IsAttacking", false);
            gameObject.GetComponent<Animator>().SetBool("IsFighting", false);
            if (lookingLeft && looking != 0) Flip();
            else if (!lookingLeft && looking != 1) Flip();
        }
        else
        {
            if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < 8.0f && Mathf.Abs(player.transform.position.y - gameObject.transform.position.y) < 2.3f && !gameObject.GetComponent<Animator>().GetBool("IsFighting") && !player.GetComponent<PlayerMovement>().talking && !gameObject.GetComponent<Animator>().GetBool("IsDead"))
            {
                gameObject.GetComponent<Animator>().SetBool("IsFighting", true);
                player.GetComponent<PlayerMovement>().attacked += 1;
            }
            else if ((Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) >= 8.0f || Mathf.Abs(player.transform.position.y - gameObject.transform.position.y) >= 2.3f) && gameObject.GetComponent<Animator>().GetBool("IsFighting") && !gameObject.GetComponent<Animator>().GetBool("IsDead") && !player.GetComponent<PlayerMovement>().talking)
            {
                gameObject.GetComponent<Animator>().SetBool("IsFighting", false);
                player.GetComponent<PlayerMovement>().attacked -= 1;
            }
            else if (player.GetComponent<PlayerMovement>().talking && gameObject.GetComponent<Animator>().GetBool("IsFighting") && !gameObject.GetComponent<Animator>().GetBool("IsDead"))
            {
                gameObject.GetComponent<Animator>().SetBool("IsFighting", false);
                player.GetComponent<PlayerMovement>().attacked -= 1;
            }

            if (!moving && !gameObject.GetComponent<Animator>().GetBool("IsDead"))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                gameObject.transform.position = new Vector2(prevPos.x, gameObject.transform.position.y);
            }
            else if (gameObject.GetComponent<Animator>().GetBool("IsDead"))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                gameObject.transform.position = prevPos;
            }
            prevPos = gameObject.transform.position;
        }
        if (health <= 0.0f && !gameObject.GetComponent<Animator>().GetBool("IsDead"))
        {
            gameObject.GetComponent<Animator>().SetBool("IsDead", true);
            combo = 0;
            if (gameObject.GetComponent<Animator>().GetBool("IsFighting")) player.GetComponent<PlayerMovement>().attacked -= 1;
        }
        if (gameObject.GetComponent<Animator>().GetBool("IsJumping") && gameObject.GetComponent<Rigidbody2D>().velocity.x == 0.0f && gameObject.GetComponent<Rigidbody2D>().velocity.y == 0.0f && (Time.realtimeSinceStartup - jumpTime) > 0.25f)
        {
            gameObject.GetComponent<Animator>().SetBool("IsJumping", false);
            attacking = false;
            moving = false;
        }
        if (damage > 0.0f)
        {
            health -= damage;
            damage = 0.0f;
        }
        if (combo > 4 && health > 0.0)
        {            
            combo = 0;
            gameObject.GetComponent<Animator>().SetBool("TakeDamage", false);
            gameObject.GetComponent<Animator>().SetBool("IsAttacking", false);
            gameObject.GetComponent<Animator>().SetBool("IsJumping", true);
            jumpTime = Time.realtimeSinceStartup;
        }
    }

    void FixedUpdate()
    {
        
        if (gameObject.GetComponent<Animator>().GetBool("IsFighting") && !gameObject.GetComponent<Animator>().GetBool("IsJumping") && health > 0.0)
        {
            if (player.transform.position.x < gameObject.transform.position.x && !attacking && (Time.fixedTime - lastAttack > 1.75f))
            {
                if (!lookingLeft) Flip();
                if (gameObject.transform.position.x - player.transform.position.x > 1.4f)
                {
                    moving = true;
                    targetVelocity = new Vector2(-6f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    moving = false;
                    targetVelocity = new Vector2(0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                    gameObject.GetComponent<Animator>().SetBool("IsAttacking", true);
                    attacking = true;
                }
            }
            else if (player.transform.position.x >= gameObject.transform.position.x && !attacking && (Time.fixedTime - lastAttack > 1.75f))
            {
                if (lookingLeft) Flip();
                if (player.transform.position.x - gameObject.transform.position.x > 1.4f)
                {
                    moving = true;
                    targetVelocity = new Vector2(6f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    moving = false;
                    targetVelocity = new Vector2(0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                    gameObject.GetComponent<Animator>().SetBool("IsAttacking", true);
                    attacking = true;
                }
            }
        }
        else targetVelocity = new Vector2(0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        gameObject.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(targetVelocity.x));
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(gameObject.GetComponent<Rigidbody2D>().velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(0.0f, -1.0f);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        lookingLeft = !lookingLeft;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //function to instantiate the damage collider
    public void startAttack()
    {
        if (!gameObject.GetComponent<Animator>().GetBool("TakeDamage"))
        {
            if (!lookingLeft) attack = Instantiate(attackPrefab, new Vector2(transform.position.x + 0.3711176f, transform.position.y + 0.3996654f), Quaternion.identity);
            else attack = Instantiate(attackPrefab, new Vector2(transform.position.x - 0.3711176f, transform.position.y + 0.3996654f), Quaternion.identity);
            lastAttack = Time.fixedTime;
        }
    }

    //function to end the attack
    public void endAttack()
    {
        gameObject.GetComponent<Animator>().SetBool("IsAttacking", false);
        attacking = false;
    }

    //function to end the damage animation
    public void endDamage()
    {
        gameObject.GetComponent<Animator>().SetBool("TakeDamage", false);
    }

    //function to give exp to the player
    public void giveExp()
    {
        PlayerPrefs.SetInt("exp", PlayerPrefs.GetInt("exp") + (int)(60.0f * (1.0f + (PlayerPrefs.GetInt("expGainingLevel") - 1.0f) * 0.1f)));
        if(PlayerPrefs.GetInt("expTutorial") == 0) PlayerPrefs.SetInt("expTutorial", 1);
    }

    //function to start the jump movement
    public void startJump()
    {
        if (player.transform.position.x < gameObject.transform.position.x)
        {
            moving = true;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(8000, 0));
        }
        else
        {
            moving = true;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-8000, 0));
        }
    }

}
