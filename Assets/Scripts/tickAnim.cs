using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class tickAnim : MonoBehaviour {

    private MasterTick masterTick;

    public float speed = 1;

    // Use this for initialization
    void Start() {
        masterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();

        masterTick.onTickEvent += tick;

        DOTween.Init();
    }

    private void Update()
    {
        //DOTween.To(() => transform.localScale, x => transform.localScale = x, new Vector3(0.1f, 0.1f, 0.1f), 10);

        float decreaseVal = 0.1f* speed;

        if (!(transform.localScale.x < 0))
            transform.localScale -= new Vector3(decreaseVal, decreaseVal, decreaseVal);
    }

    void tick()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(function());
    }

    IEnumerator function()
    {
        Debug.Log("tick");
        transform.localScale = new Vector3(1, 1, 1);
        //transform.GetComponent<>

        //Vector3 scaler = new Vector3(0.1f, 0.1f, 0.1f);

        //transform.localScale -= scaler;

        yield return null;
    }
}
