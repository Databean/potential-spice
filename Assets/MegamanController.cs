using UnityEngine;
using System.Collections;

public class MegamanController : MonoBehaviour {

	//Accessible editor properties
	public float horizontalForce = 1.0f;

	// Use this for initialization
	public void Start() {
		
	}
	
	// Update is called once per frame
	public void Update() {
		float force = horizontalForce * Input.GetAxis("Horizontal") * rigidbody2D.mass;
		rigidbody2D.AddForce(new Vector2(force, 0));

		GetComponent<Animator>().SetFloat("horizontalVelocity", rigidbody2D.velocity.x);
	}
}
