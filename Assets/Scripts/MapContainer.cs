using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapContainer : MonoBehaviour {

    [SerializeField]
    public List<Item> Ticks;
}

[Serializable]
public class Item
{
    public int subBeats = 1;

    public List<List<ObjectItem>> ObjectItemArray = new List<List<ObjectItem>>();

}

[Serializable]
public class ObjectItem
{
    public enum ObjectType
    {
        None,
        BallBurst,
        PoleBurst,
        GiantBallBurst,
    }

    public ObjectType objectType;

    public int multipleSpawn = 1;
    public float SpawnSpeed = 20f;
    public bool spawnToCamera = false;
}