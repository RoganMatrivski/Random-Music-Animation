using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class DiamondTopAnim : MonoBehaviour {

    public bool activate = false;

    MasterTick MasterTick;

    [SerializeField]
    float glowAmount = 10f;

    // Use this for initialization
    void Start()
    {
        MasterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();

        DOTween.Init();
    }

    // Update is called once per frame
    void Update () {
        if (activate)
        {
            Color temp = transform.GetComponent<Renderer>().material.color;
            transform.GetComponent<Renderer>().material.DOColor(new Color(0f, 0.8221f, 1) * glowAmount, (float)MasterTick.timePerBeat)
                .onComplete += () =>
                {
                    transform.GetComponent<Renderer>().material.DOColor(temp * glowAmount, (float)MasterTick.timePerBeat);
                };
        }
	}
}
