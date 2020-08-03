using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class PlayerMovement : MonoBehaviour
{
    //Amount of force added when the player jumps.
    private float m_JumpForce = 800f;
    //How much to smooth out the movement
    private float m_MovementSmoothing = .05f;
    //A mask determining what is ground to the character
    [SerializeField] private LayerMask m_WhatIsGround;
    //A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_GroundCheck;

    //Radius of the overlap circle to determine if grounded
    const float k_GroundedRadius = .2f;
    //Whether or not the player is grounded.
    public bool m_Grounded;            
    private Rigidbody2D m_Rigidbody2D;
    //For determining which way the player is currently facing.
    public bool m_FacingRight = true;  
    private Vector3 m_Velocity = Vector3.zero;

    //The main camera
    private Camera cam;
    //The animator
    public Animator animator;
    //Boolean to check if we are changing the gravity
    public bool changingGravity = false;
    //The sounds we are going to use
    public AudioClip deathClip;
    public AudioClip damageClip;
    public AudioClip attackClip;
    public AudioClip shurikenClip;
    public AudioClip jumpClip;
    public AudioClip dashClip;
    public AudioClip gravityClip;
    public AudioClip deathMusicClip;
    //The musicSource
    private AudioSource musicSource;


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
    //The background
    private GameObject backGround;
    //The healthbar
    private GameObject healthBar;
    //The die screen
    private GameObject dieScreen;
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
    public bool resting;
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
    //An int to see how many enemies are attacking the player
    public int attacked;
    //A boolean to see if the player is talking
    public bool talking;
    //A boolean to see if the player is changing scene
    public bool changingScene;
    //A boolean to see if the player is entering a scene
    public bool enteringScene;
    //The gameobject of the black image to fade in and out
    private GameObject fadeInOut;
    //The scene the bench is
    public int benchScene;
    //The prefab of the dieExp
    public GameObject dieExpPrefab;
    //The scene that is being played
    public int scene;
    //A boolean to see if the player has stamina
    public bool hasStamina;
    //An int to see how much stamina is being spent
    public int staminaSpent;
    //A bool to check if the player is approaching the boss
    public bool approach;
    //A bool to check if the player has ended the game
    public bool ended;
    //A bool to check if the game is paused
    public bool paused;
    //A bool to check if the die animation has ended
    public bool dead;
    //A bool to check if the player is inside a trap
    public bool trap;
    //A float to save the time of the last gravity change
    private float gravityTime;

    //The on land event
    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        //We find the rigidbody and initialize the onlandevent
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        if (OnLandEvent == null) OnLandEvent = new UnityEvent();
    }



    private void Start()
    {
        //We initialize the gravity and the camera
        gravityDown = 1.0f;
        gravityUp = 0.0f;
        gravityLeft = 0.0f;
        gravityRight = 0.0f;
        cam = Camera.main;
        //We find the background
        backGround = GameObject.Find("Background");
        //Find the healthbar
        healthBar = GameObject.Find("Healthbar");
        //Find the die screen
        dieScreen = GameObject.Find("DieScreen");
        //find the music source
        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();
        //Initialize all the variables we are going to use to manage the actions of the player
        trap = false;
        dead = false;
        hasMana = true;
        hasStamina = true;
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
        attacked = 0;
        talking = false;
        changingScene = false;
        enteringScene = false;
        approach = false;
        ended = false;
        paused = false;
        benchScene = 0;
        staminaSpent = 0;
        //Save the gameobject of the fadeInOut
        fadeInOut = GameObject.Find("FadeInOut");        
        if (PlayerPrefs.GetInt("hasDied") != 0)
        {
            //We put the player and the camera on their starting position
            cam.transform.position = new Vector3(PlayerPrefs.GetFloat("respawnx"), PlayerPrefs.GetFloat("respawny") + 1.381f, -10.0f);
            //We put the player on the respawn position
            gameObject.transform.position = new Vector2(PlayerPrefs.GetFloat("respawnx"), PlayerPrefs.GetFloat("respawny"));
            //We set the spawning animation and resting state
            animator.SetTrigger("isSpawning");
            resting = true;
            //We check where the player must face
            if (PlayerPrefs.GetInt("respawnface") == 0 && m_FacingRight) Flip();
            else if (PlayerPrefs.GetInt("respawnface") == 1 && !m_FacingRight) Flip();
            //If the player has died we set all the playerprefs to match this.
            if (PlayerPrefs.GetInt("hasDied") == 2)
            {
                PlayerPrefs.SetInt("recoveredExp", 0);
                PlayerPrefs.SetInt("diedexp", PlayerPrefs.GetInt("exp"));
                PlayerPrefs.SetInt("exp", 0);
                PlayerPrefs.SetInt("restExp", 0);
                if (PlayerPrefs.GetInt("dieTutorial") == 0) PlayerPrefs.SetInt("dieTutorial", 1);
            }
            else PlayerPrefs.SetInt("exp", PlayerPrefs.GetInt("restExp"));
            //We instantiate the die exp if the player is in the same level she died
            if (PlayerPrefs.GetInt("diedscene") == PlayerPrefs.GetInt("respawnscene") && PlayerPrefs.GetInt("diedexp") != 0)
            {
                Instantiate(dieExpPrefab, new Vector2(PlayerPrefs.GetFloat("diedx"), PlayerPrefs.GetFloat("diedy")), transform.rotation);
            }
        }
        //If the player is changing scene we check the side she is entering and instantiate the die exp if the player is in the same level she died
        else
        {
            if (PlayerPrefs.GetInt("spawnface") == 0 && m_FacingRight) Flip();
            else if (PlayerPrefs.GetInt("spawnface") == 1 && !m_FacingRight) Flip();
            if (m_FacingRight)
            {
                cam.transform.position = new Vector3(PlayerPrefs.GetFloat("spawnx") - 4.4f, PlayerPrefs.GetFloat("spawny") + 1.381f, -10.0f);
                gameObject.transform.position = new Vector2(PlayerPrefs.GetFloat("spawnx") - 4.4f, PlayerPrefs.GetFloat("spawny"));
            }
            else
            {
                cam.transform.position = new Vector3(PlayerPrefs.GetFloat("spawnx") + 4.4f, PlayerPrefs.GetFloat("spawny") + 1.381f, -10.0f);
                gameObject.transform.position = new Vector2(PlayerPrefs.GetFloat("spawnx") + 4.4f, PlayerPrefs.GetFloat("spawny"));
            }            
            enteringScene = true;
            if (PlayerPrefs.GetInt("diedscene") == PlayerPrefs.GetInt("spawnscene") && PlayerPrefs.GetInt("diedexp") != 0) Instantiate(dieExpPrefab, new Vector2(PlayerPrefs.GetFloat("diedx"), PlayerPrefs.GetFloat("diedy")), transform.rotation);
        }
        //We put the playerpref in alive mode and make the fadeInOut image disappear
        PlayerPrefs.SetInt("hasDied", 1);
        fadeInOut.GetComponent<Animator>().SetBool("Clear", true);
    }
    void Update(){
        //If we have a negative amount of enemies attacking we put it to 0
        if (attacked == -1) attacked = 0;
        //we set the trap int to make the doors to be opened
        if (PlayerPrefs.GetInt("trap") == 2 && PlayerPrefs.GetFloat("respawnx") == -62.63f) PlayerPrefs.SetInt("trap", 3);
        //We pause the game pressing ESC
        if (Input.GetKeyDown(KeyCode.Escape) && !animator.GetBool("isDead") && !resting && !ended)
        {
            paused = true;
            Time.timeScale = 0.0f;
        }
        //We put the coords of the die exp where the player lands when she dies, always looking that it isnt a trap.
        //We also start the dead music, active the die screen and save the scene the player died
        if (dead && m_Grounded && !dieScreen.activeSelf)
        {
            if (!trap)
            {
                PlayerPrefs.SetFloat("diedx", gameObject.transform.position.x);
                PlayerPrefs.SetFloat("diedy", gameObject.transform.position.y - 0.257f);
            }
            musicSource.clip = deathMusicClip;
            musicSource.Play();
            dieScreen.SetActive(true);
            PlayerPrefs.SetInt("diedscene", gameObject.GetComponent<PlayerMovement>().scene);
        }
        else if (!dead || !m_Grounded) dieScreen.SetActive(false);
        //stop throwing shurikens when the player is damaged or is dead
        if (animator.GetBool("isDead") || animator.GetBool("takeDamage"))
        {
            throwing = false;
            attacking = false;
            animator.SetBool("isSpinning", false);
        }
        //We make the player move if she is changing scene or finishing the last dialogue
        if (m_Grounded && (changingScene || PlayerPrefs.GetInt("lastDialogue") == 16 ) && !talking)
        {
            if (!m_FacingRight && PlayerPrefs.GetInt("lastDialogue") == 16) Flip();
            gravityDown = 1.0f;
            fadeInOut.GetComponent<Animator>().SetBool("Clear",false);
        }
        //We check the ended bool if the player has seen the 16 dialogue and the screen is black
        if (PlayerPrefs.GetInt("lastDialogue") == 16 && fadeInOut.GetComponent<Image>().color.a == 1)
        {
            ended = true;
        }
        //We check if the player has arrived to her spawn position correctly
        if (enteringScene)
        {
            if (m_FacingRight && PlayerPrefs.GetFloat("spawnx") <= gameObject.transform.position.x) enteringScene = false;
            else if (!m_FacingRight && PlayerPrefs.GetFloat("spawnx") >= gameObject.transform.position.x) enteringScene = false;
        }
        //We check if the player can rest, and if so it starts to rest when we press the S button
        if (canRest && !changingGravity && Input.GetKeyDown(KeyCode.S) && animator.GetFloat("Speed")<0.5 && !animator.GetBool("isJumping") && !animator.GetBool("isFalling") && !attacking && !animator.GetBool("isDead") && !animator.GetBool("isResting") && !resting && GetComponent<Rigidbody2D>().velocity == new Vector2(0f, 0f) && !tryAbsorb && attacked == 0 && !changingScene && !enteringScene)
        {
            gameObject.GetComponent<PlayerMovement>().changeGravity(true, 1.0f, 0.0f, 0.0f, 0.0f);
            if (m_FacingRight) Flip();
            resting = true;
            healing = false;
            gameObject.transform.position = new Vector2(restPos, gameObject.transform.position.y);
            PlayerPrefs.SetFloat("respawnx", restPos);
            PlayerPrefs.SetFloat("respawny", gameObject.transform.position.y);
            PlayerPrefs.SetInt("respawnface", 0);
            PlayerPrefs.SetInt("respawnscene", benchScene);
            PlayerPrefs.SetInt("restExp", PlayerPrefs.GetInt("exp"));
            animator.SetBool("isResting", true);
            if(PlayerPrefs.GetInt("recoveredExp") == 1)
            {
                PlayerPrefs.SetInt("recoveredExp", 0);
                PlayerPrefs.SetInt("diedexp", 0);
            }
        }
        //We check if the player can absorb, if so it tries to absorb pressing F
        if (Input.GetKey(KeyCode.F) && !changingGravity && animator.GetFloat("Speed") < 0.5 && !animator.GetBool("isJumping") && !animator.GetBool("isFalling") && !attacking && !animator.GetBool("isDead") && !animator.GetBool("isResting") && !resting && GetComponent<Rigidbody2D>().velocity == new Vector2(0f, 0f) &&!talking && !changingScene && !enteringScene && attacked == 0 && PlayerPrefs.GetInt("lastDialogue") != 16 && !approach)
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
        //The player tries to absorb automatically when sleeping
        if (sleeping && !fullMana) isAbsorbing = true;
        //Activate gravity change when player presses Q
        if (!changingGravity && Input.GetKeyDown(KeyCode.Q) && !animator.GetBool("isDead") && hasMana && !dashing && !takingDamage && !attacking && !resting && !tryAbsorb && !talking && !changingScene && !enteringScene && !approach && !ended && PlayerPrefs.GetInt("lastDialogue") != 16)
        {
            //Time will slow down while changing the gravity
            Time.timeScale = 0.05f;
            changingGravity = true;
            spendingMana = 0.0f;
            prevGravityDown = gravityDown;
            prevGravityUp = gravityUp;
            prevGravityLeft = gravityLeft;
            prevGravityRight = gravityRight;
            gravityTime = Time.realtimeSinceStartup + 0.1f;
        }
        else if (changingGravity)
        {
            //We check that 0.3 seconds have passed to controll better the gravity change
            if ((Time.realtimeSinceStartup - gravityTime)>0.3f)
            {
                //We'll change gravity with the movement keys. If we press left shift while doping it it will grow 5 times more in that direction.
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetKey(KeyCode.W) && !rotating)
                    {
                        if (prevGravityDown >= 0.5f) prevGravityDown -= 0.5f;
                        else if (prevGravityUp >= 9.5f) prevGravityUp = 10.0f;
                        else
                        {
                            prevGravityUp += 0.5f - prevGravityDown;
                            prevGravityDown = 0.0f;
                        }
                        gravityTime = Time.realtimeSinceStartup;
                    }
                    else if (Input.GetKey(KeyCode.S) && !rotating)
                    {
                        if (prevGravityUp >= 0.5f) prevGravityUp -= 0.5f;
                        else if (prevGravityDown >= 9.5f) prevGravityDown = 10.0f;
                        else
                        {
                            prevGravityDown += 0.5f - prevGravityUp;
                            prevGravityUp = 0.0f;
                        }
                        gravityTime = Time.realtimeSinceStartup;
                    }
                    else if (Input.GetKey(KeyCode.A) && !rotating)
                    {
                        if (prevGravityRight >= 0.5f) prevGravityRight -= 0.5f;
                        else if (prevGravityLeft >= 9.5f) prevGravityLeft = 10.0f;
                        else
                        {
                            prevGravityLeft += 0.5f - prevGravityRight;
                            prevGravityRight = 0.0f;
                        }
                        gravityTime = Time.realtimeSinceStartup;
                    }
                    else if (Input.GetKey(KeyCode.D) && !rotating)
                    {
                        if (prevGravityLeft >= 0.5f) prevGravityLeft -= 0.5f;
                        else if (prevGravityRight >= 9.5f) prevGravityRight = 10.0f;
                        else
                        {
                            prevGravityRight += 0.5f - prevGravityLeft;
                            prevGravityLeft = 0.0f;
                        }
                        gravityTime = Time.realtimeSinceStartup;
                    }
                }
                //If we press lef control we will put normal gravity to the direction we want
                else if (Input.GetKey(KeyCode.LeftControl))
                {
                    if (Input.GetKey(KeyCode.W) && !rotating)
                    {
                        prevGravityDown = 0.0f;
                        prevGravityUp = 1.0f;
                        prevGravityLeft = 0.0f;
                        prevGravityRight = 0.0f;
                        gravityTime = Time.realtimeSinceStartup - 0.1f;
                    }
                    else if (Input.GetKey(KeyCode.S) && !rotating)
                    {
                        prevGravityDown = 1.0f;
                        prevGravityUp = 0.0f;
                        prevGravityLeft = 0.0f;
                        prevGravityRight = 0.0f;
                        gravityTime = Time.realtimeSinceStartup - 0.1f;
                    }
                    else if (Input.GetKey(KeyCode.A) && !rotating)
                    {
                        prevGravityDown = 0.0f;
                        prevGravityUp = 0.0f;
                        prevGravityLeft = 1.0f;
                        prevGravityRight = 0.0f;
                        gravityTime = Time.realtimeSinceStartup - 0.1f;
                    }
                    else if (Input.GetKey(KeyCode.D) && !rotating)
                    {
                        prevGravityDown = 0.0f;
                        prevGravityUp = 0.0f;
                        prevGravityLeft = 0.0f;
                        prevGravityRight = 1.0f;
                        gravityTime = Time.realtimeSinceStartup - 0.1f;
                    }
                }
                //if we only press the direction we will change the gravity slowly
                else
                {
                    if (Input.GetKey(KeyCode.W) && !rotating)
                    {
                        if (prevGravityDown > 0.1f) prevGravityDown -= 0.1f;
                        else if (prevGravityDown > 0.0f) prevGravityDown = 0.0f;
                        else if (prevGravityUp >= 9.9f) prevGravityUp = 10.0f;
                        else prevGravityUp += 0.1f;
                        gravityTime = Time.realtimeSinceStartup;
                    }
                    else if (Input.GetKey(KeyCode.S) && !rotating)
                    {
                        if (prevGravityUp > 0.1f) prevGravityUp -= 0.1f;
                        else if (prevGravityUp > 0.0f) prevGravityUp = 0.0f;
                        else if (prevGravityDown >= 9.9f) prevGravityDown = 10.0f;
                        else prevGravityDown += 0.1f;
                        gravityTime = Time.realtimeSinceStartup;
                    }
                    else if (Input.GetKey(KeyCode.A) && !rotating)
                    {
                        if (prevGravityRight > 0.1f) prevGravityRight -= 0.1f;
                        else if (prevGravityRight > 0.0f) prevGravityRight = 0.0f;
                        else if (prevGravityLeft >= 9.9f) prevGravityLeft = 10.0f;
                        else prevGravityLeft += 0.1f;
                        gravityTime = Time.realtimeSinceStartup;
                    }
                    else if (Input.GetKey(KeyCode.D) && !rotating)
                    {
                        if (prevGravityLeft > 0.1f) prevGravityLeft -= 0.1f;
                        else if (prevGravityLeft > 0.0f) prevGravityLeft = 0.0f;
                        else if (prevGravityRight >= 9.9f) prevGravityRight = 10.0f;
                        else prevGravityRight += 0.1f;
                        gravityTime = Time.realtimeSinceStartup;
                    }
                }
            }
            //Return to normal when Q is pressed again
            if (Input.GetKeyDown(KeyCode.Q) && !rotating)
            {
                gameObject.GetComponent<AudioSource>().clip = gravityClip;
                gameObject.GetComponent<AudioSource>().Play();
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
                    else if (gravityUp <= gravityLeft)
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
                        else lastGravity = gravity;
                    }
                    
                }
                //We'll change the facing component depending on the last gravity
                if ((lastGravity == 0 && gravity == 1) || (lastGravity == 1 && gravity == 0) || (lastGravity == 0 && gravity == 2) || (lastGravity == 2 && gravity == 0) || (lastGravity == 3 && gravity == 1) || (lastGravity == 1 && gravity == 3) || (lastGravity == 3 && gravity == 2) || (lastGravity == 2 && gravity == 3))
                {
                    m_FacingRight = !m_FacingRight;
                }
                //We set the rotation that needs to have the player and where is the "ground" supposed to be
                if (gravity == 0) rotation = 0.0f;
                else if (gravity == 1) rotation = 180.0f;
                else if (gravity == 2) rotation = 270.0f;
                else if (gravity == 3) rotation = 90.0f;
            }
        }
        //If the player has already rotated she will start moving and spending mana
        if (rotated)
        {
            GetComponent<Rigidbody2D>().velocity = prevVelocity;
            changingGravity = false;
            if(!talking) spendingMana = ((gravityUp) + Mathf.Abs(gravityDown - 1.0f) + gravityLeft + gravityRight) / 50.0f;
            rotating = false;
            rotated = false;
        }
        //We can dash again if 0.5 seconds have passed
        if (Time.fixedTime - lastDash <= 0.5f && m_Grounded) wasGround = true;
        if (Time.fixedTime - lastDash > 0.5f && (m_Grounded || wasGround)) canDash = true;
        //We activate/deactivate the healing using the R button
        if (!changingGravity && Input.GetKeyDown(KeyCode.R) && !animator.GetBool("isDead") && hasMana && !resting && !tryAbsorb && !talking && !changingScene && !enteringScene && !paused && !ended && PlayerPrefs.GetInt("lastDialogue") != 16 && !approach) healing = !healing;
        //We dash using the right button of the mouse
        if (!changingGravity && Input.GetKeyDown(KeyCode.Mouse1) && !animator.GetBool("isDead") && canDash && !takingDamage && !attacking && !resting && !tryAbsorb && !talking && !changingScene && !enteringScene && hasStamina && !paused && !ended && PlayerPrefs.GetInt("lastDialogue") != 16 && !approach)
        {
            gameObject.GetComponent<AudioSource>().clip = dashClip;
            gameObject.GetComponent<AudioSource>().Play();
            animator.SetBool("isDashing", true);
            if (gravity == 0 && m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(3200f, 0));
            else if (gravity == 0 && !m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3200f, 0));
            else if (gravity == 1 && m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(3200f, 0));
            else if (gravity == 1 && !m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3200f, 0));
            else if (gravity == 2 && m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3200f));
            else if (gravity == 2 && !m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -3200f));
            else if (gravity == 3 && m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3200f));
            else if (gravity == 3 && !m_FacingRight) gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -3200f));
            lastDash = Time.fixedTime;
            canDash = false;
            wasGround = false;
            staminaSpent = 5;
        }

        //We attack using the left button of the mouse, choosing the side of the attack depending on where is the mouse
        if (!changingGravity && Input.GetKeyDown(KeyCode.Mouse0) && !dashing && !animator.GetBool("isDead") && !animator.GetBool("isSpinning") && !resting && !tryAbsorb && !talking && !changingScene && !enteringScene && hasStamina && !paused && !takingDamage && !ended && PlayerPrefs.GetInt("lastDialogue") != 16 && !approach)
        {            
            attacking = true;
            animator.SetBool("isAttacking", true);
            animator.SetBool("isSpinning", false);
        }
        //We throw shurikens using the E key. 
        if (!changingGravity && Input.GetKey(KeyCode.E) && !dashing && !animator.GetBool("isDead") && m_Grounded && !resting && !tryAbsorb && !talking && !changingScene && !enteringScene && !paused && !ended && PlayerPrefs.GetInt("lastDialogue") != 16 && !approach)
        {
            attacking = true;
            animator.SetBool("isSpinning", true);
        }
        //We will stop throwing when the right button is no longer pressed
        else
        {
            animator.SetBool("isSpinning", false);
        }       

        //We'll save the movement if the player is not dead or is attacking or is changing gravity or trying to absorb or talking
        if (!changingGravity && !paused && !ended)
        {           
            if (!attacking)
            {
                if (gravity < 2) horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
                else horizontalMove = Input.GetAxisRaw("Vertical") * runSpeed;
            }
            if (animator.GetBool("isDead") || takingDamage || resting || tryAbsorb || talking) horizontalMove = 0.0f;
            if ((changingScene && gravityDown == 1.0f) || enteringScene || approach || (PlayerPrefs.GetInt("lastDialogue") == 16 && fadeInOut.GetComponent<Image>().color.a != 1) && !talking)
            {
                if (m_FacingRight) horizontalMove = runSpeed;
                else horizontalMove = -runSpeed;
            }            
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump") && !animator.GetBool("isDead") && !takingDamage && !resting && !attacking && !tryAbsorb && !talking && !changingScene && !enteringScene && PlayerPrefs.GetInt("lastDialogue") != 16 && !approach)
            {
                jump = true;
            }
        }
        else if (ended) horizontalMove = 0.0f;

        //We'll activate the jumping animation if the player is not on the ground
        if (!m_Grounded) animator.SetBool("isJumping", true);
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
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
        //We apply the gravity
        gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(gravityRight - gravityLeft, gravityUp - gravityDown);
        
        //We call the move function
        Move(horizontalMove * Time.fixedDeltaTime, jump, gravity, changingGravity || paused);
        jump = false;
        //We throw shurikens depending on the firing rate. The shurikens will go where the mouse is aiming at.
        if ( throwing && ((Time.fixedTime - lastShuriken) >= firingRate))
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var rot = Quaternion.AngleAxis(angle, Vector3.forward);
            gameObject.GetComponent<AudioSource>().clip = shurikenClip;
            gameObject.GetComponent<AudioSource>().Play();
            shuriken = Instantiate(shurikenPrefab, transform.position, rot);
            dir.Normalize();
            shuriken.GetComponent<Rigidbody2D>().velocity = dir * 10.0f;
            lastShuriken = Time.fixedTime;
        }        
        //We set a maximum velocity to fall
        if (!changingGravity || rotating)
        {                        
            if (gameObject.GetComponent<Rigidbody2D>().velocity.x > 199) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(199.0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            else if(gameObject.GetComponent<Rigidbody2D>().velocity.x < -199) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-199.0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y > 199) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 199.0f);
            else if (gameObject.GetComponent<Rigidbody2D>().velocity.y < -199) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, -199.0f);
            if (!rotated && rotating) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
        //If the player is dashing we instantiate the dash shadows
        if (dashing)
        {
            dash = Instantiate(dashPrefab, transform.position, transform.rotation);
            if (!m_FacingRight && (gravity == 0 || gravity == 3))
            {
                Vector3 theScale = dash.transform.localScale;
                theScale.x *= -1;
                dash.transform.localScale = theScale;
            }
            else if (m_FacingRight && (gravity == 1 || gravity == 2))
            {
                Vector3 theScale = dash.transform.localScale;
                theScale.x *= -1;
                dash.transform.localScale = theScale;
            }
        }
        //We make the player rotate depending on her actual rotation and aim rotation
        if (gameObject.GetComponent<Rigidbody2D>().rotation != rotation && rotating)
        {
            if ((rotation > gameObject.GetComponent<Rigidbody2D>().rotation) && !(rotation == 270 && gameObject.GetComponent<Rigidbody2D>().rotation < 1.0f))
            {
                if ((rotation - gameObject.GetComponent<Rigidbody2D>().rotation) < 5.0f) gameObject.GetComponent<Rigidbody2D>().rotation = rotation;
                else gameObject.GetComponent<Rigidbody2D>().rotation += 10.0f;
            }
            else if ((rotation < gameObject.GetComponent<Rigidbody2D>().rotation) && !(rotation == 0 && gameObject.GetComponent<Rigidbody2D>().rotation > 269.0f))
            {
                if ((gameObject.GetComponent<Rigidbody2D>().rotation - rotation) < 5.0f) gameObject.GetComponent<Rigidbody2D>().rotation = rotation;
                else gameObject.GetComponent<Rigidbody2D>().rotation -= 10.0f;
            }
            else if (gameObject.GetComponent<Rigidbody2D>().rotation < 1.0f && rotation == 270) gameObject.GetComponent<Rigidbody2D>().rotation = 360;
            else if (gameObject.GetComponent<Rigidbody2D>().rotation > 269.0f && rotation == 0) gameObject.GetComponent<Rigidbody2D>().rotation = -90;
        }
        gravityDamage = 0.0f;
        //if the player is on the floor and has 3 gravity or more she takes damage per second
        if (m_Grounded && (gravityDown > 3.05f || gravityUp > 3.05f || gravityLeft > 3.05f || gravityRight > 3.05f))
        {
            if (gravity == 0) gravityDamage = gravityDown / 50.0f;
            else if (gravity == 1) gravityDamage = gravityUp / 50.0f;
            else if (gravity == 2) gravityDamage = gravityLeft / 50.0f;
            else if (gravity == 3) gravityDamage = gravityRight / 50.0f;
            healthBar.GetComponent<PlayerLifeController>().receiveDamage(gravityDamage);
        }

        //if the player falls in more than 20 velocity she takes instant damage
        if (!rotating && ((Mathf.Abs(Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x) - Mathf.Abs(lastVelocity.x)) > 20.0f && Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x) < (gravityLeft + gravityRight + 1.0f)) || (Mathf.Abs(Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) - Mathf.Abs(lastVelocity.y)) > 20.0f && Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) < (gravityUp + gravityDown + 1.0f))))
        {
            if (gravity == 0 || gravity == 1)
            {
                gravityDamage = Mathf.Abs(lastVelocity.y) / 2.0f;
            }
            else
            {
                gravityDamage = Mathf.Abs(lastVelocity.x) / 2.0f;
            }
            healthBar.GetComponent<PlayerLifeController>().receiveDamage(gravityDamage);
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
    //Function to start the attack
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
        if (hasStamina) attacking = animator.GetBool("isAttacking");
        else
        {
            attacking = false;
            animator.SetBool("isAttacking", false);
        }
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
        Physics2D.IgnoreLayerCollision(8,9,true);
    }

    //function to end the dash
    public void endDash()
    {
        dashing = false;
        animator.SetBool("isDashing", false);
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
    //function to start the damage audio
    public void startDamage()
    {
        gameObject.GetComponent<AudioSource>().clip = damageClip;
        gameObject.GetComponent<AudioSource>().Play();
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
            if (m_FacingRight) slashPos = new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z);
            else slashPos = new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z);
            slashRotation = 90;
        }
        if (gravity == 1)
        {
            if (m_FacingRight) slashPos = new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z);
            else slashPos = new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z);
            slashRotation = 90;
        }
        if (gravity == 2)
        {
            if (m_FacingRight) slashPos = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
            else slashPos = new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z);
            slashRotation = 0;
        }
        if (gravity == 3)
        {
            if (m_FacingRight) slashPos = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
            else slashPos = new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z);
            slashRotation = 0;
        }
        gameObject.GetComponent<AudioSource>().clip = attackClip;
        gameObject.GetComponent<AudioSource>().Play();
        Instantiate(slashPrefab, slashPos, Quaternion.AngleAxis(slashRotation, Vector3.forward));
    }

    //function to return to normal gravity when the player runs out of mana
    public void changeGravity(bool mana, float g0, float g1, float g2, float g3)
    {
        hasMana = mana;
        gravityDown = g0;
        gravityUp = g1;
        gravityLeft = g2;
        gravityRight = g3;
        rotating = true;
        prevVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
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
            m_FacingRight = !m_FacingRight;
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

    //function to move the player
    public void Move(float move, bool jump, int gravity, bool pausedMove)
    {
        //only control the player if not paused
        if (!pausedMove)
        {
            Vector3 targetVelocity;
            //Move the character by finding the target velocity
            if (gravity < 2) targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            else targetVelocity = new Vector2(m_Rigidbody2D.velocity.x, move * 10f);

            //And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            //If the input is moving the player right and the player is facing left flip the player
            if (move > 0 && !m_FacingRight) Flip();
            //Otherwise if the input is moving the player left and the player is facing right flip the player
            else if (move < 0 && m_FacingRight) Flip(); 
        }
        //If the player should jump
        if (m_Grounded && jump)
        {
            //Add a vertical force to the player.
            gameObject.GetComponent<AudioSource>().clip = jumpClip;
            gameObject.GetComponent<AudioSource>().Play();
            m_Grounded = false;
            if (gravity == 0) m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            else if (gravity == 1) m_Rigidbody2D.AddForce(new Vector2(0f, -m_JumpForce));
            else if (gravity == 2) m_Rigidbody2D.AddForce(new Vector2(m_JumpForce, 0));
            else if (gravity == 3) m_Rigidbody2D.AddForce(new Vector2(-m_JumpForce, 0));
        }
    }

    //Function to flip the player
    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //The player can deal damage to the enemies falling into them, but she will take damage too
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "HeavyBandit")
        {
            if (Mathf.Abs(lastVelocity.y) > 20.0f)
            {
                gravityDamage = Mathf.Abs(lastVelocity.y) / 2.0f;
                collision.transform.GetComponent<HeavyBanditScript>().damage = gravityDamage * 4;
                collision.transform.GetComponent<Animator>().SetBool("TakeDamage", true);
            }
            else if (Mathf.Abs(lastVelocity.x) > 20.0f)
            {
                gravityDamage = Mathf.Abs(lastVelocity.x) / 2.0f;
                collision.transform.GetComponent<HeavyBanditScript>().damage = gravityDamage * 4;
                collision.transform.GetComponent<Animator>().SetBool("TakeDamage", true);
            }
            healthBar.GetComponent<PlayerLifeController>().receiveDamage(gravityDamage);
        }
        else if (collision.transform.tag == "Knight")
        {            
            if (Mathf.Abs(lastVelocity.y) > 20.0f)
            {
                gravityDamage = Mathf.Abs(lastVelocity.y) / 2.0f;
                collision.transform.GetComponent<KnightScript>().damage = gravityDamage * 4;
                collision.transform.GetComponent<Animator>().SetBool("isTakingDamage", true);
            }
            else if (Mathf.Abs(lastVelocity.x) > 20.0f)
            {
                gravityDamage = Mathf.Abs(lastVelocity.x) / 2.0f;
                collision.transform.GetComponent<KnightScript>().damage = gravityDamage * 4;
                collision.transform.GetComponent<Animator>().SetBool("isTakingDamage", true);
            }
            healthBar.GetComponent<PlayerLifeController>().receiveDamage(gravityDamage);
        }
        else if (collision.transform.tag == "King")
        {
            if (Mathf.Abs(lastVelocity.y) > 20.0f)
            {
                gravityDamage = Mathf.Abs(lastVelocity.y) / 2.0f;
                collision.transform.GetComponent<KingScript>().damage = gravityDamage * 4;
            }
            else if (Mathf.Abs(lastVelocity.x) > 20.0f)
            {
                gravityDamage = Mathf.Abs(lastVelocity.x) / 2.0f;
                collision.transform.GetComponent<KingScript>().damage = gravityDamage * 4;
            }
            healthBar.GetComponent<PlayerLifeController>().receiveDamage(gravityDamage);
        }
    }

    //When we click outside the game the menu will open 
    private void OnApplicationPause(bool appPause)
    {
        if (appPause && !resting && !animator.GetBool("isDead"))
        {
            paused = true;
            Time.timeScale = 0.0f;
        }
    }
    //function to start the death audio
    public void startDie()
    {
        musicSource.Stop();
        gameObject.GetComponent<AudioSource>().clip = deathClip;
        gameObject.GetComponent<AudioSource>().Play();
    }
    //function to enter the dead state
    public void endDie()
    {
        dead = true;
    }

}
