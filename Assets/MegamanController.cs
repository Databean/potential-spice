using UnityEngine;
using System.Collections;

public class MegamanController : MonoBehaviour {

	//Accessible editor properties
	public float horizontalForce = 1.0f;
	public float jumpForce = 1.0f;
	public bool canJump = false;

	// Use this for initialization
	public void Start() {
		
	}
	
	// Update is called once per frame
	public void Update() {
		GetComponent<Animator>().SetBool("grounded", canJump);
		GetComponent<Animator>().SetFloat("horizontalVelocity", rigidbody2D.velocity.x);

		if(canJump) {
			if(Input.GetButton("Jump")) {
				rigidbody2D.AddForce(new Vector2(0, jumpForce * rigidbody2D.mass));
				canJump = false;
			}

			float force = horizontalForce * Input.GetAxis("Horizontal") * rigidbody2D.mass;
			rigidbody2D.AddForce(new Vector2(force, 0));
		}
	}

	public void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("entered collision");
		canJump = true;
	}

	public void OnTriggerExit2D(Collider2D other) {
		Debug.Log("exited collisiion");
		canJump = false;
	}
}
