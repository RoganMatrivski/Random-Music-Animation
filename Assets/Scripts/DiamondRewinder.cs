using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondRewinder : MonoBehaviour {

    public bool isRewinding = false;

    private LinkedList<rotation> rotations = new LinkedList<rotation>();

	// Update is called once per frame
	void FixedUpdate () {
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

    void startRewind()
    {
        gameObject.GetComponent<SpinnerObject>().enableRotation = false;

        rotation rotation = new rotation();

        if (rotations.Count > 1)
            rotation = rotations.Last.Value;
        else
            rotation = rotations.First.Value;

        transform.rotation = rotation.rot;

        if (rotations.Count > 1)
            rotations.RemoveLast();
        else
            Debug.Log("Somethign");
    }

    void stopRewindAndRecord()
    {
        gameObject.GetComponent<SpinnerObject>().enableRotation = true;

        rotations.AddLast(new rotation(transform.rotation));
    }
}

struct rotation
{
    public Quaternion rot;

    public rotation(Quaternion _rot)
    {
        rot = _rot;
    }
}
