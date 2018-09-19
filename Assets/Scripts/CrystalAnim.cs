using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalAnim : MonoBehaviour {

    public float speed = 1f;
    public float amplitude = 2f;

	// Update is called once per frame
	void Update () {
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Sin(Time.time * speed) * amplitude, transform.localPosition.z);
	}
}
