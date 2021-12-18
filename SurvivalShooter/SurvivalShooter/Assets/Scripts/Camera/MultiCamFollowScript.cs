using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCamFollowScript : MonoBehaviour
{
	public Transform target;
	public float smoothing = 5f;
	bool playerSpawned = false;
	private Vector3 offset;


	public void SetupOffset(Transform t)
	{
		target = t;
		offset = transform.position - target.position;
		playerSpawned = true;
	}

	void FixedUpdate()
	{
		if (!playerSpawned)
			return;

		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
