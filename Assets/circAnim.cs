using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using DG.Tweening;

public class circAnim : MonoBehaviour {

    RawImage img;

    [SerializeField]
    float speed = 1;

    //float MAX_OPACITY = 255;

	// Use this for initialization
	void Start () {
        img = gameObject.GetComponent<RawImage>();
	}

	// Update is called once per frame
	void Update () {
        //float finalOpacity = MAX_OPACITY * Mathf.Sin(Time.time);
        img.color = new Color(1, 1, 1, (Mathf.Sin(Time.time * speed) + 1) / 2);

    }
}
