using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpeed : MonoBehaviour {

	[SerializeField]
	float objectSpeed;

	[SerializeField]
	float minObjectSpeed = 4;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		objectSpeed = GetComponent<Rigidbody>().velocity.magnitude;

		if (objectSpeed < minObjectSpeed && objectSpeed > 0)
		{
			GetComponent<Rigidbody>().drag = 0;
		}
	}
}
