using UnityEngine;
using System.Collections;

public class testCollisionParent : MonoBehaviour {

	public bool isTrigger = false;
	public GameObject objectInTrigger;
	public GameObject weapon;
	public GameObject follow;

	private Vector3 pos;
	private Quaternion rot;

	void FixedUpdate() {
		transform.position = follow.transform.position;
		transform.rotation = follow.transform.rotation;
		if (isTrigger == true && objectInTrigger.GetComponent<Rigidbody>() &&  objectInTrigger.GetComponent<Rigidbody>().isKinematic == true) {
			weapon.GetComponent<Rigidbody> ().isKinematic = true;
			Debug.Log (objectInTrigger);
		} else {
			weapon.GetComponent<Rigidbody> ().isKinematic = false;
			//weapon.transform.position = transform.position;
			//weapon.transform.rotation = transform.rotation;
		}
	}

}