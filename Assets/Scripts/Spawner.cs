using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform PoleObj;
    public Transform SphereObj;
    public Transform GiantSphereObj;


    public IEnumerator PoleSpawn(int multipleSpawn, int objectSpeed, bool launchToCamera)
    {
        int multiplier = 25;
        for (int i = 0; i < multipleSpawn; i++)
        {
            Vector3 randRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), 1); //Vector3([X], [Y], [Z])

            //Vector3 randRotation = new Vector3(Random.Range(xAngle.x, xAngle.y), Random.Range(yAngle.x, yAngle.y), 1); //Vector3([X], [Y], [Z])
            Vector3 randRot = new Vector3(Random.Range(-1 * multiplier, 1 * multiplier), Random.Range(-1 * multiplier, 1 * multiplier), Random.Range(-1 * multiplier, 1 * multiplier));
            //Debug.Log(randRot);

            Transform obj = Instantiate(PoleObj);
            if (launchToCamera)
            {
                var dir = GameObject.FindGameObjectWithTag("MainCamera").transform.position - transform.position;
                obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
                obj.Rotate(randRot);
            }
            else
                obj.Rotate(randRotation);
        }

        yield return null;
    }

    public IEnumerator SphereSpawn(int multipleSpawn, int objectSpeed, bool launchToCamera)
    {
        int multiplier = 25;

        for (int i = 0; i < multipleSpawn; i++)
        {
            float randomizedPercent = 0.8f;

            float randomSpeed = objectSpeed + (Random.Range(-1f, 1f) * (objectSpeed * randomizedPercent));

            Vector3 randRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), 1); //Vector3([X], [Y], [Z])

            //Vector3 randRotation = new Vector3(Random.Range(xAngle.x, xAngle.y), Random.Range(yAngle.x, yAngle.y), 1); //Vector3([X], [Y], [Z])
            Vector3 randRot = new Vector3(Random.Range(-1 * multiplier, 1 * multiplier), Random.Range(-1 * multiplier, 1 * multiplier), Random.Range(-1 * multiplier, 1 * multiplier));
            //Debug.Log(randRot);

            Transform obj = Instantiate(SphereObj);

            if (launchToCamera)
            {
                var dir = GameObject.FindGameObjectWithTag("MainCamera").transform.position - transform.position;
                obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
                obj.Rotate(randRot);
            }
            else
                obj.Rotate(randRotation);

            obj.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * randomSpeed, ForceMode.VelocityChange);
        }

        yield return null;
    }

    public IEnumerator GiantSphereSpawn(int multipleSpawn, int objectSpeed, bool launchToCamera)
    {
        int multiplier = 25;

        for (int i = 0; i < multipleSpawn; i++)
        {
            float randomizedPercent = 0.8f;

            float randomSpeed = objectSpeed + (Random.Range(-1f, 1f) * (objectSpeed * randomizedPercent));

            Vector3 randRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), 1); //Vector3([X], [Y], [Z])

            //Vector3 randRotation = new Vector3(Random.Range(xAngle.x, xAngle.y), Random.Range(yAngle.x, yAngle.y), 1); //Vector3([X], [Y], [Z])
            Vector3 randRot = new Vector3(Random.Range(-1 * multiplier, 1 * multiplier), Random.Range(-1 * multiplier, 1 * multiplier), Random.Range(-1 * multiplier, 1 * multiplier));
            //Debug.Log(randRot);

            Transform obj = Instantiate(GiantSphereObj);

            if (launchToCamera)
            {
                var dir = GameObject.FindGameObjectWithTag("MainCamera").transform.position - transform.position;
                obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
                obj.Rotate(randRot);
            }
            else
                obj.Rotate(randRotation);

            obj.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * randomSpeed, ForceMode.VelocityChange);
        }

        yield return null;
    }

    public IEnumerator NullPlaceholder()
    {
        yield return null;
    }
}
