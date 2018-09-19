using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MasterTick_OLD : MonoBehaviour {

    //public delegate void onTickEvent(double msPerTick);
    //public static event onTickEvent onTick = delegate { };
    public event Action onTickEvent;
    public event Action<double> onTickInitEvent;

    [Header("BPM Settings")]
    [Tooltip("Beats Per Minutes")]
    [Range(30, 240)]
    [SerializeField]
    public double bpm = 140.0F;

    //[Space]
    [Tooltip("Sub-Beats per Beat")]
    [Range(1, 4)]
    [SerializeField]
    public int Subdivide = 1;

    [Tooltip("Offsets")]
    [Range(-256, 256)]
    [SerializeField]
    //[HideInInspector]
    public int offset = 0;

    double nextTick = 0.0F; // The next tick in dspTime
    double sampleRate = 0.0F;
    bool ticked = false;

    double msPerTick;
    public double timePerTick;

    double dspTime = 0;

    [SerializeField]
    AudioSource song;

    void Start()
    {
        Debug.Log(Time.fixedDeltaTime);
        Time.fixedDeltaTime = Time.fixedDeltaTime / 4;


        timePerTick = 60.0f / bpm / (float)Subdivide;
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick + timePerTick;

        song = GetComponent<AudioSource>();
        song.PlayScheduled(0);

        //if (onTickInitEvent != null)
        //    onTickInitEvent(timePerTick);

        //onTickInitEvent += addMessage;
    }

    void LateUpdate()
    {
        if (!ticked && nextTick >= AudioSettings.dspTime)
        {
            ticked = true;

            //Do stuff here

            if (onTickInitEvent != null)
                onTickInitEvent(timePerTick);

            if (onTickEvent != null)
            {
                onTickEvent.Invoke();
            }

        }
    }

    void FixedUpdate()
    {
        timePerTick = 60.0f / bpm / (float)Subdivide;

        dspTime = AudioSettings.dspTime + ((float)offset / 1000);
        while (dspTime >= nextTick)
        {
            ticked = false;
            nextTick += timePerTick;
        }
    }
}
