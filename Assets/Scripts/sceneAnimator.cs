using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using DG.Tweening;

using Cinemachine;

public class sceneAnimator : MonoBehaviour {

    MasterTick MasterTick;

    public Image blackScreen;
    public Image blackScreen2;
    public Image blackScreen3;
    public Image whiteScreen;
    public Image whiteScreen2;
    public Image whiteScreen3;

    public CinemachineVirtualCamera virtualCamera;

    public GameObject spawnedObject;

    public GameObject[] crystals = new GameObject[3];
    public GameObject diamonds;

    int tick = 32;

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
    void onTick()
    {
        Tween tempCont = null;

        Color blackOpaque = new Color(0, 0, 0, 1);
        Color blackTransparent = new Color(0, 0, 0, 0);

        Color whiteOpaque = new Color(1, 1, 1, 1);
        Color whiteTransparent = new Color(1, 1, 1, 1);

        var dispatch = UnityMainThreadDispatcher.Instance();

        tick = MasterTick.tick;
        switch (tick)
        {
            case 64:
                {
                    //Debug.Log("tick at 64");
                    dispatch.Enqueue(() =>
                    {
                        Debug.Log("Faded");

                        blackScreen2.CrossFadeAlpha(0, (float)MasterTick.timePerBeat * 8, false);

                        //virtualCamera.GetCinemachineComponent<NoiseSettings>().PositionNoise.
                    });

                    break;
                }

            case 880:
                {
                    //Debug.Log("tick at 928");
                    dispatch.Enqueue(() =>
                    {
                        GameObject.FindGameObjectWithTag("RewindControl").GetComponent<rewindControl>().rewind = true;

                        //Debug.Log("triggered at 928");
                    });
                    break;
                }

            case 888:
                {
                    //Debug.Log("tick at 936");
                    dispatch.Enqueue(() =>
                    {
                        GameObject.FindGameObjectWithTag("RewindControl").GetComponent<rewindControl>().rewind = false;

                        //Debug.Log("triggered at 936");
                    });
                    break;
                }

            case 1008:
                {
                    //Debug.Log("tick at 928");
                    dispatch.Enqueue(() =>
                    {
                        GameObject.FindGameObjectWithTag("RewindControl").GetComponent<rewindControl>().rewind = true;

                        //Debug.Log("triggered at 928");
                    });
                    break;
                }

            case 1016:
                {
                    //Debug.Log("tick at 936");
                    dispatch.Enqueue(() =>
                    {
                        GameObject.FindGameObjectWithTag("RewindControl").GetComponent<rewindControl>().rewind = false;

                        //Debug.Log("triggered at 936");
                    });
                    break;
                }

            case 1088:
                {
                    dispatch.Enqueue(() =>
                    {
                        blackScreen.color = blackOpaque;

                        //while (spawnedObject.transform.childCount > 1)
                        //{
                        //    Destroy(spawnedObject.transform.GetChild(0));
                        //}

                        for (int i = 0; i < spawnedObject.transform.childCount; i++)
                        {
                            if (i > 100)
                                break;

                            Destroy(spawnedObject.transform.GetChild(i).gameObject);
                            Debug.Log("Deleted Child");
                        }

                        blackScreen.CrossFadeAlpha(0, (float)MasterTick.timePerBeat * 8, false);

                        diamonds.SetActive(false);
                        crystals[0].SetActive(true);
                    });
                    break;
                }

            case 1344: //Glow Crystal
                {
                    dispatch.Enqueue(() =>
                    {
                        crystals[0].GetComponent<Renderer>().material.DOFloat(5, "_obj_glow", (float)MasterTick.timePerBeat * 7);
                    });
                    break;
                }

            case 1576:
                {
                    dispatch.Enqueue(() =>
                    {
                        Debug.Log(whiteScreen.color);
                        whiteScreen.color = whiteOpaque;
                        Debug.Log(whiteScreen.color);
                        whiteScreen.CrossFadeAlpha(0, (float)MasterTick.timePerBeat / 2, false);
                        //DOTween.To(() => whiteScreen.color, x => whiteScreen.color = x, whiteTransparent, (float)MasterTick.timePerBeat / 2);
                        Debug.Log(whiteScreen.color);

                        crystals[0].SetActive(false);
                        crystals[1].SetActive(true);
                        Debug.Log("Triggered 1576, triggered at " + tick);
                    });
                    break;
                }

            case 1584:
                {
                    dispatch.Enqueue(() =>
                    {
                        whiteScreen2.color = whiteOpaque;
                        whiteScreen2.CrossFadeAlpha(0, (float)MasterTick.timePerBeat/2, false);
                        //DOTween.To(() => whiteScreen.color, x => whiteScreen.color = x, whiteTransparent, (float)MasterTick.timePerBeat / 2);

                        crystals[1].SetActive(false);
                        crystals[2].SetActive(true);
                        Debug.Log("Triggered 1584, triggered at " + tick);
                    });
                    break;
                }

            case 1592:
                {
                    dispatch.Enqueue(() =>
                    {
                        whiteScreen.CrossFadeAlpha(1, (float)MasterTick.timePerBeat, false);
                    });
                    break;
                }

            case 1600:
                {
                    dispatch.Enqueue(() =>
                    {
                        whiteScreen.CrossFadeAlpha(0, (float)MasterTick.timePerBeat*2, false);
                        blackScreen.color = blackTransparent;

                        crystals[2].SetActive(false);
                        diamonds.SetActive(true);
                    });
                    break;
                }

            case 1856: //Fade from white
                {
                    dispatch.Enqueue(() =>
                    {
                        whiteScreen3.color = whiteOpaque;
                        whiteScreen3.CrossFadeAlpha(0, (float)MasterTick.timePerBeat * 4, false);
                    });
                    break;
                }

            case 2304: //Fade to black
                {
                    dispatch.Enqueue(() =>
                    {
                        blackScreen3.CrossFadeAlpha(1, (float)MasterTick.timePerBeat * 4, false);
                    });
                    break;
                }
        }
    }
}
