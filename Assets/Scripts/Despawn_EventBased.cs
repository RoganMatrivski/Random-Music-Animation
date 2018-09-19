using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class Despawn_EventBased : MonoBehaviour {
	public int despawnTickAmount = 4;
	public int transitionTickAmount = 1;

	float transitionMsAmount;

	public GameObject masterTickGameObject;
	MasterTick TickClass;

	int tick;

	public float millisecondsPerTick;

	// Use this for initialization
	void Start () {
		masterTickGameObject = GameObject.FindGameObjectWithTag("MasterTick");
		TickClass = masterTickGameObject.GetComponent<MasterTick>();

		TickClass.onTickEvent += onTickMethod;
		init(TickClass.timePerTick);

		DOTween.Init(false, true, LogBehaviour.Default);
		DOTween.defaultEaseType = Ease.OutSine;
	}

	// Update is called once per frame
	void Update () {
		if (tick > despawnTickAmount - transitionTickAmount)
		{
			GetComponent<Renderer>().material.DOFloat(0, "_obj_opacity", transitionMsAmount).SetEase(Ease.InQuad);

			if (tick > despawnTickAmount)
			{
				Destroy(gameObject);
			}
		}
	}

	public void init(double msPerTick)
	{
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
