using UnityEngine;
using System.Collections;

public enum Players
{
    Player1 = 1,
    Player2 = 2
}

public class StickManControllerScript : MonoBehaviour {

    public Players player = Players.Player1;
    public float maxSpeed = 10f;
    public float jumpForce = 1550f;
    public float jumpSpeedModifier = 0.7f;

    private bool facingRight = false;    
	private bool dualPlay = false;

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

		if (GameObject.Find("Character 2") != null)
		{
			dualPlay = true;
		}
    }
		
	void FixedUpdate () {
		var isPlayer1 = player == Players.Player1;
		Camera cam = Camera.main;
		Vector3 position = cam.gameObject.transform.position;
		float height = cam.orthographicSize;
		if (position.y - height >  this.transform.position.y){
			string gagnant = isPlayer1 ? " 2 " : " 1 ";
			if (dualPlay) {
				UnityEditor.EditorUtility.DisplayDialog ("Fin de la partie", "La partie est terminé\nLe joueur"+ gagnant+"a gagné après: " + Time.time + " secondes", "OK");
			} else {
				UnityEditor.EditorUtility.DisplayDialog ("Fin de la partie", "Vous avez perdu!\nVous avez survévu pendant: " + Time.time + " secondes", "OK");
			}
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
			
        //Custom axis can be found in: Edit > Project Settings > Input
        var axis = isPlayer1 ? "Horizontal" : "Horizontal2";

        float move = Input.GetAxis(axis);
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
        var jumpKey = player == Players.Player1 ? KeyCode.Space : KeyCode.W;
        if (grounded && Input.GetKeyDown(jumpKey))
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
