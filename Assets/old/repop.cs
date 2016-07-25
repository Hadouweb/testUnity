using UnityEngine;
using System.Collections;

public class repop : MonoBehaviour {

	public GameObject prefab;	
	public Transform t;

	void OnTriggerEnter(Collider col) {
//		Debug.Log (col.name);
		GameObject parent = col.transform.parent.gameObject;
		if (parent) {
			parent = parent.transform.parent.gameObject;
		}

		if (parent.GetComponent<Rigidbody> ()) {
			//Debug.Log (col.name);
			parent.transform.position = t.position;
			parent.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			parent.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		}
	}

}
