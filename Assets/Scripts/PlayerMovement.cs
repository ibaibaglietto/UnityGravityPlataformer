﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    //The main camera
    private Camera cam;
    //The character controller
    public CharacterController2D controller;
    //The animator
    public Animator animator;
    //Boolean to check if we are changing the gravity
    public bool changingGravity = false;

    //The prefab of the shuriken
    public GameObject shurikenPrefab;
    //The variable that we are going to use each time we throw a shuriken
    private GameObject shuriken;
    //The prefab of the dash shadow
    public GameObject dashPrefab;
    //The variable that we are going to use each time we create a dash shadow
    private GameObject dash;
    //The prefab of the slash
    public GameObject slashPrefab;
    public int gravity = 0; //0 -> down, 1 -> up, 2-> left, 3 -> right.
    //the gravity of the previous frame
    public int lastGravity = 0;
    //movement speed
    public float runSpeed = 40f;
    //The horizontal move we are going to apply to the Move function. This will always be looking from player's perspective, not camera's
    float horizontalMove = 0f;
    //Boolean to start jumping
    bool jump = false;
    //4 floats to save the gravity from each side 
    public float gravityDown;
    public float gravityUp;
    public float gravityLeft;
    public float gravityRight;
    //4 floats to save the gravity from each side while the gravity is being changed
    public float prevGravityDown;
    public float prevGravityUp;
    public float prevGravityLeft;
    public float prevGravityRight;
    //The rotation that the player must have on land
    public float rotation;
    //a boolean to check if the player is attacking
    private bool attacking = false;
    //Boolean to check if the player is throwing shurikens
    private bool throwing = false;
    //Boolean to check if the player is dashing
    private bool dashing;
    //the time the last shuriken was thrown
    private float lastShuriken = -0.3f;
    //The firing rate
    private float firingRate = 0.3f;
    //The mana that will be spend 50 times every second
    public float spendingMana;
    //Bool to see if the player has mana
    public bool hasMana;
    //Bool to see if the player has full mana
    public bool fullMana;
    //Damage taken because of the gravity
    public float gravityDamage;
    //vector to store players velocity before changing gravity
    public Vector3 prevVelocity;
    //Boolean to see if the player is rotating
    public bool rotating;
    //Boolean to see if the player is rotated
    public bool rotated;
    //vector to store the velocity of the player the previous frame
    private Vector2 lastVelocity;
    //Boolean to see if the player has the healing activated
    public bool healing;
    //The time the last dash was done
    private float lastDash;
    //Boolean to see if the player can dash
    private bool canDash;
    //Boolean to see if the player was on the ground before the time to dash passed
    private bool wasGround;
    //Boolean to see if the player is taking damage
    public bool takingDamage;
    //The time the last damage was taken
    public float lastDamage;
    //The damage taken by traps
    public float trapDamage;
    //The damage taken by enemies
    public float enemyDamage;
    //The position that the slash needs to spawn
    private Vector3 slashPos;
    //The rotation the slash must have
    private int slashRotation;
    //A boolean to see if the player can rest
    public bool canRest;
    //A boolean to see if the player is resting
    private bool resting;
    //A float to see the position the player will rest
    public float restPos;
    //A float to see if the player is sleeping
    public bool sleeping;
    //A boolean to see if the player can absorb fire
    public bool canAbsorb;
    //A boolean to see if the player is absorbing fire
    public bool isAbsorbing;
    //A boolean to see if the player is trying to absorb
    private bool tryAbsorb;


    private void Start()
    {
        //We initialize the gravity and the camera
        gravityDown = 1.0f;
        gravityUp = 0.0f;
        gravityLeft = 0.0f;
        gravityRight = 0.0f;
        cam = Camera.main;
        //Initialize all the variables we are going to use to manage the actions of the player
        hasMana = true;
        rotated = false;
        rotating = false;
        healing = false;
        dashing = false;
        lastDash = Time.fixedTime - 1.0f;
        canDash = true;
        takingDamage = false;
        lastDamage = Time.fixedTime - 2.0f;
        resting = false;
        sleeping = false;
        canAbsorb = false;
        isAbsorbing = false;
        fullMana = true;
        tryAbsorb = false;
        //Check if the levels are initialized
        //The health level 
        if (!PlayerPrefs.HasKey("healthLevel")) PlayerPrefs.SetInt("healthLevel", 1);
        //The mana level
        if (!PlayerPrefs.HasKey("manaLevel")) PlayerPrefs.SetInt("manaLevel", 1);
        //The dealt damage level
        if (!PlayerPrefs.HasKey("dealtDamageLevel")) PlayerPrefs.SetInt("dealtDamageLevel", 1);
        //The dash level
        if (!PlayerPrefs.HasKey("dashLevel")) PlayerPrefs.SetInt("dashLevel", 1);
        //The healing level
        if (!PlayerPrefs.HasKey("healingLevel")) PlayerPrefs.SetInt("healingLevel", 1);
        //The gravity damage level
        if (!PlayerPrefs.HasKey("gravityDamageLevel")) PlayerPrefs.SetInt("gravityDamageLevel", 1);
        //The damage resistance level
        if (!PlayerPrefs.HasKey("damageResistanceLevel")) PlayerPrefs.SetInt("damageResistanceLevel", 1);
        //The exp gaining level
        if (!PlayerPrefs.HasKey("expGainingLevel")) PlayerPrefs.SetInt("expGainingLevel", 1);
    }
    
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Closing game");
        }
        if (animator.GetBool("isDead") && Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
        //stop throwing shurikens when the player is damaged or is dead
        if (animator.GetBool("isDead") || animator.GetBool("takeDamage"))
        {
            throwing = false;
            attacking = false;
            animator.SetBool("isSpinning", false);
        }
        //set the gravity to normal in case the player has not mana
        if (!hasMana && rotated)
        {
            rotating = false;
            rotated = false;
        }
        if(canRest && !changingGravity && Input.GetKeyDown(KeyCode.S) && animator.GetFloat("Speed")<0.5 && !animator.GetBool("isJumping") && !animator.GetBool("isFalling") && !attacking && !animator.GetBool("isDead") && !animator.GetBool("isResting") && !resting && GetComponent<Rigidbody2D>().velocity == new Vector2(0f, 0f) && !tryAbsorb)
        {
            if (!gameObject.GetComponent<CharacterController2D>().m_FacingRight) gameObject.GetComponent<CharacterController2D>().Flip();
            resting = true;
            healing = false;            
            gameObject.transform.position = new Vector2(restPos, gameObject.transform.position.y);
            animator.SetBool("isResting", true);
        }
        if (resting && animator.GetBool("isResting") && Input.GetKeyDown(KeyCode.S) && sleeping && !tryAbsorb)
        {
            animator.SetBool("isResting", false);
            sleeping = false;
        }
        if (Input.GetKey(KeyCode.F) && !changingGravity && animator.GetFloat("Speed") < 0.5 && !animator.GetBool("isJumping") && !animator.GetBool("isFalling") && !attacking && !animator.GetBool("isDead") && !animator.GetBool("isResting") && !resting && GetComponent<Rigidbody2D>().velocity == new Vector2(0f, 0f))
        {
            if (canAbsorb && !fullMana) isAbsorbing = true;
            else isAbsorbing = false;
            animator.SetBool("isAbsorbing", true);
            tryAbsorb = true;
        }
        else
        {
            isAbsorbing = false;
            animator.SetBool("isAbsorbing", false);
        }
        //Activate gravity change when player presses Q
        if (!changingGravity && Input.GetKeyDown(KeyCode.Q) && !animator.GetBool("isDead") && hasMana && !dashing && !takingDamage && !attacking && !resting && !tryAbsorb)
        {
            //Time will slow down while changing the gravity
            Time.timeScale = 0.05f;
            changingGravity = true;
            spendingMana = 0.0f;
            prevGravityDown = gravityDown;
            prevGravityUp = gravityUp;
            prevGravityLeft = gravityLeft;
            prevGravityRight = gravityRight;
        }
        else if (changingGravity)
        {
            //Return to normal when Q is pressed again
            if(Input.GetKeyDown(KeyCode.Q))
            {
                gravityDown = prevGravityDown;
                gravityUp = prevGravityUp;
                gravityLeft = prevGravityLeft;
                gravityRight = prevGravityRight;
                rotating = true;
                prevVelocity = GetComponent<Rigidbody2D>().velocity;
                Time.timeScale = 1.0f;
                //We'll check what is the gravity now and we will save it.
                if (gravityDown > gravityUp)
                {
                    if (gravityDown > gravityLeft)
                    {
                        if (gravityDown > gravityRight)
                        {
                            lastGravity = gravity;
                            gravity = 0;
                        }
                        else
                        {
                            lastGravity = gravity;
                            gravity = 3;
                        }
                    }
                    else
                    {
                        if (gravityLeft > gravityRight)
                        {
                            lastGravity = gravity;
                            gravity = 2;
                        }
                        else
                        {
                            lastGravity = gravity;
                            gravity = 3;
                        }
                    }
                }
                else if (gravityDown <= gravityUp)
                {
                    if (gravityUp > gravityLeft)
                    {
                        if (gravityUp > gravityRight)
                        {
                            lastGravity = gravity;
                            gravity = 1;
                        }
                        else if (gravityUp < gravityRight)
                        {
                            lastGravity = gravity;
                            gravity = 3;
                        }
                    }
                    else
                    {
                        if (gravityLeft > gravityRight)
                        {
                            lastGravity = gravity;
                            gravity = 2;
                        }
                        else if (gravityLeft < gravityRight)
                        {
                            lastGravity = gravity;
                            gravity = 3;
                        }
                    }
                }
                //We'll change the facing component depending on the last gravity
                if ((lastGravity == 0 && gravity == 1) || (lastGravity == 1 && gravity == 0) || (lastGravity == 0 && gravity == 2) || (lastGravity == 2 && gravity == 0) || (lastGravity == 3 && gravity == 1) || (lastGravity == 1 && gravity == 3) || (lastGravity == 3 && gravity == 2) || (lastGravity == 2 && gravity == 3))
                {
                    gameObject.GetComponent<CharacterController2D>().m_FacingRight = !gameObject.GetComponent<CharacterController2D>().m_FacingRight;
                }
                //We set the rotation that needs to have the player and where is the "ground" supposed to be
                if (gravity == 0) rotation = 0.0f;
                else if (gravity == 1) rotation = 180.0f;
                else if (gravity == 2) rotation = 270.0f;
                else if (gravity == 3) rotation = 90.0f;
            }            
            if (rotated)
            {
                GetComponent<Rigidbody2D>().velocity = prevVelocity;
                changingGravity = false;
                spendingMana = ((gravityUp) + Mathf.Abs(gravityDown - 1.0f) + gravityLeft + gravityRight) / 50.0f;
                rotating = false;
                rotated = false;
            }
        }
        if (Time.fixedTime - lastDash <= 0.5f && gameObject.GetComponent<CharacterController2D>().m_Grounded) wasGround = true;
        if (Time.fixedTime - lastDash > 0.5f && (gameObject.GetComponent<CharacterController2D>().m_Grounded || wasGround)) canDash = true;
        //We activate/deactivate the healing using the R button
        if (!changingGravity && Input.GetKeyDown(KeyCode.R) && !animator.GetBool("isDead") && hasMana && !resting && !tryAbsorb) healing = !healing;
        //We dash using the right button of the mouse
        if (!changingGravity && Input.GetKeyDown(KeyCode.Mouse1) && !animator.GetBool("isDead") && canDash && !takingDamage && !attacking && !resting && !tryAbsorb)
        {
            animator.SetBool("isDashing", true);
            if (gravity == 0 && gameObject.GetComponent<CharacterController2D>().m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(3200f, 0));
            else if (gravity == 0 && !gameObject.GetComponent<CharacterController2D>().m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3200f, 0));
            else if (gravity == 1 && gameObject.GetComponent<CharacterController2D>().m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(3200f, 0));
            else if (gravity == 1 && !gameObject.GetComponent<CharacterController2D>().m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3200f, 0));
            else if (gravity == 2 && gameObject.GetComponent<CharacterController2D>().m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3200f));
            else if (gravity == 2 && !gameObject.GetComponent<CharacterController2D>().m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -3200f));
            else if (gravity == 3 && gameObject.GetComponent<CharacterController2D>().m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3200f));
            else if (gravity == 3 && !gameObject.GetComponent<CharacterController2D>().m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -3200f));
            lastDash = Time.fixedTime;
            canDash = false;
            wasGround = false;

        }

        //We attack using the left button of the mouse, choosing the side of the attack depending on where is the mouse
        if (!changingGravity && Input.GetKeyDown(KeyCode.Mouse0) && !dashing && !animator.GetBool("isDead") && !animator.GetBool("isSpinning") && !resting && !tryAbsorb)
        {            
            attacking = true;
            animator.SetBool("isAttacking", true);
            animator.SetBool("isSpinning", false);
        }
        //We throw shurikens using the right click of the mouse. 
        if (!changingGravity && Input.GetKey(KeyCode.E) && !dashing && !animator.GetBool("isDead") && gameObject.GetComponent<CharacterController2D>().m_Grounded && !resting && !tryAbsorb)
        {
            attacking = true;
            animator.SetBool("isSpinning", true);
        }
        //We will stop throwing when the right button is no longer pressed
        else
        {
            animator.SetBool("isSpinning", false);
        }       

        //We'll save the movement if the player is not dead nor is attacking nor is changing gravity
        if (!changingGravity)
        {           
            if (!attacking)
            {
                if (gravity < 2) horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
                else horizontalMove = Input.GetAxisRaw("Vertical") * runSpeed;
            }
            if (animator.GetBool("isDead") || takingDamage || resting || tryAbsorb) horizontalMove = 0.0f;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump") && !animator.GetBool("isDead") && !takingDamage && !resting && !attacking && !tryAbsorb)
            {
                jump = true;
            }
        }
        
        //We'll activate the jumping animation if the player is not on the ground
        if(!gameObject.GetComponent<CharacterController2D>().m_Grounded) animator.SetBool("isJumping", true);
        //We'll activate the falling animation depending on the gravity
        if (gravity == 0)
        {
            if (animator.GetBool("isJumping") && gameObject.GetComponent<Rigidbody2D>().velocity.y < 0) animator.SetBool("isFalling", true);
            else animator.SetBool("isFalling", false);
        }
        else if (gravity == 1)
        {
            if (animator.GetBool("isJumping") && gameObject.GetComponent<Rigidbody2D>().velocity.y > 0) animator.SetBool("isFalling", true);
            else animator.SetBool("isFalling", false);
        }
        else if (gravity == 2)
        {
            if (animator.GetBool("isJumping") && gameObject.GetComponent<Rigidbody2D>().velocity.x < 0) animator.SetBool("isFalling", true);
            else animator.SetBool("isFalling", false);
        }
        else if (gravity == 3)
        {
            if (animator.GetBool("isJumping") && gameObject.GetComponent<Rigidbody2D>().velocity.x > 0) animator.SetBool("isFalling", true);
            else animator.SetBool("isFalling", false);
        }

    }

    //When the player lands we uncheck the jumping and falling booleans
    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", false);
    }
    void FixedUpdate()
    {
        if (changingGravity)
        {
            //We'll change gravity with the movement keys. If we press left shift while doping it it will grow 5 times more in that direction.
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.W) && !rotating)
                {
                    if (prevGravityDown >= 0.5f) prevGravityDown -= 0.5f;
                    else
                    {
                        prevGravityUp += 0.5f - prevGravityDown;
                        prevGravityDown = 0.0f;
                    }
                }
                else if (Input.GetKey(KeyCode.S) && !rotating)
                {
                    if (prevGravityUp >= 0.5f) prevGravityUp -= 0.5f;
                    else
                    {
                        prevGravityDown += 0.5f - prevGravityUp;
                        prevGravityUp = 0.0f;
                    }
                }
                else if (Input.GetKey(KeyCode.A) && !rotating)
                {
                    if (prevGravityRight >= 0.5f) prevGravityRight -= 0.5f;
                    else
                    {
                        prevGravityLeft += 0.5f - prevGravityRight;
                        prevGravityRight = 0.0f;
                    }
                }
                else if (Input.GetKey(KeyCode.D) && !rotating)
                {
                    if (prevGravityLeft >= 0.5f) prevGravityLeft -= 0.5f;
                    else
                    {
                        prevGravityRight += 0.5f - prevGravityLeft;
                        prevGravityLeft = 0.0f;
                    }
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.W) && !rotating)
                {
                    if (prevGravityDown > 0.1f) prevGravityDown -= 0.1f;
                    else if (prevGravityDown > 0.0f) prevGravityDown = 0.0f;
                    else prevGravityUp += 0.1f;
                }
                else if (Input.GetKey(KeyCode.S) && !rotating)
                {
                    if (prevGravityUp > 0.1f) prevGravityUp -= 0.1f;
                    else if (prevGravityUp > 0.0f) prevGravityUp = 0.0f;
                    else prevGravityDown += 0.1f;
                }
                else if (Input.GetKey(KeyCode.A) && !rotating)
                {
                    if (prevGravityRight > 0.1f) prevGravityRight -= 0.1f;
                    else if (prevGravityRight > 0.0f) prevGravityRight = 0.0f;
                    else prevGravityLeft += 0.1f;
                }
                else if (Input.GetKey(KeyCode.D) && !rotating)
                {
                    if (prevGravityLeft > 0.1f) prevGravityLeft -= 0.1f;
                    else if (prevGravityLeft > 0.0f) prevGravityLeft = 0.0f;
                    else prevGravityRight += 0.1f;
                }
            }
        }
        //We call the move function
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, gravity, changingGravity);
        jump = false;
        //We throw shurikens depending on the firing rate. The shurikens will go where the mouse is aiming at.
        if ( throwing && ((Time.fixedTime - lastShuriken) >= firingRate))
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var rot = Quaternion.AngleAxis(angle, Vector3.forward);
            shuriken = Instantiate(shurikenPrefab, transform.position, rot);
            dir.Normalize();
            shuriken.GetComponent<Rigidbody2D>().velocity = dir * 10.0f;
            lastShuriken = Time.fixedTime;
        }
        //We apply the gravity
        gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(gravityRight - gravityLeft, gravityUp - gravityDown);
        //We set a maximum velocity to fall
        if (!changingGravity || rotating)
        {                        
            if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 199) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(199.0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            else if(gameObject.GetComponent<Rigidbody2D>().velocity.x < -199) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-199.0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y > 199) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 199.0f);
            else if (gameObject.GetComponent<Rigidbody2D>().velocity.y < -199) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -199.0f);
            if (!rotated && rotating) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
        if (dashing)
        {
            dash = Instantiate(dashPrefab, transform.position, transform.rotation);
            if (!gameObject.GetComponent<CharacterController2D>().m_FacingRight && (gravity == 0 || gravity == 3))
            {
                Vector3 theScale = dash.transform.localScale;
                theScale.x *= -1;
                dash.transform.localScale = theScale;
            }
            else if (gameObject.GetComponent<CharacterController2D>().m_FacingRight && (gravity == 1 || gravity == 2))
            {
                Vector3 theScale = dash.transform.localScale;
                theScale.x *= -1;
                dash.transform.localScale = theScale;
            }
        }
        //We make the player rotate depending on her actual rotation and aim rotation
        if (gameObject.GetComponent<Rigidbody2D>().rotation != rotation && rotating)
        {
            if ((rotation > gameObject.GetComponent<Rigidbody2D>().rotation) && !(rotation == 270 && gameObject.GetComponent<Rigidbody2D>().rotation == 0))
            {
                if ((rotation - gameObject.GetComponent<Rigidbody2D>().rotation) < 5.0f) gameObject.GetComponent<Rigidbody2D>().rotation = rotation;
                else gameObject.GetComponent<Rigidbody2D>().rotation += 10.0f;
            }
            else if ((rotation < gameObject.GetComponent<Rigidbody2D>().rotation) && !(rotation == 0 && gameObject.GetComponent<Rigidbody2D>().rotation == 270))
            {
                if ((gameObject.GetComponent<Rigidbody2D>().rotation - rotation) < 5.0f) gameObject.GetComponent<Rigidbody2D>().rotation = rotation;
                else gameObject.GetComponent<Rigidbody2D>().rotation -= 10.0f;
            }
            else if (gameObject.GetComponent<Rigidbody2D>().rotation == 0 && rotation == 270) gameObject.GetComponent<Rigidbody2D>().rotation = 360;
            else if (gameObject.GetComponent<Rigidbody2D>().rotation == 270 && rotation == 0) gameObject.GetComponent<Rigidbody2D>().rotation = -90;
        }
        //else if (gameObject.GetComponent<Rigidbody2D>().rotation == rotation && rotating) rotated = true;
        gravityDamage = 0.0f;
        //if the player is on the floor and has 3 gravity or more she takes damage per second
        if (gameObject.GetComponent<CharacterController2D>().m_Grounded && (gravityDown > 3.0f || gravityUp > 3.0f || gravityLeft > 3.0f || gravityRight > 3.0f))
        {
            if (gravity == 0) gravityDamage += gravityDown / 50.0f;
            else if (gravity == 1) gravityDamage += gravityUp / 50.0f;
            else if (gravity == 2) gravityDamage += gravityLeft / 50.0f;
            else if (gravity == 3) gravityDamage += gravityRight / 50.0f;
        }
        //if the player falls in more than 20 velocity she takes instant damage
        if (!rotating && ((Mathf.Abs(Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x) - Mathf.Abs(lastVelocity.x)) > 20.0f && Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x) < (gravityLeft + gravityRight + 1.0f)) || (Mathf.Abs(Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) - Mathf.Abs(lastVelocity.y)) > 20.0f && Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) < (gravityUp + gravityDown + 1.0f))))
        {
            if (gravity == 0 || gravity == 1)
            {
                gravityDamage += Mathf.Abs(Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) - Mathf.Abs(lastVelocity.y)) / 2.0f;
            }
            else
            {
                gravityDamage += Mathf.Abs(Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x) - Mathf.Abs(lastVelocity.x)) / 2.0f;
            }
        }
        //we check if the player has taken damage to make it visually easy to see that he can't take damage now
        if ((Time.fixedTime - lastDamage) < 0.3f && !animator.GetBool("isDead"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0.2f);
        }
        else if ((Time.fixedTime - lastDamage) < 0.6f && !animator.GetBool("isDead"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 1.0f);
        }
        else if ((Time.fixedTime - lastDamage) < 0.9f && !animator.GetBool("isDead"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0.2f);
        }
        else if ((Time.fixedTime - lastDamage) < 1.2f && !animator.GetBool("isDead"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 1.0f);
        }
        else if ((Time.fixedTime - lastDamage) < 1.4f && !animator.GetBool("isDead"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0.2f);
        }
        else if ((Time.fixedTime - lastDamage) < 1.6f && !animator.GetBool("isDead"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 1.0f);
        }
        else if ((Time.fixedTime - lastDamage) < 1.7f && !animator.GetBool("isDead"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0.2f);
        }
        else if ((Time.fixedTime - lastDamage) < 1.8f && !animator.GetBool("isDead"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 1.0f);
        }
        else if ((Time.fixedTime - lastDamage) < 1.9f && !animator.GetBool("isDead"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0.2f);
        }
        else if ((Time.fixedTime - lastDamage) < 2.0f && !animator.GetBool("isDead"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 1.0f);
        }
        else gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 1.0f);
        if (animator.GetBool("isDead")) dashing = false;

        lastVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;

    }
    public void startAttack()
    {
        //we make the player aim where the mouse is aiming
        animator.SetBool("isAttacking", false);
        if(gravity == 0 || gravity == 1)
        {
            if (gameObject.transform.position.x > cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)).x)
            {
                horizontalMove = -0.001f;
            }
            else if (gameObject.transform.position.x < cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)).x)
            {
                horizontalMove = 0.001f;
            }
        }
        else if (gravity == 2 || gravity == 3)
        {
            if (gameObject.transform.position.y > cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)).y)
            {
                horizontalMove = -0.001f;
            }
            else if (gameObject.transform.position.y < cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)).y)
            {
                horizontalMove = 0.001f;
            }
        }


    }
    //function to end the attack
    public void endAttack()
    {
        attacking = animator.GetBool("isAttacking");
    }
    //function to end the spin damage animation
    public void endSpin()
    {
        attacking = animator.GetBool("isSpinning");
    }
    //function to start the spin damage animation
    public void startSpin()
    {
        if (gravity == 0 || gravity == 1)
        {
            if (gameObject.transform.position.x > cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)).x)
            {
                horizontalMove = -0.001f;
            }
            else if (gameObject.transform.position.x < cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)).x)
            {
                horizontalMove = 0.001f;
            }
        }
        else if (gravity == 2 || gravity == 3)
        {
            if (gameObject.transform.position.y > cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)).y)
            {
                horizontalMove = -0.001f;
            }
            else if (gameObject.transform.position.y < cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)).y)
            {
                horizontalMove = 0.001f;
            }
        }
    }
    //function to start throwing shurikens
    public void startThrow()
    {
        throwing = true;
    }
    //function to end throwing shurikens
    public void endThrow()
    {
        throwing = false;
    }

    //function to generate the dash shadow
    public void dashShadow()
    {
        dashing = true;
    }

    //function to end the dash
    public void endDash()
    {
        dashing = false;
        animator.SetBool("isDashing", false);
    }

    //function to end the getting damage state
    public void endDamage()
    {
        takingDamage = false;
        animator.SetBool("takeDamage", false);
    }

    //function to generate the slash of the sword
    public void slash()
    {
        if(gravity == 0)
        {
            if (gameObject.GetComponent<CharacterController2D>().m_FacingRight) slashPos = new Vector3(transform.position.x + 0.75f, transform.position.y, transform.position.z);
            else slashPos = new Vector3(transform.position.x - 0.75f, transform.position.y, transform.position.z);
            slashRotation = 90;
        }
        if (gravity == 1)
        {
            if (gameObject.GetComponent<CharacterController2D>().m_FacingRight) slashPos = new Vector3(transform.position.x + 0.75f, transform.position.y, transform.position.z);
            else slashPos = new Vector3(transform.position.x - 0.75f, transform.position.y, transform.position.z);
            slashRotation = 90;
        }
        if (gravity == 2)
        {
            if (gameObject.GetComponent<CharacterController2D>().m_FacingRight) slashPos = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
            else slashPos = new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z);
            slashRotation = 0;
        }
        if (gravity == 3)
        {
            if (gameObject.GetComponent<CharacterController2D>().m_FacingRight) slashPos = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
            else slashPos = new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z);
            slashRotation = 0;
        }
        Instantiate(slashPrefab, slashPos, Quaternion.AngleAxis(slashRotation, Vector3.forward));
    }

    //function to return to normal gravity when the player runs out of mana
    public void normalGravity()
    {
        gameObject.GetComponent<PlayerMovement>().hasMana = false;
        gameObject.GetComponent<PlayerMovement>().gravityDown = 1.0f;
        gameObject.GetComponent<PlayerMovement>().gravityUp = 0.0f;
        gameObject.GetComponent<PlayerMovement>().gravityLeft = 0.0f;
        gameObject.GetComponent<PlayerMovement>().gravityRight = 0.0f;
        gameObject.GetComponent<PlayerMovement>().rotating = true;
        gameObject.GetComponent<PlayerMovement>().spendingMana = 0.0f;
        gameObject.GetComponent<PlayerMovement>().prevVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        //We'll check what is the gravity now and we will save it.
        if (gravityDown > gravityUp)
        {
            if (gravityDown > gravityLeft)
            {
                if (gravityDown > gravityRight)
                {
                    lastGravity = gravity;
                    gravity = 0;
                }
                else
                {
                    lastGravity = gravity;
                    gravity = 3;
                }
            }
            else
            {
                if (gravityLeft > gravityRight)
                {
                    lastGravity = gravity;
                    gravity = 2;
                }
                else
                {
                    lastGravity = gravity;
                    gravity = 3;
                }
            }
        }
        else if (gravityDown <= gravityUp)
        {
            if (gravityUp > gravityLeft)
            {
                if (gravityUp > gravityRight)
                {
                    lastGravity = gravity;
                    gravity = 1;
                }
                else if (gravityUp < gravityRight)
                {
                    lastGravity = gravity;
                    gravity = 3;
                }
            }
            else
            {
                if (gravityLeft > gravityRight)
                {
                    lastGravity = gravity;
                    gravity = 2;
                }
                else if (gravityLeft < gravityRight)
                {
                    lastGravity = gravity;
                    gravity = 3;
                }
            }
        }
        //We'll change the facing component depending on the last gravity
        if ((lastGravity == 0 && gravity == 1) || (lastGravity == 1 && gravity == 0) || (lastGravity == 0 && gravity == 2) || (lastGravity == 2 && gravity == 0) || (lastGravity == 3 && gravity == 1) || (lastGravity == 1 && gravity == 3) || (lastGravity == 3 && gravity == 2) || (lastGravity == 2 && gravity == 3))
        {
            gameObject.GetComponent<CharacterController2D>().m_FacingRight = !gameObject.GetComponent<CharacterController2D>().m_FacingRight;
        }
        //We set the rotation that needs to have the player and where is the "ground" supposed to be
        if (gravity == 0) rotation = 0.0f;
        else if (gravity == 1) rotation = 180.0f;
        else if (gravity == 2) rotation = 270.0f;
        else if (gravity == 3) rotation = 90.0f;
    }
    //function to enter the sleeping state
    public void startSleeping()
    {
        sleeping = true;
    }

    //function to end the resting state
    public void endRest()
    {
        resting = false;
    }

    //function to end the absorbing state
    public void endAbsorb()
    {
        tryAbsorb = false;
    }
}
