using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stop : MonoBehaviour
{
	// Careful when setting this to true - it might cause double
	// events to be fired - but it won't pass through the trigger
	public bool sendTriggerMessage = false; 	

	public LayerMask layerMask = -1; //make sure we aren't in this layer 
	public float skinWidth = 0.1f; //probably doesn't need to be changed 

	private float minimumExtent; 
	private float partialExtent; 
	private float sqrMinimumExtent; 
	private Vector3 previousPosition; 
	private Rigidbody myRigidbody;
	private List<Collider> myColliders = new List<Collider>();
	private Collider myCollider;
	public GameObject parentCollider;
	private float elapsedTime = 0f;
	private int countFrame = 0;

	//initialize values 
	void Start() 
	{ 
		myRigidbody = GetComponent<Rigidbody>();
		minimumExtent = 1000f;

		for (int i = 0; i < parentCollider.transform.childCount; i++) {
			myColliders.Add(parentCollider.transform.GetChild (i).GetComponent<Collider>());
			float tmpMin = Mathf.Min(Mathf.Min(myColliders[i].bounds.extents.x, myColliders[i].bounds.extents.y),
				myColliders[i].bounds.extents.z); 
			if (tmpMin < minimumExtent) {
				minimumExtent = tmpMin;
			}
		}
		previousPosition = myRigidbody.position;
		partialExtent = minimumExtent * (1.0f - skinWidth); 
		sqrMinimumExtent = minimumExtent * minimumExtent; 
	} 

	void FixedUpdate() 
	{ 
		//have we moved more than our minimum extent? 
		Vector3 movementThisStep = myRigidbody.position - previousPosition; 
		float movementSqrMagnitude = movementThisStep.sqrMagnitude;

		if (movementSqrMagnitude > sqrMinimumExtent) 
		{ 
			float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
			RaycastHit hitInfo; 

			//check for obstructions we might have missed 
			if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
			{
				if (!hitInfo.collider)
					return;

				if (!hitInfo.collider.isTrigger) {
					if (hitInfo.collider.transform.parent.name != parentCollider.name) {
						myRigidbody.position = hitInfo.point - (movementThisStep / movementMagnitude) * partialExtent;
						//Debug.Log ("reset " + hitInfo.collider.name);	
					}
				}

			}
		} 

		previousPosition = myRigidbody.position; 
	}

}
