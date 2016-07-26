using UnityEngine;
using System.Collections;

public class testCollision : MonoBehaviour {

	public testCollisionParent triggerManager;

	void OnTriggerEnter(Collider col) {
		if (col.isTrigger == false) {
			triggerManager.isTrigger = true;
			triggerManager.objectInTrigger = col.gameObject;
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.isTrigger == false) {
			triggerManager.isTrigger = false;
			triggerManager.objectInTrigger = null;
		}
	}

}
