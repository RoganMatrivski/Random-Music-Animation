using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

using UnityEngine.SceneManagement;

public class saveConfiguration : MonoBehaviour {
    private MasterTick masterTick;

    // Use this for initialization
    void Start()
    {
        masterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();
    }

    // Update is called once per frame
    void Update () {

	}

    public void saveConf()
    {
        config conf = new config();

        conf.offset = masterTick.offset;

        using (System.IO.StreamWriter file = System.IO.File.CreateText(Application.dataPath + "\\settings.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, conf);
        }

        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
    }
}