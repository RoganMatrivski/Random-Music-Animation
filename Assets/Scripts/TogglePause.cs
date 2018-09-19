using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePause : MonoBehaviour {

    private bool paused;

    public GameObject pauseScreen;

    private float tempTimeScale;

    MasterTick MasterTick;

    void Start()
    {
        MasterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (!paused)
            {
                MasterTick.pauseAudio();

                pauseScreen.SetActive(true);

                paused = true;
            }
            else
            {
                MasterTick.playAudio();

                paused = false;
            }
    }
}
