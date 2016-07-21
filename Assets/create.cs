using UnityEngine;
using System.Collections;

public class create : MonoBehaviour {

	public GameObject go;
	public Transform t;

	void OnCollisionEnter(Collision collision) {
		if (go.transform.childCount < 10 && collision.collider.name == "WeaponSword000") {
			Instantiate (go, t.position, t.rotation);
		}
	}
}
