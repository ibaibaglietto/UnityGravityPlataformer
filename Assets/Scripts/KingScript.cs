using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingScript : MonoBehaviour
{
    //The player
    private GameObject player;
    //The side the king is looking
    private bool lookingRight;
    //A boolean to see if the king is moving
    public bool moving;
    //A vector to see the position of the kingthe previous frame
    private Vector2 prevPos;
    // How much to smooth out the movement
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private Vector3 m_Velocity = Vector3.zero;
    //the velocity we want the king to move
    private Vector3 targetVelocity;
    //The prefab of the combo1
    public GameObject combo1Prefab;
    //The variable that we are going to use for the combo1
    private GameObject combo1;
    //The prefab of the combo2
    public GameObject combo2Prefab;
    //The variable that we are going to use for the combo2
    private GameObject combo2;
    //The prefab of the combo2fase2
    public GameObject combo2fase2Prefab;
    //The prefab of the groundAttack
    public GameObject groundAttackPrefab;
    //The variable that we are going to use for the combo2
    private GameObject groundAttack;
    //The prefab of the groundAttackfase2
    public GameObject groundAttackFase2Prefab;
    //The number we are going to use to decide what attack to do
    private float r;
    //A boolean to see if the king is attacking
    private bool attacking;
    //A float to see when was the last attack of the king
    private float lastAttack;
    //bool to see if the king is teleporting
    private bool teleporting;
    //float to see the damage the king is receiving
    public float damage;
    //boolean to see if the boss is in his secon fase
    public bool fase2;
    //boolean to see if the fight has started
    private bool fighting;
    //boolean to see if the king has already tried to teleport this attack
    private bool teleportAttack;

    void Start()
    {
        player = GameObject.Find("Player");
        lookingRight = true;
        moving = false;
        prevPos = gameObject.transform.position;
        lastAttack = -1.0f;
        teleporting = false;
        damage = 0.0f;
        fase2 = false;
        fighting = false;
        teleportAttack = false;
    }

    private void Update()
    {
        if (player.transform.position.x > -10.0f && player.transform.position.x < 16.5f) fighting = true;
        if (!moving)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            gameObject.transform.position = prevPos;
        }
        prevPos = gameObject.transform.position;
    }

    void FixedUpdate()
    {
        if (fighting)
        {
            if ((gameObject.transform.position.y > 2.5 && player.transform.position.y < 2.5) || (gameObject.transform.position.y < 2.5 && player.transform.position.y > 2.5))
            {
                gameObject.GetComponent<Animator>().SetBool("StartTeleport", true);
                teleporting = true;
            }
            if(Time.fixedTime - lastAttack < 1.0f)
            {
                r = Random.Range(0.0f, 100.0f);
                if (r < 30.0f && !teleportAttack)
                {
                    gameObject.GetComponent<Animator>().SetBool("StartTeleport", true);
                    teleporting = true;
                }
                else teleportAttack = true;
            }           

            if (player.transform.position.x < gameObject.transform.position.x && !attacking && (Time.fixedTime - lastAttack > 1.0f) && !teleporting && !gameObject.GetComponent<Animator>().GetBool("EnterFase2"))
            {
                if (lookingRight) Flip();
                if (gameObject.transform.position.x - player.transform.position.x > 2.0f)
                {
                    moving = true;
                    targetVelocity = new Vector2(-3f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    r = Random.Range(0.0f, 100.0f);
                    moving = false;
                    targetVelocity = new Vector2(0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                    if (r < 70.0f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("IsAttacking", true);
                        teleportAttack = false;
                        attacking = true;
                    }
                    else
                    {
                        gameObject.GetComponent<Animator>().SetBool("IsGroundAttacking", true);
                        teleportAttack = false;
                        attacking = true;
                    }
                }
            }
            else if (player.transform.position.x >= gameObject.transform.position.x && !attacking && (Time.fixedTime - lastAttack > 1.0f) && !teleporting && !gameObject.GetComponent<Animator>().GetBool("EnterFase2"))
            {
                if (!lookingRight) Flip();
                if (player.transform.position.x - gameObject.transform.position.x > 2.0f)
                {
                    moving = true;
                    targetVelocity = new Vector2(3f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    r = Random.Range(0.0f, 100.0f);
                    moving = false;
                    targetVelocity = new Vector2(0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
                    if (r < 70.0f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("IsAttacking", true);
                        teleportAttack = false;
                        attacking = true;
                    }
                    else
                    {
                        gameObject.GetComponent<Animator>().SetBool("IsGroundAttacking", true);
                        teleportAttack = false;
                        attacking = true;
                    }
                }
            }


            gameObject.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(targetVelocity.x));
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(gameObject.GetComponent<Rigidbody2D>().velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
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

    public void Combo1Appear()
    {
        combo1 = Instantiate(combo1Prefab, transform.position, Quaternion.identity);
    }

    //functions used to make the damage areas appear and disappear
    public void Combo1Disappear()
    {
        Destroy(combo1);
    }

    public void Combo2Appear()
    {
        if (lookingRight) combo2 = Instantiate(combo2Prefab, new Vector2(transform.position.x + 3.246086f, transform.position.y), Quaternion.identity);
        else combo2 = Instantiate(combo2Prefab, new Vector2(transform.position.x - 3.246086f, transform.position.y), Quaternion.identity);
    }

    public void Combo2Disappear()
    {
        Destroy(combo2);
    }

    public void GroundAttackAppear()
    {
        if (lookingRight) groundAttack = Instantiate(groundAttackPrefab, new Vector2(transform.position.x - 0.3566999f, transform.position.y), Quaternion.identity);
        else groundAttack = Instantiate(groundAttackPrefab, new Vector2(transform.position.x + 0.3566999f, transform.position.y), Quaternion.identity);
    }

    public void GroundAttackDisappear()
    {
        Destroy(groundAttack);
    }

    //function to make the sprite renderer dissapear when the king is teleporting
    public void disappearTeleport()
    {
        moving = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        if (gameObject.transform.position.y > 2.5 && player.transform.position.y < 2.5) gameObject.transform.position = new Vector3(gameObject.transform.position.x, -2.9f, 0.0f);
        else if (gameObject.transform.position.y < 2.5 && player.transform.position.y > 2.5) gameObject.transform.position = new Vector3(gameObject.transform.position.x, 4.1f, 0.0f);
        else
        {
            if(gameObject.transform.position.x < 3.25) r = Random.Range(gameObject.transform.position.x + 3.0f, 13.5f);
            else if((gameObject.transform.position.x > 3.25)) r = Random.Range(-7.0f, gameObject.transform.position.x - 3.0f);
            gameObject.transform.position = new Vector3(r, gameObject.transform.position.y, 0.0f);            
        }
        gameObject.GetComponent<Animator>().SetBool("StartTeleport", false);
        gameObject.GetComponent<Animator>().SetTrigger("EndTeleport");
        lastAttack -= 1;
    }
    //function to make the sprite renderer appear when the king is teleporting
    public void appearTeleport()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    //function to end the teleportation
    public void endTeleport()
    {
        teleporting = false;
        moving = false;
        if (player.transform.position.x >= gameObject.transform.position.x)
        {
            if (!lookingRight) Flip();
        }
        else
        {
            if (lookingRight) Flip();
        }
    }

    //functions to end the gorund attack and the combo attack
    public void endComboAttack()
    {
        gameObject.GetComponent<Animator>().SetBool("IsAttacking", false);
        attacking = false;
        lastAttack = Time.fixedTime;
    }
    public void endGroundAttack()
    {
        gameObject.GetComponent<Animator>().SetBool("IsGroundAttacking", false);
        attacking = false;
        lastAttack = Time.fixedTime;
    }
    //spawn the first combo2 
    public void spawnCombo2Fase2()
    {
        if (fase2)
        {
            if (lookingRight) Instantiate(combo2fase2Prefab, new Vector2(transform.position.x + 7.4f, transform.position.y), Quaternion.identity);
            else Instantiate(combo2fase2Prefab, new Vector2(transform.position.x - 7.4f, transform.position.y), Quaternion.identity);
        }        
    }
    //spawn the second combo2
    public void spawn2Combo2Fase2()
    {
        if (fase2)
        {
            if (lookingRight) Instantiate(combo2fase2Prefab, new Vector2(transform.position.x + 11.7f, transform.position.y), Quaternion.identity);
            else Instantiate(combo2fase2Prefab, new Vector2(transform.position.x - 11.7f, transform.position.y), Quaternion.identity);
        }
    }
    //spawn the ground attack
    public void spawnGroundAttackFase2()
    {
        if (fase2)
        {
            if (lookingRight)
            {
                Instantiate(groundAttackFase2Prefab, new Vector2(transform.position.x + 3.5f, transform.position.y), Quaternion.identity);
                Instantiate(groundAttackFase2Prefab, new Vector2(transform.position.x - 4.3f, transform.position.y), Quaternion.identity);
            }
            else
            {
                Instantiate(groundAttackFase2Prefab, new Vector2(transform.position.x + 4.2f, transform.position.y), Quaternion.identity);
                Instantiate(groundAttackFase2Prefab, new Vector2(transform.position.x - 3.6f, transform.position.y), Quaternion.identity);
            }
        }
    }
    //function to make the king enter the second fase
    public void enterSecondFase()
    {
        gameObject.GetComponent<Animator>().SetBool("EnterFase2", false);
        fase2 = true;
    }
}
