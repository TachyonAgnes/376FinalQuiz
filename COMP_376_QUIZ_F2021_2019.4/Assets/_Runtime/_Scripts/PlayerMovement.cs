using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ------------------------------------------------------------------------------ 
// Quiz 
// Written by: Zisen Ling & 40020293
// For COMP 376 – Fall 2021 
// ----------------------------------------------------------------------------- 
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private CharacterController controller;
    [SerializeField, Min(0)] private float speed = 5f;
    [SerializeField, Min(0)] private float rotationSpeed = 10f;

    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField, Min(0)] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField, Min(0)] private float jumpHeight = 2f;
    [SerializeField] private Text coin;


    private Vector3 movement;
    private Vector3 gravitationalForce;
    private bool isGrounded;
    private bool jumpMomentumCheck;
    private bool marioRun;
    private bool marioJump;
    private bool rotating;
    private float rotateTimer;
    private Animator animator;
    private Animator win_animator;
    public static GameObject win_obj;
    public static GameObject mario;
    private Vector3 marioScale;
    private float localJumpHeight;
    private float boostTimer;
    private bool isBoosting;
    private int coinCount;
    //private bool marioPunch;

    private AudioSource marioJumpSound;
    private AudioSource coinSound;

    private void Start()
    {   
        //get mario animator 
        mario = GameObject.Find("mario64_animated");
        animator = mario.GetComponent<Animator>();
        win_obj = GameObject.Find("star_Position");
        win_animator = win_obj.GetComponent<Animator>();
        //initialize
        marioJump = false;
        marioRun = false;
        rotating = false;
        coinCount = 0;
        transform.GetChild(0).gameObject.SetActive(false);
        //add soundEffect
        AudioSource[] audioSources = GetComponents<AudioSource>();
        marioJumpSound = audioSources[0];
        coinSound = audioSources[1];
        rotateTimer = 0;
        marioScale = mario.transform.localScale;
        localJumpHeight = jumpHeight;
    }

    private void Update()
    {
        // calculate movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cam.transform.right, Vector3.up).normalized;
        movement = right * horizontal + forward * vertical;


        // check if player is trying to move
        if (movement != Vector3.zero)
        {
            //// look in the direction of the movement
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed * Time.deltaTime);
            marioRun = true;
        }
        else
        {
            marioRun = false;
        }

        // check if mario is grounded
        isGrounded = false;
        Collider[] hitColliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, whatIsGround);
        for (int i = 0; i < hitColliders.Length; ++i)
        {
            if (hitColliders[i].gameObject == this.gameObject)
                continue;

            if (!hitColliders[i].isTrigger)
            {
                isGrounded = true;
                break;
            }
        }

        jumpMomentumCheck = jumpMomentumCheck && Input.GetButton("Jump") && !isGrounded;

        // simulate gravity
        if (isGrounded)
        {
            // mario is standing on ground
            gravitationalForce.y = gravity * Time.deltaTime;
            jumpMomentumCheck = true;
        }
        else
        {
            // mario is in the air
            if (!jumpMomentumCheck && gravitationalForce.y > 0)
                gravitationalForce.y = 0;
            else
            {
                gravitationalForce.y += gravity * Time.deltaTime;
            }
        }

        // jump
        if (Input.GetButton("Jump") && isGrounded)
        {
            gravitationalForce.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
            marioJump = true;
            marioJumpSound.Play();
        }
        else
        {
            marioJump = false;
        }

        if (isBoosting)
        {
            boostTimer += Time.deltaTime;
            if(boostTimer >= 3)
            {
                speed = 5f;
                boostTimer = 0;
                isBoosting = false;
                mario.transform.localScale = marioScale;
                jumpHeight = localJumpHeight;
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if (rotating)
        {
            rotateTimer += Time.deltaTime;
            gameObject.transform.Rotate(0, 10.0f, 0, Space.Self);
            if (rotateTimer >= 2)
            {
                rotateTimer = 0;
                rotating = false;
            }
        }

        // move mario
        if (!rotating)
        {
            controller.Move((movement * speed * Time.deltaTime) + (gravitationalForce * Time.deltaTime));
            UpdateAnimator();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //print(collision.collider.gameObject.tag);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Coin")
        {
            coinSound.Play();
            coinCount++;
            Destroy(col.gameObject);
            coin.text = coinCount.ToString();
            isBoosting = true;
            speed = 7.5f;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (col.tag == "Thwomp")
        {
            HealthBar.current -= 20;
        }
        if (col.tag == "Bobomb")
        {
            col.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
            Destroy(col.gameObject, 0.5f);
            HealthBar.current -= 10;
        }
        if (col.tag == "Flag")
        {
            win_animator.SetBool("isWin", true);
            HealthBar.isWin = true;
        }
        if (col.tag == "Boo")
        {
            rotating = true;
        }
        if (col.tag == "RedCoin")
        {
            coinSound.Play();
            coinCount++;
            coinCount++;
            Destroy(col.gameObject);
            coin.text = coinCount.ToString();
            isBoosting = true;
            speed = 15f;
            mario.transform.localScale = (mario.transform.localScale)* 0.2f;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (col.tag == "BlueCoin")
        {
            coinSound.Play();
            coinCount++;
            coinCount++;
            coinCount++;
            Destroy(col.gameObject);
            coin.text = coinCount.ToString();
            isBoosting = true;
            speed = 5f;
            jumpHeight = 10f;
            transform.GetChild(0).gameObject.SetActive(true);
        }

    }


    private void UpdateAnimator()
    {
        animator.SetBool("Run", marioRun);
        animator.SetBool("Jump", marioJump);
        //animator.SetBool("Punch", marioPunch);
    }

}


