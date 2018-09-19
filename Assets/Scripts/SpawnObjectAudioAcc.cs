using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectAudioAcc : MonoBehaviour {

    [Header("Spawn Settings")]
    [Tooltip("Enable Spawning")]
    [SerializeField]
    public bool enableSpawn = true;

    //[Space]

    [Tooltip("Object Speed")]
    [Range(1, 64)]
    [SerializeField]
    public float speed = 16f;

    [Tooltip("Randomize Speed")]
    [Range(0, 100)]
    [SerializeField]
    public float randomizeSpeed = 0f;

    [SerializeField]
    Transform spheres;

    [Header("BPM Settings")]
    [Tooltip("Beats Per Minutes")]
    [Range(30, 240)]
    [SerializeField]
    public double bpm = 140.0F;

    //[Space]
    [Tooltip("Sub-Beats per Beat")]
    [Range(1, 16)]
    [SerializeField]
    public int subdivide = 1;

    [Tooltip("Offsets")]
    [Range(-256, 256)]
    [SerializeField]
    //[HideInInspector]
    public int offset = 0;

    public Vector2Int xAngle = new Vector2Int(0, 360);
    public Vector2Int yAngle = new Vector2Int(0, 360);

    [HideInInspector]
    public Vector2Int zAngle = new Vector2Int(0, 360);

    [Header("Miscellaneous")]
    [Tooltip("Spawn the object multiple time")]
    [Range(1, 64)]
    [SerializeField]
    public int multipleSpawn = 1;

    double nextTick = 0.0F; // The next tick in dspTime
    double sampleRate = 0.0F;
    bool ticked = false;
    float randomizedPercent;
    float randomSpeed;

    double dspTime = 0;

    void Start()
    {
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

            if (enableSpawn)
                for (int i = 0; i < multipleSpawn; i++)
                    spawnObject();
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

    void spawnObject()
    {
        //Debug.Log("Tap!");

        randomizedPercent = randomizeSpeed / 100f;

        randomSpeed = speed + (Random.Range(-1f, 1f) * (speed * randomizedPercent));

        //Vector3 randRotation = new Vector3(Random.Range(xAngle.x, xAngle.y), Random.Range(yAngle.x, yAngle.y), Random.Range(zAngle.x, zAngle.y));
        Vector3 randRotation = new Vector3(Random.Range(xAngle.x, xAngle.y), Random.Range(yAngle.x, yAngle.y), 1); //Vector3([X], [Y], [Z])

        Transform sphere = Instantiate(spheres, transform.position, Quaternion.Euler(randRotation));

        sphere.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * randomSpeed, ForceMode.VelocityChange);
    }
}
