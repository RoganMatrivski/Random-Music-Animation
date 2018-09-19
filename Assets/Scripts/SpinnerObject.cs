using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerObject : MonoBehaviour {

    public float spinningSpeed = 1f;

    public bool enableRotation = true;

    // Update is called once per frame
	void Update () {
        if (enableRotation)
        {
            //float rot = transform.rotation.eulerAngles.y + spinningSpeed;
            //transform.Rotate(new Vector3());
            transform.Rotate(new Vector3(0, 0, spinningSpeed));
        }

    }
}
