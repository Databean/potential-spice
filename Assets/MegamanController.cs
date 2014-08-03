using UnityEngine;
using System.Collections;

public class MegamanController : MonoBehaviour {

	//Accessible editor properties
	public float horizontalForce = 1.0f;
	public float jumpForce = 1.0f;
	public float maxVel = 1.0f;
	public bool canJump = false;

	// Use this for initialization
	public void Start() {
		
	}
	
	public void Update() {
		Transform groundCheckA = transform.Find("groundDetectionA");
		Transform groundCheckB = transform.Find("groundDetectionB");
		canJump = Physics2D.Linecast(transform.position, groundCheckA.position, 1 << LayerMask.NameToLayer("Ground"))
			|| Physics2D.Linecast(transform.position, groundCheckB.position, 1 << LayerMask.NameToLayer("Ground"));
		
		GetComponent<Animator>().SetBool("grounded", canJump);
		GetComponent<Animator>().SetFloat("horizontalVelocity", rigidbody2D.velocity.x);
	}
	
	// Update is called once per frame
	public void FixedUpdate() {
		if(canJump) {
			if(Input.GetButton("Jump")) {
				rigidbody2D.AddForce(new Vector2(0, jumpForce * rigidbody2D.mass));
				canJump = false;
			}

			float impulse = horizontalForce * Input.GetAxis("Horizontal");
			float xvel = rigidbody2D.velocity.x;
			if(Mathf.Abs(impulse + xvel) > maxVel) {
				if(impulse > 0) {
					rigidbody2D.velocity = Vector2.right * maxVel;
				} else {
					rigidbody2D.velocity = Vector2.right * -maxVel;
				}
			} else {
				rigidbody2D.AddForce(new Vector2(impulse * rigidbody2D.mass, 0), 0);
			}
		}
	}
}
