using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class MapLoader : MonoBehaviour {

	MasterTick MasterTick;

    Queue<Queue<IEnumerator>> SpawnListQueue = new Queue<Queue<IEnumerator>>(); //Contains list of item to spawn. Triggered by onBPMTick

    //public GameObject mapObjects;

    Item map;

    public LayerMask ignoredCollision;

    public TextAsset mapText;

    public float defaultSpeed = 10f;

    // Use this for initialization
    void Start()
    {
        //map = mapObjects.GetComponent<FileToMap>().temp;

        MasterTick = GameObject.FindGameObjectWithTag("MasterTick").GetComponent<MasterTick>();
        MasterTick.onTickEvent += runQueueTickBased;

        map = fileToMap(mapText);

        //for (int i = 0; i < MasterTick.Subdivide; i++) // To give a headstart
        //{
        //    Queue<IEnumerator> itemSpawnList = new Queue<IEnumerator>();

        //    itemSpawnList.Enqueue(NullPlaceholder());

        //    SpawnListQueue.Enqueue(itemSpawnList);
        //}

        //try
        //      {
        //          for (int i = 0; i < 10; i++)
        //          {
        //              Queue<IEnumerator> itemSpawnList = new Queue<IEnumerator>();

        //              for (int j = 0; j < 5; j++)
        //              {
        //                  itemSpawnList.Enqueue(SpawnBalls());
        //              }

        //              SpawnListQueue.Enqueue(itemSpawnList);
        //          }
        //      }

        //      catch (System.Exception ex)
        //      {
        //          Debug.Log(ex.ToString());
        //      }

        foreach (List<ObjectItem> items in map.ObjectItemArray)
        {
            Queue<IEnumerator> itemSpawnList = new Queue<IEnumerator>();

            foreach (ObjectItem itemToSpawn in items)
            {
                switch ((int)itemToSpawn.objectType)
                {
                    case 0:
                        {
                            itemSpawnList.Enqueue(NullPlaceholder());

                            break;
                        }

                    case 1:
                        {
                            itemSpawnList.Enqueue(SphereSpawn(itemToSpawn.multipleSpawn, itemToSpawn.SpawnSpeed, itemToSpawn.spawnToCamera));

                            break;
                        }

                    case 2:
                        {
                            itemSpawnList.Enqueue(PoleSpawn(itemToSpawn.multipleSpawn, itemToSpawn.SpawnSpeed, itemToSpawn.spawnToCamera));

                            break;
                        }

                    case 3:
                        {
                            itemSpawnList.Enqueue(GiantSphereSpawn(itemToSpawn.multipleSpawn, itemToSpawn.SpawnSpeed, itemToSpawn.spawnToCamera));

                            break;
                        }
                }
            }

            SpawnListQueue.Enqueue(itemSpawnList);
        }

        //runQueue();
    }

    //void ballL

    void runQueueTickBased()
    {
        if (SpawnListQueue.Count > 0)
        {
            Queue<IEnumerator> tempQueue = SpawnListQueue.Dequeue();

            while (tempQueue.Count > 0)
                UnityMainThreadDispatcher.Instance().Enqueue(tempQueue.Dequeue());
        }
        else
            Debug.Log("No item to Run!");

        Debug.Log("tap");
    }

	void runQueue()
	{
		while (SpawnListQueue.Count > 0)
		{
			Queue<IEnumerator> tempQueue = SpawnListQueue.Dequeue();

			while (tempQueue.Count > 0)
				StartCoroutine(tempQueue.Dequeue());
		}
	}

    public Transform PoleObj;
    public Transform SphereObj;
    public Transform GiantSphereObj;

    Vector3 randomRotation(bool collideWithObject)
    {
        //while (true)
        for (int i = 0; i< 50; i++)
        {
            Vector3 randRot = new Vector3(Random.Range(0, 360), Random.Range(0, 360), 1);

            if (collideWithObject)
                return randRot;

            RaycastHit hit = new RaycastHit();
            if (!Physics.Raycast(transform.position, randRot, ignoredCollision))
            {
                //Debug.DrawLine(transform.position, hit.point, Color.green);

                //Debug.Log(hit);

                return randRot;
            }

            Debug.DrawLine(transform.position, hit.point, Color.red); return randRot;

            //Debug.Log(hit);
        }
        //Debug.Log("can't find any object to hit, already passed the threshold number of loop.");
        return new Vector3();
    }

    Vector3 randomRotation()
    {
        return new Vector3(Random.Range(0, 360), Random.Range(0, 360), 1);
    }

    Vector3 randomRotationToCamera(int multiplier)
    {
        var dir = GameObject.FindGameObjectWithTag("MainCamera").transform.position - transform.position;
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, dir);

        Quaternion randRot = Quaternion.Euler(new Vector3(Random.Range(-1 * multiplier, 1 * multiplier), Random.Range(-1 * multiplier, 1 * multiplier), Random.Range(-1 * multiplier, 1 * multiplier)));

        Quaternion totalRot = rot * randRot;

        return totalRot.eulerAngles;
    }

    Vector3 randomRotationToCamera(bool collideWithObject, int multiplier)
    {
        var dir = GameObject.FindGameObjectWithTag("MainCamera").transform.position - transform.position;

        Quaternion rot = Quaternion.FromToRotation(Vector3.up, dir);

        while (true)
        {
            Vector3 randRot = new Vector3(Random.Range(0, 360), Random.Range(0, 360), 1);
            Quaternion totalRot = rot * Quaternion.Euler(randRot);

            if (collideWithObject)
                return totalRot.eulerAngles;

            if (!Physics.Raycast(transform.position, totalRot.eulerAngles, ignoredCollision))
                return totalRot.eulerAngles;
        }
    }

    public IEnumerator PoleSpawn(int multipleSpawn, float objectSpeed, bool launchToCamera)
    {
        int multiplier = 25;
        for (int i = 0; i < multipleSpawn; i++)
        {
            Transform obj = Instantiate(PoleObj, GameObject.FindGameObjectWithTag("SpawnedObject").transform);
            if (launchToCamera)
            {
                obj.Rotate(randomRotationToCamera(false, multiplier));
            }
            else
                obj.Rotate(randomRotation(false));
        }

        yield return null;
    }

    public IEnumerator SphereSpawn(int multipleSpawn, float objectSpeed, bool launchToCamera)
    {
        int multiplier = 25;

        for (int i = 0; i < multipleSpawn; i++)
        {
            float randomizedPercent = 0.8f;

            float randomSpeed = objectSpeed + (Random.Range(-1f, 1f) * (objectSpeed * randomizedPercent));

            Transform obj = Instantiate(SphereObj, GameObject.FindGameObjectWithTag("SpawnedObject").transform);

            if (launchToCamera)
            {
                obj.Rotate(randomRotationToCamera(false, multiplier));
            }
            else
                obj.Rotate(randomRotation(false)); ;

            obj.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * randomSpeed, ForceMode.VelocityChange);
        }

        yield return null;
    }

    public IEnumerator GiantSphereSpawn(int multipleSpawn, float objectSpeed, bool launchToCamera)
    {
		Debug.Log("Giant Tap");

        int multiplier = 25;

        for (int i = 0; i < multipleSpawn; i++)
        {
            float randomizedPercent = 0.8f;

            float randomSpeed = objectSpeed + (Random.Range(-1f, 1f) * (objectSpeed * randomizedPercent));

            Transform obj = Instantiate(GiantSphereObj, GameObject.FindGameObjectWithTag("SpawnedObject").transform);

            if (launchToCamera)
            {
                obj.Rotate(randomRotationToCamera(false, multiplier));
            }
            else
                obj.Rotate(randomRotation(false));

            obj.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * randomSpeed, ForceMode.VelocityChange);
        }

        yield return null;
    }

    public IEnumerator NullPlaceholder()
    {
        yield return null;
    }

    Item fileToMap(TextAsset map)
    {
        Item temp = new Item();

        int counter = 0;

        List<string> lists = map.text.Split(System.Environment.NewLine.ToCharArray()).ToList();

        //for (int i = 0; i < MasterTick.Subdivide*2; i++)
        //{
        //    //List<ObjectItem> tempContainer = new List<ObjectItem>(); //Items to spawn simultaneously.

        //    //ObjectItem tempObj = new ObjectItem();
        //    //tempObj.objectType = ObjectItem.ObjectType.None;

        //    //tempContainer.Add(new ObjectItem() { objectType = ObjectItem.ObjectType.None });

        //    temp.ObjectItemArray.Add(new List<ObjectItem>() { new ObjectItem() { objectType = ObjectItem.ObjectType.None } });
        //}

        foreach (string item in lists) // Root
        {
            counter++;
            if (!(
                item.Contains("#") || string.IsNullOrWhiteSpace(item)
                ))
            {
                List<ObjectItem> tempContainer = new List<ObjectItem>(); //Items to spawn simultaneously.

                List<string> objectStr = item.Split(' ').ToList(); // Make sure to remove the whitespace after.
                foreach (string objStr in objectStr) //Level 1
                {
                    if (string.IsNullOrWhiteSpace(objStr))
                        continue;

                    try
                    {
                        if (System.Convert.ToInt32(objStr) == 0)
                        {
                            //ObjectItem tempObj = new ObjectItem() { objectType = ObjectItem.ObjectType.None };

                            tempContainer.Add(new ObjectItem() { objectType = ObjectItem.ObjectType.None });

                            continue;
                        }
                    }
                    catch (System.FormatException)
                    {
                        //dostuff
                    }

                    List<int> obj = new List<int>();
                    foreach (string str in objStr.Split(','))
                        obj.Add(System.Convert.ToInt32(str));

                    try
                    {
                        ObjectItem tempObj = new ObjectItem();
                        tempObj.objectType = (ObjectItem.ObjectType)obj[0];

                        if (obj[1] >= 0)
                            tempObj.multipleSpawn = obj[1];

                        if (obj[2] >= 0)
                            tempObj.SpawnSpeed = obj[2];
                        else
                            tempObj.SpawnSpeed = defaultSpeed;

                        if (obj[3] == 1)
                            tempObj.spawnToCamera = true;

                        if (tempObj.objectType == ObjectItem.ObjectType.PoleBurst)
                        {
                            //int tmp1 = temp.ObjectItemArray.Count;
                            //int tmp2 = PoleObj.GetComponent<PoleSpawner_EventBased_DOTween>().warningTickAmount;
                            //int tmp3 = temp.ObjectItemArray.Count - PoleObj.GetComponent<PoleSpawner_EventBased_DOTween>().warningTickAmount;

                            temp.ObjectItemArray[temp.ObjectItemArray.Count - PoleObj.GetComponent<PoleSpawner_EventBased_DOTween>().warningTickAmount - 1].Add(tempObj);
                            tempContainer.Add(new ObjectItem() { objectType = ObjectItem.ObjectType.None });

                            continue;
                        }

                        tempContainer.Add(tempObj);
                    }
                    catch (System.InvalidCastException)
                    {
                        throw new System.Exception(string.Format("Failed to parse item on line number {0}.", counter));
                    }
                }

                temp.ObjectItemArray.Add(tempContainer);
            }
        }

        return temp;
    }
}
