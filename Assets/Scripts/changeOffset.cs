using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class changeOffset : MonoBehaviour {

    public GameObject offsetText;

    private MasterTick masterTick;

    public float speed = 1;

    // Use this for initialization
    void Start()
    {
        masterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();
    }

    public void changePos(float pos)
    {
        var text = offsetText.GetComponent<TextMeshProUGUI>();

        text.SetText(pos.ToString() + " ms");

        masterTick.offset = (int)pos;
    }
}
