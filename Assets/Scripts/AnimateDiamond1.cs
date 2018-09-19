using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class AnimateDiamond1 : MonoBehaviour {

    MasterTick MasterTick;

    int tick = 32;

    [SerializeField]
    AnimationCurve easing;

    [SerializeField]
    AnimationCurve rotEasing;

    //public enum Rotation
    //{
    //    Clockwise,
    //    Counterclockwise
    //}

    [SerializeField]
    AnimationCurve rotease;

    public bool counterclockwise = false;

    bool elapsed;

    // Use this for initialization
    void Start()
    {
        MasterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();
        MasterTick.onTickEvent += onTick;

        DOTween.Init();
    }

	// Update is called once per frame
	void onTick () {

        Tween tweenCont = null;
        var dispatch = UnityMainThreadDispatcher.Instance();

        tick = MasterTick.tick;
        switch (tick)
        {
            case 256:
                {
                    dispatch.Enqueue(() =>
                    {
                        transform.DOMoveY(transform.position.y * 2, (float)MasterTick.timePerBeat * 4).SetEase(easing);

                        tweenCont = DOTween.To(() => GetComponent<SpinnerObject>().spinningSpeed, x => GetComponent<SpinnerObject>().spinningSpeed = x, 100f, (float)MasterTick.timePerBeat * 29).SetEase(rotEasing);
                    });
                    elapsed = true;
                    break;
                }

            case 544:
                {
                    dispatch.Enqueue(() =>
                    {
                        tweenCont.Kill();
                        GetComponent<SpinnerObject>().spinningSpeed = 1;
                        tweenCont = DOTween.To(() => GetComponent<SpinnerObject>().spinningSpeed, x => GetComponent<SpinnerObject>().spinningSpeed = x, 10f, (float)MasterTick.timePerBeat * 4).SetEase(rotease);
                    });
                    break;
                }

            case 576:
                {
                    tweenCont.Kill();
                    dispatch.Enqueue(() => { GetComponent<SpinnerObject>().spinningSpeed = 10; });
                    break;
                }
        }
	}
}
