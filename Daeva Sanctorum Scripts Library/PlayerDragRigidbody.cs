using UnityEngine;
using System.Collections;

//Script Obtained from: http://pastebin.com/j3DWqe3R
//I tweaked the script a bit so that the raycasts emit in the center of the screen

//I tweaked this script a bit so the raycast will come out in the middle of the camera
//I also tweaked it so it only requires a click or button press

//Tweaky tweaky, I'm butt hurt and squeeky...

public class PlayerDragRigidbody : MonoBehaviour
{
	public float maxDistance = 100.0f;
	
	public float spring = 50.0f;
	public float damper = 5.0f;
	public float drag = 10.0f;
	public float angularDrag = 5.0f;
	public float distance = 0.2f;
	public float throwForce = 500f;
	public float throwRange = 1000f;
	public bool attachToCenterOfMass = false;

	public static bool click;

	
	private SpringJoint springJoint;
	
	void Update()
	{
		if(!Input.GetButtonDown("Fire1"))
			return;
		
		RaycastHit hit;
		if(!Physics.Raycast(camera.ViewportPointToRay (new Vector3(0.5f, 0.5f, 0f)), out hit, maxDistance))
			return;
		if(!hit.rigidbody || hit.rigidbody.isKinematic)
			return;
		
		if(!springJoint)
		{
			GameObject go = new GameObject("Rigidbody dragger");
			Rigidbody body = go.AddComponent<Rigidbody>();
			body.isKinematic = true;
			springJoint = go.AddComponent<SpringJoint>();
		}
		
		springJoint.transform.position = hit.point;
		if(attachToCenterOfMass)
		{
			Vector3 anchor = transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
			anchor = springJoint.transform.InverseTransformPoint(anchor);
			springJoint.anchor = anchor;
		}
		else
		{
			springJoint.anchor = Vector3.zero;
		}
		
		springJoint.spring = spring;
		springJoint.damper = damper;
		springJoint.maxDistance = distance;
		springJoint.connectedBody = hit.rigidbody;
		
		StartCoroutine(DragObject(hit.distance));
	}
	
	IEnumerator DragObject(float distance)
	{
		float oldDrag             = springJoint.connectedBody.drag;
		float oldAngularDrag     = springJoint.connectedBody.angularDrag;
		springJoint.connectedBody.drag             = this.drag;
		springJoint.connectedBody.angularDrag     = this.angularDrag;
		
		while(Input.GetButtonDown("Fire1"))
		{

			if(click)
			{
				click = false;
			}
			else
			{
				click = true;
			}

			yield return null;
		}

		while(click)
		{
			Ray ray = camera.ViewportPointToRay (new Vector3(0.5f, 0.5f, 0f));
			springJoint.transform.position = ray.GetPoint(distance);
			if(click && Input.GetButtonDown("Fire1"))
			{
				click = false;
			}
			yield return null;

			//Throw object if throw button is pressed
			if(Input.GetButtonDown ("Fire2") || Input.GetAxis ("Throw") == 1f)
			{
				springJoint.connectedBody.AddExplosionForce(throwForce,camera.transform.position,throwRange);
				springJoint.connectedBody.drag = oldDrag;
				springJoint.connectedBody.angularDrag = oldAngularDrag;
				springJoint.connectedBody = null;
				click = false;
			}
		}
		
		if(springJoint.connectedBody)
		{
			springJoint.connectedBody.drag             = oldDrag;
			springJoint.connectedBody.angularDrag     = oldAngularDrag;
			springJoint.connectedBody                 = null;
		}
	}
}