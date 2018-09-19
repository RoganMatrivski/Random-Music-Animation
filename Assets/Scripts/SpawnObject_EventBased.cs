using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Mathematics;
using Unity.Entities;

public class SpawnObject_EventBased : MonoBehaviour {
	[Header("BPM Settings")]
	[Tooltip("Sub-Beats per Beat")]
	[Range(1, 4)]
	[SerializeField]
	public int localSubdivision = 1;

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

	int tick;

    public GameObject Player;

    public float multiplier = 5;

    //public Vector3 asdasd;

    public bool launchToCamera;

    // Use this for initialization
    void Start () {
		MasterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();
		MasterTick.onTickEvent += onTick;
	}

	void onTick ()
	{
		//Debug.Log((int)System.Math.Pow(2, 3 - localSubdivision));
		int localSubdivisionPowered = (int)System.Math.Pow(2, 4 - localSubdivision);

		tick++;

		if (tick % localSubdivisionPowered == 0)
            //spawnObject();
            UnityMainThreadDispatcher.Instance().Enqueue(spawnObject());
    }

	//void spawnObject()
	//{
	//	for (int i = 0; i < multipleSpawn; i++)
	//	{
	//		Vector3 randRotation = new Vector3(Random.Range(xAngle.x, xAngle.y), Random.Range(yAngle.x, yAngle.y), 1); //Vector3([X], [Y], [Z])

	//		Transform obj = Instantiate(objGameObject, transform.position, Quaternion.Euler(randRotation));
	//	}
	//}

    IEnumerator spawnObject()
    {
        for (int i = 0; i < multipleSpawn; i++)
        {
            Vector3 randRotation = new Vector3(Random.Range(xAngle.x, xAngle.y), Random.Range(yAngle.x, yAngle.y), 1); //Vector3([X], [Y], [Z])

            //Vector3 randRotation = new Vector3(Random.Range(xAngle.x, xAngle.y), Random.Range(yAngle.x, yAngle.y), 1); //Vector3([X], [Y], [Z])
            Vector3 randRot = new Vector3(Random.Range(-1 * multiplier, 1 * multiplier), Random.Range(-1 * multiplier, 1 * multiplier), Random.Range(-1 * multiplier, 1 * multiplier));
            //Debug.Log(randRot);

            Transform obj = Instantiate(objGameObject);
            if (launchToCamera)
            {
                var dir = Player.transform.position - transform.position;
                obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
                obj.Rotate(randRot);
            }
            else
                obj.Rotate(randRotation);
        }

        yield return null;
    }
}
