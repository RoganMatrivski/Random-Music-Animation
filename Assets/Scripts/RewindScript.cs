using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindScript : MonoBehaviour {

    public bool isRewinding = false;

    LinkedList<PointsInTime> points = new LinkedList<PointsInTime>();

    Rigidbody rb;

    public Vector3 velocityBefore = new Vector3();
    public Vector3 velocityAfter = new Vector3();

    [SerializeField]
    int count = 0;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate () {

        //if (points.Count > 6000)
        //    points.RemoveFirst();

        count = points.Count;

        if (GameObject.FindGameObjectWithTag("RewindControl").GetComponent<rewindControl>().rewind)
            isRewinding = true;
        else
            isRewinding = false;

        if (isRewinding)
            startRewind();
        else
        {
            stopRewindAndRecord();
        }
	}

    void record()
    {

    }

    void startRewind()
    {
        //rb.isKinematic = true;
        PointsInTime point = new PointsInTime();
        if (points.Count > 1)
            point = points.Last.Value;
        else
            point = points.First.Value;

        transform.position = point.pos;
        transform.rotation = point.rot;

        velocityBefore = rb.velocity;

        //rb.velocity = point.velocity;

        //rb.AddForce(point.velocity, ForceMode.VelocityChange);

        //velocityAfter = rb.velocity;

        Vector3 vel = rb.velocity;

        rb.velocity = vel;

        velocityAfter = rb.velocity;

        if (points.Count > 1)
            points.RemoveLast();
        else
            Debug.Log("Somethign");
    }

    void stopRewindAndRecord()
    {
        //rb.isKinematic = false;

        points.AddLast(new PointsInTime(transform.position, transform.rotation, rb.velocity));
    }
}

struct PointsInTime
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 velocity;

    public PointsInTime (Vector3 _pos, Quaternion _rot, Vector3 _velocity)
    {
        pos = _pos;
        rot = _rot;
        velocity = _velocity;
    }
}