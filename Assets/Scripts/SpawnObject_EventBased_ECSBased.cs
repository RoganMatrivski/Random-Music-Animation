using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Entities;

public class SpawnObject_EventBased_ECSBased : MonoBehaviour {
    [Header("Spawn Settings")]
    [Tooltip("Enable Spawning")]
    [SerializeField]
    public bool enableSpawn = true;

    [SerializeField]
    Transform objGameObject;

    public Vector2Int xAngle = new Vector2Int(0, 360);
    public Vector2Int yAngle = new Vector2Int(0, 360);

    [HideInInspector]
    public Vector2Int zAngle = new Vector2Int(0, 360);

    [Header("Miscellaneous")]
    [Tooltip("Spawn the object multiple time")]
    [Range(1, 64)]
    [SerializeField]
    public int multipleSpawn = 1;

    MasterTick MasterTick;

    // Use this for initialization
    void Start()
    {
        MasterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();
        MasterTick.onTickEvent += spawnObject;
    }

    void spawnObject()
    {
        //Vector3 randRotation = new Vector3(Random.Range(xAngle.x, xAngle.y), Random.Range(yAngle.x, yAngle.y), Random.Range(zAngle.x, zAngle.y));
        Vector3 randRotation = new Vector3(Random.Range(xAngle.x, xAngle.y), Random.Range(yAngle.x, yAngle.y), 1); //Vector3([X], [Y], [Z])

        Transform obj = Instantiate(objGameObject, transform.position, Quaternion.Euler(randRotation));
    }
}
