using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : MonoBehaviour
{
    //The player
    private GameObject player;
    //The side the king is looking
    public bool lookingRight;
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
    public bool attacking;
    //A float to see when was the last attack of the enemy
    private float lastAttack;
    //float to see the damage the enemy is receiving
    public float damage;
    //The prefab of the first attack
    public GameObject firstAttackPrefab;
    //The variable that we are going to use for the first attack
    private GameObject firstAttack;
    //The prefab of the second attack
    public GameObject secondAttackPrefab;
    //The variable that we are going to use for the second attack
    private GameObject secondAttack;
    //The prefab of the third attack
    public GameObject thirdAttackPrefab;
    //The variable that we are going to use for the third attack
    private GameObject thirdAttack;
    //A float to see the health of the enemy
    private float health;
    //A boolean to see if the enemy is fighting
    private bool fighting;
    //A float to see when was the first damage of the combo dealt
    public float lastDamage;
    //An int to see the number of the strikes of the combo (player)
    public int combo;
    //The time the shielding started
    private float shieldTime;
    //A vector3 to save the starting pos
    public Vector3 startPos;
    //A boolean to save where is looking the bandit when spawned. 0->left, 1-> right
    public int looking;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        moving = false;
        prevPos = gameObject.transform.position;
        lastAttack = -1.5f;
        damage = 0.0f;
        health = 120.0f;
        fighting = false;
        lastDamage = Time.fixedTime - 3.0f;
        shieldTime = Time.fixedTime - 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerMovement>().sleeping && gameObject.transform.position != startPos)
        {
            health = 120.0f;
            gameObject.GetComponent<Animator>().SetBool("isDead", false);
            gameObject.transform.position = startPos;
            prevPos = gameObject.transform.position;
            attacking = false;
            gameObject.GetComponent<Animator>().SetBool("isTakingDamage", false);
            gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
            fighting = false;
            if (!lookingRight && looking != 0) Flip();
            else if (lookingRight && looking != 1) Flip();

        }
        else
        {
            if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < 8.0f && Mathf.Abs(player.transform.position.y - gameObject.transform.position.y) < 2.3f && !fighting && !player.GetComponent<PlayerMovement>().talking && !gameObject.GetComponent<Animator>().GetBool("isDead"))
            {
                fighting = true;
                player.GetComponent<PlayerMovement>().attacked += 1;
            }
            else if ((Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) >= 8.0f || Mathf.Abs(player.transform.position.y - gameObject.transform.position.y) >= 2.3f) && fighting && !gameObject.GetComponent<Animator>().GetBool("isDead") && !player.GetComponent<PlayerMovement>().talking)
            {
                fighting = false;
                player.GetComponent<PlayerMovement>().attacked -= 1;
            }
            else if (player.GetComponent<PlayerMovement>().talking && fighting)
            {
                player.GetComponent<PlayerMovement>().attacked -= 1;
                fighting = false;
            }
            else if (player.GetComponent<PlayerMovement>().talking) fighting = false;

            if (!moving && !gameObject.GetComponent<Animator>().GetBool("isDead"))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                gameObject.transform.position = new Vector2(prevPos.x, gameObject.transform.position.y);
            }
            else if (gameObject.GetComponent<Animator>().GetBool("isDead"))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                gameObject.transform.position = prevPos;
            }
            prevPos = gameObject.transform.position;
        }        
    }

    void FixedUpdate()
    {
        if (health <= 0.0f && !gameObject.GetComponent<Animator>().GetBool("isDead"))
        {
            gameObject.GetComponent<Animator>().SetBool("isDead", true);
            combo = 0;
            if(fighting) player.GetComponent<PlayerMovement>().attacked -= 1;
        }
        if (damage > 0.0f)
        {
            health -= damage;
            damage = 0.0f;
        }
        if (combo == 6 && health > 0.0)
        {            
            combo = 0;
            gameObject.GetComponent<Animator>().SetBool("isTakingDamage", false);
            gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
            gameObject.GetComponent<Animator>().SetBool("isRolling", true);
        }
        if(gameObject.GetComponent<Animator>().GetBool("isShielding") && Time.fixedTime - shieldTime > 1.5f)
        {
            gameObject.GetComponent<Animator>().SetBool("isShielding", false);
        }
        if (fighting && !gameObject.GetComponent<Animator>().GetBool("isRolling") && health > 0.0 && !gameObject.GetComponent<Animator>().GetBool("isShielding") && !gameObject.GetComponent<Animator>().GetBool("isTakingDamage"))
        {
            if (player.transform.position.x < gameObject.transform.position.x && !attacking && (Time.fixedTime - lastAttack > 1.5f))
            {
                if (lookingRight) Flip();
                if (gameObject.transform.position.x - player.transform.position.x > 1.25f)
                {
                    moving = true;
                    targetVelocity = new Vector2(-6f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    moving = false;
                    targetVelocity = new Vector2(0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                    gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
                    attacking = true;
                }
            }
            else if (player.transform.position.x >= gameObject.transform.position.x && !attacking && (Time.fixedTime - lastAttack > 1.5f))
            {
                if (!lookingRight) Flip();
                if (player.transform.position.x - gameObject.transform.position.x > 1.25f)
                {
                    moving = true;
                    targetVelocity = new Vector2(6f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }
                else 
                {
                    moving = false;
                    targetVelocity = new Vector2(0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                    gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
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
        lookingRight = !lookingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //function to instantiate the first damage collider
    public void firstComboAttack()
    {
        if (lookingRight) firstAttack = Instantiate(firstAttackPrefab, new Vector2(transform.position.x + 0.9764156f, transform.position.y + 0.4553868f), Quaternion.identity);
        else firstAttack = Instantiate(firstAttackPrefab, new Vector2(transform.position.x - 0.9764156f, transform.position.y + 0.4553868f), Quaternion.identity);
    }

    //function to instantiate the second damage collider
    public void secondComboAttack()
    {
        if (lookingRight) secondAttack = Instantiate(secondAttackPrefab, new Vector2(transform.position.x + 1.00466f, transform.position.y + 0.1953509f), Quaternion.identity);
        else secondAttack = Instantiate(secondAttackPrefab, new Vector2(transform.position.x - 1.00466f, transform.position.y + 0.1953509f), Quaternion.identity);
    }

    //function to instantiate the third damage collider
    public void thirdComboAttack()
    {
        if (lookingRight) thirdAttack = Instantiate(thirdAttackPrefab, new Vector2(transform.position.x + 1.204221f, transform.position.y + 0.19383f), Quaternion.identity);
        else thirdAttack = Instantiate(thirdAttackPrefab, new Vector2(transform.position.x - 1.204221f, transform.position.y + 0.19383f), Quaternion.identity);
    }

    //function to end the attack
    public void endAttack()
    {
        gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
        attacking = false;
        lastAttack = Time.fixedTime;
        gameObject.GetComponent<Animator>().SetBool("isShielding", true);
        shieldTime = Time.fixedTime;
        if (player.transform.position.x < gameObject.transform.position.x && lookingRight) Flip();
        else if (player.transform.position.x > gameObject.transform.position.x && !lookingRight) Flip();
    }

    //function to end the damage animation
    public void endDamage()
    {
        gameObject.GetComponent<Animator>().SetBool("isTakingDamage", false);
        gameObject.GetComponent<Animator>().SetBool("isShielding", false);
        gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
        attacking = false;
    }
    //function to start the roll movement
    public void startRoll()
    {
        if (player.transform.position.x < gameObject.transform.position.x)
        {
            moving = true;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-4000, 0));
        }
        else
        {
            moving = true;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(4000, 0));
        }
    }
    //function to end the roll animation
    public void endRoll()
    {
        gameObject.GetComponent<Animator>().SetBool("isRolling", false);
        gameObject.GetComponent<Animator>().SetBool("isShielding", false);
        gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
        attacking = false;
        moving = false;
    }

    //function to give exp to the player
    public void giveExp()
    {
        PlayerPrefs.SetInt("exp", PlayerPrefs.GetInt("exp") + (int)(100.0f * (1.0f + (PlayerPrefs.GetInt("expGainingLevel") - 1.0f) * 0.1f)));
    }

    
}
