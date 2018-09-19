using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour {

	public float timeUntilDespawn = 5f;

	public float timeToTransition = 1f;

	float time = 0f;
	float timeInterval;

	//public Color matColor;
	//public Color newMat;
	//public Color oldMat;
	//public Color ColorAltered;

	public float test;

	public float objOpacity;

	[SerializeField]
	float floatVal;

	bool passed = false;

	// Use this for initialization
	void Start () {
		//matColor = gameObject.GetComponent<Renderer>().material.GetColor("_Color");
		//matColor = gameObject.GetComponent<Renderer>().material.color;
		//newMat = new Color(alpha.r, alpha.g, alpha.b, 0f);
		//oldMat = matColor;
		//newMat = matColor;
		//newMat.a = 0f;
	}

	// Update is called once per frame
	void Update () {
		objOpacity = GetComponent<Renderer>().material.GetFloat("_obj_opacity");
		//test = matColor.a;

		timeInterval = timeUntilDespawn - timeToTransition;
		floatVal = (timeInterval - (time - timeToTransition)) / timeInterval;

		time += Time.deltaTime;

		if (time >= timeUntilDespawn - timeToTransition)
		{
			passed = true;
			//ColorAltered = Color.Lerp(oldMat, newMat, floatVal);
			//matColor = ColorAltered;
			//matColor.a = Mathf.Lerp(0, 1, floatVal);
			//Mathf.Lerp()
			GetComponent<Renderer>().material.SetFloat("_obj_opacity", Mathf.Lerp(0, 1, floatVal));

		}

		if (time >= timeUntilDespawn)
		{
			Destroy(transform.gameObject);
		}
	}


}
