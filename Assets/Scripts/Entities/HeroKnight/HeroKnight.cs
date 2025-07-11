﻿using UnityEngine;
using UnityEngine.UI;

public class HeroKnight : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = PlayerConstants.DEFAULT_MOVEMENT_SPEED;

    [SerializeField]
    private float jumpForce = PlayerConstants.DEFAULT_JUMP_FORCE;

    [SerializeField]
    private float rollForce = PlayerConstants.DEFAULT_ROLL_FORCE;

    [SerializeField]
    private float rollCooldown = 3f;

    [SerializeField]
    private float blockCooldown = 3f;

    [SerializeField]
    private float attackCooldown = 0.5f;

    [SerializeField]
    private int health = 100;

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private AudioClip attackSound;

    [SerializeField]
    private AudioClip rollSound;

    [SerializeField]
    private AudioClip jumpSound;

    [SerializeField]
    private AudioClip dieSound;

    [SerializeField]
    private AudioClip hurtSound;

    [SerializeField]
    private AudioClip walkSound;

    [SerializeField]
    private AudioClip blockSound;

    public GameManager gameManager;
    public PlayerAttackZone attackZone;

    private Animator animator;
    private Rigidbody2D rigibody;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private Sensor_HeroKnight groundSensor;
    private Sensor_HeroKnight wallSensorR1;
    private Sensor_HeroKnight wallSensorR2;
    private Sensor_HeroKnight wallSensorL1;
    private Sensor_HeroKnight wallSensorL2;
    public GameObject slideDust;

    private bool isWallSliding = false;
    private bool isGrounded = false;
    private bool isRolling = false;
    private bool isBlocking = false;
    public bool isDead = false;

    private int facingDirection = 1;
    private int currentAttack = 0;

    private float delayToIdle = 0.0f;

    private readonly float rollDuration = 0.643f;
    private readonly float blockDuration = 0.35f;
    private readonly float deathDuration = 2.0f;
    private readonly float stepDuration = 0.305f;

    private float rollCurrentTime;
    private float blockCurrentTime;

    private float timeSinceLastRoll = 0.0f;
    private float timeSinceLastBlock = 0.0f;
    private float timeSinceLastAttack = 0.0f;
    private float timeSinceLastStep = 0f;

    private const KeyCode ROLL_KEY = KeyCode.LeftShift;
    private const KeyCode JUMP_KEY = KeyCode.Space;
    private const KeyCode ATTACK_KEY = KeyCode.Z;
    private const KeyCode BLOCK_KEY = KeyCode.X;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigibody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundSensor = transform.Find(PlayerConstants.GROUND_SENSOR_NAME).GetComponent<Sensor_HeroKnight>();
        wallSensorR1 = transform.Find(PlayerConstants.R1_SENSOR_NAME).GetComponent<Sensor_HeroKnight>();
        wallSensorR2 = transform.Find(PlayerConstants.R2_SENSOR_NAME).GetComponent<Sensor_HeroKnight>();
        wallSensorL1 = transform.Find(PlayerConstants.L1_SENSOR_NAME).GetComponent<Sensor_HeroKnight>();
        wallSensorL2 = transform.Find(PlayerConstants.L2_SENSOR_NAME).GetComponent<Sensor_HeroKnight>();
    }

    void Update()
    {
        if (isDead) return;

        // Update walk step timer
        timeSinceLastStep += Time.deltaTime;

        // Increase timer that controls attack combo
        timeSinceLastAttack += Time.deltaTime;

        // Increase timer that controls block cooldown
        timeSinceLastBlock += Time.deltaTime;

        // Increase timer that controls roll cooldown
        timeSinceLastRoll += Time.deltaTime;

        // Increase timer that checks roll duration
        if (isRolling)
            rollCurrentTime += Time.deltaTime;

        // Increase timer that checks block duration
        if (isBlocking)
            blockCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if (rollCurrentTime > rollDuration)
        {
            isRolling = false;
            rollCurrentTime = 0.0f;
        }

        if (blockCurrentTime > blockDuration)
        {
            isBlocking = false;
            blockCurrentTime = 0.0f;
        }

        //Check if character just landed on the ground
        if (!isGrounded && groundSensor.State())
        {
            isGrounded = true;
            animator.SetBool("Grounded", isGrounded);
        }

        //Check if character just started falling
        if (isGrounded && !groundSensor.State())
        {
            isGrounded = false;
            animator.SetBool("Grounded", isGrounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            spriteRenderer.flipX = false;
            facingDirection = 1;
            if (timeSinceLastStep >= stepDuration && isGrounded && !isRolling && !isBlocking)
            {
                audioSource.PlayOneShot(walkSound);
                timeSinceLastStep = 0f;
            }

            // Flip AttackZone
            if (attackZone.transform.localPosition.x < 0)
            {
                FlipAttackZone();
            }
        }
        else if (inputX < 0)
        {
            spriteRenderer.flipX = true;
            facingDirection = -1;
            if (timeSinceLastStep >= stepDuration && isGrounded && !isRolling && !isBlocking)
            {
                audioSource.PlayOneShot(walkSound);
                timeSinceLastStep = 0f;
            }

            // Flip AttackZone
            if (attackZone.transform.localPosition.x > 0)
            {
                FlipAttackZone();
            }
        }

        // Move
        if (!isRolling)
            rigibody.linearVelocity = new Vector2(inputX * movementSpeed, rigibody.linearVelocity.y);

        //Set AirSpeed in animator
        animator.SetFloat("AirSpeedY", rigibody.linearVelocity.y);

        // -- Handle Animations --
        //Wall Slide
        isWallSliding = (wallSensorR1.State() && wallSensorR2.State()) || (wallSensorL1.State() && wallSensorL2.State());
        animator.SetBool("WallSlide", isWallSliding);

        //Attack
        if (Input.GetKeyDown(ATTACK_KEY) && timeSinceLastAttack > attackCooldown && !isRolling)
        {
            currentAttack++;

            // Loop back to one after third attack
            if (currentAttack > 3)
                currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (timeSinceLastAttack > attackCooldown * 2)
                currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            animator.SetTrigger("Attack" + currentAttack);
            audioSource.PlayOneShot(attackSound);

            timeSinceLastAttack = 0.0f;
            attackZone.AttackEnemiesInRange();
        }

        // Block
        else if (Input.GetKeyDown(BLOCK_KEY) && !isRolling && !isBlocking && timeSinceLastBlock > blockCooldown)
        {
            isBlocking = true;
            timeSinceLastBlock = 0.0f;
            animator.SetTrigger("Block");
        }

        // Roll
        else if (Input.GetKeyDown(ROLL_KEY) && !isRolling && !isBlocking && timeSinceLastRoll > rollCooldown)
        {
            isRolling = true;
            timeSinceLastRoll = 0.0f;
            animator.SetTrigger("Roll");
            audioSource.PlayOneShot(rollSound);
            rigibody.linearVelocity = new Vector2(facingDirection * rollForce, rigibody.linearVelocity.y);
        }

        //Jump
        else if (Input.GetKeyDown(JUMP_KEY) && isGrounded && !isRolling)
        {
            animator.SetTrigger("Jump");
            audioSource.PlayOneShot(jumpSound);
            isGrounded = false;
            animator.SetBool("Grounded", isGrounded);
            rigibody.linearVelocity = new Vector2(rigibody.linearVelocity.x, jumpForce);
            groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            delayToIdle = 0.05f;
            animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            delayToIdle -= Time.deltaTime;
            if (delayToIdle < 0)
                animator.SetInteger("AnimState", 0);
        }
    }

    private void FlipAttackZone()
    {
        Vector3 pos = attackZone.transform.localPosition;
        pos.x *= -1;
        attackZone.transform.localPosition = pos;
    }

    public void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (facingDirection == 1)
            spawnPosition = wallSensorR2.transform.position;
        else
            spawnPosition = wallSensorL2.transform.position;

        if (slideDust != null)
        {
            GameObject dust = Instantiate(slideDust, spawnPosition, gameObject.transform.localRotation);
            dust.transform.localScale = new Vector3(facingDirection, 1, 1);
        }
    }

    public void TakeDamage(int amount)
    {
        if (CanTakeDamage())
        {
            health -= amount;
            animator.SetTrigger("Hurt");
            healthBar.fillAmount = health / 100f;
            if (health <= 0)
            {
                animator.SetTrigger("Death");
                audioSource.PlayOneShot(dieSound);
                isDead = true;
                Invoke(nameof(ShowDeathMenu), deathDuration);
            }
            else
            {
                audioSource.PlayOneShot(hurtSound);
            }
        }
        else if (isBlocking)
        {
            animator.SetTrigger("BlockSuccess");
            audioSource.PlayOneShot(blockSound);
        }
    }

    private void ShowDeathMenu()
    {
        gameManager.ShowDeathMenu();
    }

    private bool CanTakeDamage()
    {
        return !isRolling && !isBlocking && !isDead;
    }
}
