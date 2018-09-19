using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using DG.Tweening;

public class ChangeText : MonoBehaviour {

    public string[] tips;

    public float messageAppearTime;

    float timePass;

    Queue<string> message = new Queue<string>();

    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        for (int i = 0; i < tips.Length; i++)
        {
            message.Enqueue(tips[i]);
        }
    }

    // Update is called once per frame
    void Update () {
        if (Time.timeSinceLevelLoad > timePass && message.Count > 1)
        {
            timePass += messageAppearTime;

            StartCoroutine(changeMessage());
        }
	}

    IEnumerator changeMessage()
    {
        DOTween.ToAlpha(() => text.color, x => text.color = x, 0f, 1f);

        text.text = message.Dequeue();

        DOTween.ToAlpha(() => text.color, x => text.color = x, 1f, 1f);

        yield return null;
    }
}
