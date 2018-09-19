using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

public class ConfigurationLoader : MonoBehaviour {

    MasterTick MasterTick;

    void Start()
    {
        MasterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();

        config conf = new config();


    }

}
