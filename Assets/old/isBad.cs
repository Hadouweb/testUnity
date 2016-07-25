using UnityEngine;
using System.Collections;

public class isBad : MonoBehaviour {

	private float elapsedTime = 0f;
	private int countFrame = 0;

	void OnTriggerEnter(Collider col) {
		Debug.Log ("BAD");
	}

	void Update() {
		elapsedTime += Time.deltaTime;
		countFrame++;
		if (elapsedTime >= 1f) {
			elapsedTime = 0f;
			//Debug.Log (countFrame);
			countFrame = 0;
		}
	}

}
