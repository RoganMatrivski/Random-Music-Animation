using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rewindControl : MonoBehaviour {
    public bool rewind = false;

    // Update is called once per frame
	void Update () {
        KeyCode keyToPress = KeyCode.R;

        if (Input.GetKeyDown(keyToPress))
            rewind = true;
        if (Input.GetKeyUp(keyToPress))
            rewind = false;
	}

    public void toggleRewind()
    {
        if (rewind)
            rewind = false;
        else
            rewind = true;
    }
}
