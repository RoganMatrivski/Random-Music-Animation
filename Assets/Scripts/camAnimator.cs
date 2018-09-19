using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camAnimator : MonoBehaviour {

    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<AudioSource>();
        animator.enabled = false;
    }

    void Update()
    {
        // Change time for currently played state
        animator.PlayInFixedTime(0, 0, audioSource.time);
        animator.Update(0.0f);
    }
}
