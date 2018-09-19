using UnityEngine;
using System.Collections;

using UnityEngine.Serialization;

using System;

// The code example shows how to implement a metronome that procedurally generates the click sounds via the OnAudioFilterRead callback.
// While the game is paused or the suspended, this time will not be updated and sounds playing will be paused. Therefore developers of music scheduling routines do not have to do any rescheduling after the app is unpaused

[RequireComponent(typeof(AudioSource))]
public class MasterTick : MonoBehaviour
{
    public event Action onTickEvent;

    [Header("BPM Settings")]
    [Tooltip("Beats Per Minutes")]
    [Range(30, 240)]
    [SerializeField]
    public double bpm = 140.0F;

    //[Space]
    [Tooltip("Sub-Beats per Beat")]
    [Range(1, 16)]
    [SerializeField]
    public int Subdivide = 1;

    [Tooltip("Offsets")]
    [Range(-1000, 1000)]
    [SerializeField]
    //[HideInInspector]
    public int offset = 0;

    //public double bpm = 140.0F;
    //public float gain = 0.5F;
    //public int signatureHi = 4;
    //public int signatureLo = 4;
    private double nextTick = 0.0F;
    private double sampleRate = 0.0F;
    private bool running = false;

    [HideInInspector]
    public double timePerTick;

    [HideInInspector]
    public double timePerBeat;

    [SerializeField]
    AudioSource song;

    [SerializeField]
    float audioDelay = 4; //to give chance to totally load things.

    [SerializeField]
    [Range(0, 5)]
    public float audioRate = 1;

    //[HideInInspector]
    public int tick;

    [SerializeField]
    bool songPlayed = false;

    float time;

    public void pauseAudio()
    {
        song.Pause();
    }

    public void playAudio()
    {
        song.UnPause();
    }

    void Start()
    {
        tick = 32;

        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        running = true;

        song = GetComponent<AudioSource>();

        onTickEvent += onTick;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time > audioDelay && !songPlayed)
        {
            song.Play();
            songPlayed = true;

            tick = 32;
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            song.pitch = audioRate;
            Time.timeScale = audioRate;
        });

        timePerBeat = 60.0f / bpm;

        timePerTick = 60.0f / bpm / (float)Subdivide;

        double samplesPerTick = sampleRate * 60.0F / bpm * (1.0f / (double)Subdivide) / audioRate; // change 1.0F to
        double sample = (AudioSettings.dspTime + ((float)offset / 1000)) * sampleRate;
        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            while (sample + n >= nextTick)
            {
                nextTick += samplesPerTick;

                tick++;

                //if (onTickEvent != null)
                //    onTickEvent();

                onTickEvent?.Invoke(); //the same as the above
            }
            n++;
        }
    }

    void onTick()
    {
        //Debug.Log("Tick");
    }
}