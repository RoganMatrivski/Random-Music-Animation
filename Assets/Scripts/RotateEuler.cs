using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEuler : MonoBehaviour {

	public float x = 0;
	public float y = 0;
	public float z = 0;

	Vector3 vec3 = new Vector3();

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		vec3 = new Vector3(x, y, z);

        transform.rotation = Quaternion.Euler(vec3);
	}
}
