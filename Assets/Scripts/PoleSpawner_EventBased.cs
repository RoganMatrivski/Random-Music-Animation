using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoleSpawner_EventBased : MonoBehaviour {

    public int warningTickAmount = 4;
    public int despawnTickAmount = 4;
    public int transitionTickAmount = 1;

    float warningMsAmount;
    float transitionMsAmount;

    public GameObject masterTickGameObject;
    MasterTick TickClass;

    int tick;

    float time;

    public float millisecondsPerTick;

    float opacityFloat;
    float transitionFloat;

    GameObject WarningCylinder;
    GameObject Cylinder;

    void Start()
    {
        TickClass = masterTickGameObject.GetComponent<MasterTick>();

        TickClass.onTickEvent += onTickMethod;
        //TickClass.onTickInitEvent += init;

        WarningCylinder = transform.GetChild(0).gameObject;
        Cylinder = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        time += Time.deltaTime;

        opacityFloat = Mathf.Clamp01(time / warningMsAmount);
        transitionFloat = Mathf.Clamp01((time - warningMsAmount - ((despawnTickAmount - transitionTickAmount) * millisecondsPerTick)) / transitionMsAmount);

        if (tick > warningTickAmount)
        {
            //Spawn Cylinder
            WarningCylinder.transform.GetChild(0).GetComponent<Renderer>().material.SetFloat("_obj_opacity", Mathf.Lerp(0f, 0.5f, opacityFloat));
        }

        if (tick > transitionMsAmount)
        {
            Destroy(WarningCylinder);

            //Transition to despawn
        }
    }

    public void init(double msPerTick)
    {
        warningMsAmount = (float)msPerTick * warningTickAmount;
        transitionMsAmount = (float)msPerTick * transitionTickAmount;
        millisecondsPerTick = (float)msPerTick;
    }

    public void onTickMethod()
    {
        tick++;
    }

    void OnDisable()
    {
        TickClass.onTickEvent -= onTickMethod;
        //TickClass.onTickInitEvent -= init;
    }
}
