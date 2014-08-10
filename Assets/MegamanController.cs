using UnityEngine;
using System.Collections;

public class MegamanController : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.

	//Accessible editor properties
	public float horizontalForce = 1.0f;
	public float jumpForce = 1.0f;
	public float maxVel = 1.0f;
	public bool canJump = false;

	// Use this for initialization
	public void Start() {
		
	}

	void Flip () {
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	public void Update() {
		Transform groundCheckA = transform.Find("groundDetectionA");
		Transform groundCheckB = transform.Find("groundDetectionB");
		canJump = Physics2D.Linecast(transform.position, groundCheckA.position, 1 << LayerMask.NameToLayer("Ground"))
			|| Physics2D.Linecast(transform.position, groundCheckB.position, 1 << LayerMask.NameToLayer("Ground"));
		
		GetComponent<Animator>().SetBool("grounded", canJump);
		GetComponent<Animator>().SetFloat("horizontalVelocity", Mathf.Abs(rigidbody2D.velocity.x));
	}
	
	// Update is called once per frame
	public void FixedUpdate() {
		if(canJump) {
			if(Input.GetButton("Jump")) {
				rigidbody2D.AddForce(new Vector2(0, jumpForce * rigidbody2D.mass));
				canJump = false;
			}

			float h = Input.GetAxis("Horizontal");
			float impulse = horizontalForce * h;
			float xvel = rigidbody2D.velocity.x;

			if(h > 0 && !facingRight) {
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(h < 0 && facingRight) {
				// ... flip the player.
				Flip();
			}

			if(Mathf.Abs(impulse + xvel) > maxVel) {
				if(impulse > 0) {
					rigidbody2D.velocity = Vector2.right * maxVel;
				} else {
					rigidbody2D.velocity = Vector2.right * -maxVel;
				}
			} else {
				rigidbody2D.AddForce(new Vector2(impulse * rigidbody2D.mass, 0));
			}
		}
	}
}
