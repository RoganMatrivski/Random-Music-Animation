using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour {

	[SerializeField]
	AudioSource song;

	// Use this for initialization
	void Start () {
		song = GetComponent<AudioSource>();
		song.PlayScheduled(0);
	}

	// Update is called once per frame
	void Update () {

	}
}
