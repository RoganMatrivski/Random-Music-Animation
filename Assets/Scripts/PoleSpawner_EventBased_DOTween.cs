using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using DG.Tweening;

public class PoleSpawner_EventBased_DOTween : MonoBehaviour {
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

    GameObject WarningCylinder;
    GameObject Cylinder;

    void Start()
    {
        masterTickGameObject = GameObject.FindGameObjectWithTag("MasterTick");
        TickClass = masterTickGameObject.GetComponent<MasterTick>();

        TickClass.onTickEvent += onTickMethod;
        init(TickClass.timePerTick);

        //TickClass.onTickInitEvent += init;

        WarningCylinder = transform.GetChild(0).gameObject;
        Cylinder = transform.GetChild(1).gameObject;

        DOTween.Init(false, true, LogBehaviour.Default);
        DOTween.defaultEaseType = Ease.OutSine;
    }

    void Update()
    {
        //Animate Warning
        if (WarningCylinder != null)
            WarningCylinder.transform.GetChild(0).GetComponent<Renderer>().material.DOFloat(0.1f, "_obj_opacity", warningMsAmount).SetEase(Ease.Linear);


        if (tick > warningTickAmount)
        {
            Destroy(WarningCylinder);

            //Spawn Cylinder
            if (Cylinder != null)
                Cylinder.transform.DOScaleY(20, 1f);

            if (tick > warningTickAmount + despawnTickAmount - transitionTickAmount)
            {
                if (Cylinder != null)
                    Cylinder.transform.GetChild(0).GetComponent<Renderer>().material.DOFloat(0f, "_obj_opacity", transitionMsAmount).SetEase(Ease.Linear);
                //Transition to despawn
            }
        }

        if (tick > warningTickAmount + despawnTickAmount)
            Destroy(gameObject);
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
