using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingLookAt : MonoBehaviour {

    public GameObject a;

    public Vector3 b;

	// Update is called once per frame
	void Update () {
        //transform.LookAt(a.transform, b);
        var dir = a.transform.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
	}
}
