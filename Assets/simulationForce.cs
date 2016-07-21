using UnityEngine;
using System.Collections;

public class simulationForce : MonoBehaviour {

	private Rigidbody rb;
	public float mulVel = 10f;
	private float elapsedTime = 0f;
	private int countFrame = 0;

	void Awake() {
		rb = GetComponent<Rigidbody> ();
		rb.maxAngularVelocity = 1000f;
	}

	void Start() {
		rb.velocity = rb.velocity.normalized * mulVel;
		rb.angularVelocity =  rb.velocity.normalized * mulVel;
	}

	void OnCollisionEnter(Collision collision) {
		//Debug.Log (collision.collider.name);
	}

	void FixedUpdate() {
		elapsedTime += Time.deltaTime;
		countFrame++;
		if (elapsedTime >= 1f) {
			elapsedTime = 0f;
			//Debug.Log (countFrame);
			countFrame = 0;
		}
		rb.velocity = rb.velocity.normalized * mulVel;
		rb.angularVelocity =  rb.velocity.normalized * mulVel * 2;
	}
		
}
