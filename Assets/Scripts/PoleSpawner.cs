using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleSpawner : MonoBehaviour {

    public MasterTick masterTickGameObject;

    public int warningTickAmount = 4;


    double bpm = 140.0F;
    int subdivide = 1;
    int offset = 0;

    double nextTick = 0.0F; // The next tick in dspTime
    double sampleRate = 0.0F;
    bool ticked = false;
    double dspTime = 0;

    void Start()
    {
        bpm = masterTickGameObject.bpm;
        subdivide = masterTickGameObject.Subdivide;
        offset = masterTickGameObject.offset;

        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick + (60.0f / (bpm / (float)subdivide));
    }
    void LateUpdate()
    {
        if (!ticked && nextTick >= AudioSettings.dspTime)
        {
            ticked = true;
            //spawnObject();
        }
    }

    void FixedUpdate()
    {
        double timePerTick = 60.0f / bpm / (float)subdivide;
        dspTime = AudioSettings.dspTime + ((float)offset / 1000);
        while (dspTime >= nextTick)
        {
            ticked = false;
            nextTick += timePerTick;
        }
    }
}
