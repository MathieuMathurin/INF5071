﻿using UnityEngine;
using System.Collections;

public class StickManControllerScript : MonoBehaviour {

    public float maxSpeed = 10f;
    public float jumpForce = 1000f;
    public float jumpSpeedModifier = 0.5f;

    private bool facingRight = false;    

    public bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;    

    Animator animator;
    Rigidbody2D rigidBody;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
		
	void FixedUpdate () {
		Camera cam = Camera.main;
		Vector3 position = cam.gameObject.transform.position;
		float height = cam.orthographicSize;
		if (position.y - height >  this.transform.position.y){
			UnityEditor.EditorUtility.DisplayDialog ("Fin de la partie", "Vous avez perdu!", "OK");
			UnityEditor.EditorApplication.isPlaying = false;
			Application.Quit();
		}
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        animator.SetBool("Ground", grounded);

        //animator.SetFloat("vSpeed", rigidBody.velocity.y);
        var speedModifier = 1f;

        if (!grounded)
        {
            speedModifier = jumpSpeedModifier;
        }

        float move = Input.GetAxis("Horizontal");
        var speed = Mathf.Abs(move);
        animator.SetFloat("Speed", speed * maxSpeed);


        rigidBody.velocity = new Vector2(move * maxSpeed * speedModifier, rigidBody.velocity.y);

        if(move > 0 && !facingRight)
        {
            Flip();
        }else if(move < 0 && facingRight)
        {
            Flip();
        }
	}


    void Update()
    {
        //Jump code.
        //TODO: Change GetKeyDown for GetKey (Allows to configure it)
        if(grounded && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Ground", false);
            rigidBody.AddForce(new Vector2(0, jumpForce));

        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
