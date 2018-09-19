using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingAnim : MonoBehaviour {

    RectTransform img;

    public AnimationCurve anim;

    public float speed = 5;

    [SerializeField]
    float amount;

	// Use this for initialization
	void Start () {
        img = GetComponent<RectTransform>();

        img.localScale = new Vector3(0.1f, 0.1f, 0.1f);
	}

	// Update is called once per frame
	void Update () {
        float time = Time.time * speed;
        amount = Mathf.Clamp01(Mathf.Sin(time)) + Mathf.Clamp01(Mathf.Sin(time) * -1);
        //amount = Mathf.Tan(time) + 1; //I'm just playing around on this one.
        Vector3 scale = new Vector3(amount, amount, amount);

        img.localScale = scale;
    }
}
