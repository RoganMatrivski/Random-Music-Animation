using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class FileToMap : MonoBehaviour {

    public TextAsset map;

    [SerializeField]
    public Item temp = new Item();

	// Use this for initialization
	void Start () {
        int counter = 0;

        List<string> lists = map.text.Split('\n').ToList();

        foreach (string item in lists) // Root
        {
            counter++;
            if (!(
                item.Contains("#") || string.IsNullOrWhiteSpace(item)
                ))
            {
                List<ObjectItem> tempContainer = new List<ObjectItem>(); //Items to spawn simultaneously.

                List<string> objectStr = item.Split(' ').ToList();
                foreach (string objStr in objectStr) //Level 1
                {
                    try
                    {
                        if (System.Convert.ToInt32(objStr) == 0)
                        {
                            ObjectItem tempObj = new ObjectItem();
                            tempObj.objectType = ObjectItem.ObjectType.None;

                            tempContainer.Add(tempObj);

                            continue;
                        }
                    }
                    catch(System.FormatException)
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

                        if (obj[3] == 1)
                            tempObj.spawnToCamera = true;

                        tempContainer.Add(tempObj);
                    }
                    catch (System.InvalidCastException)
                    {
                        throw new System.Exception(string.Format("Failed to parse item on line number {0}.", counter));
                    }
                }

                temp.ObjectItemArray.Add(tempContainer);

                //switch (items[0])
                //{
                //    case 0:
                //        {
                //            ObjectItem tempObj = new ObjectItem();
                //            tempObj.objectType = ObjectItem.ObjectType.None;

                //            temp.ObjectItemArray.Add(tempObj);

                //            break;
                //        }

                //    case 1:
                //        {
                //            ObjectItem tempObj = new ObjectItem();
                //            tempObj.objectType = ObjectItem.ObjectType.BallBurst;

                //            if (items[1] >= 0)
                //                tempObj.multipleSpawn = items[1];

                //            if (items[2] >= 0)
                //                tempObj.SpawnSpeed = items[2];

                //            if (items[3] == 1)
                //                tempObj.spawnToCamera = true;

                //            temp.ObjectItemArray.Add(tempObj);
                //            break;
                //        }

                //    case 2:
                //        {
                //            ObjectItem tempObj = new ObjectItem();
                //            tempObj.objectType = ObjectItem.ObjectType.PoleBurst;

                //            if (items[1] >= 0)
                //                tempObj.multipleSpawn = items[1];

                //            if (items[2] >= 0)
                //                tempObj.SpawnSpeed = items[2];

                //            if (items[3] == 1)
                //                tempObj.spawnToCamera = true;

                //            temp.ObjectItemArray.Add(tempObj);
                //            break;
                //        }

                //    case 3:
                //        {
                //            ObjectItem tempObj = new ObjectItem();
                //            tempObj.objectType = ObjectItem.ObjectType.GiantBallBurst;

                //            if (items[1] >= 0)
                //                tempObj.multipleSpawn = items[1];

                //            if (items[2] >= 0)
                //                tempObj.SpawnSpeed = items[2];

                //            if (items[3] == 1)
                //                tempObj.spawnToCamera = true;

                //            temp.ObjectItemArray.Add(tempObj);
                //            break;
                //        }

                //    default:
                //        throw new System.Exception(string.Format("Failed to parse item on line number {0}.", counter));
                //        break;
                //}
            }
        }
	}
}
