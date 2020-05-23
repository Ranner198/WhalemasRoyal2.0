using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrack : MonoBehaviour
{
    public GameObject spawnPosition;
    public bool isSpawned = false;

    public List<GameObject> signs = new List<GameObject>();
    public List<GameObject> signSpawnPositions = new List<GameObject>();

    public List<GameObject> objectSpawnPositions = new List<GameObject>();
    public List<GameObject> objectsToSpawn = new List<GameObject>();

    public void Start()
    {
        List<GameObject> adSigns = new List<GameObject>();
        foreach (GameObject signSpawnPosition in signSpawnPositions)
        {
            GameObject newSign = Instantiate(signs[Random.Range(0, signs.Count-1)], signSpawnPosition.transform.position, Quaternion.identity);
            newSign.name = "NewSign";
            newSign.transform.parent = signSpawnPosition.transform;
            newSign.transform.localRotation = Quaternion.Euler(-180, 180, 180);
            adSigns.Add(newSign);
        }

        int spawnPosition = Random.Range(0, 3);
        int objectToSpawn = Random.Range(0, objectsToSpawn.Count);

        GameObject newObject = Instantiate(objectsToSpawn[objectToSpawn], objectSpawnPositions[spawnPosition].transform.position, Quaternion.Euler(-82.595f, 0, 90));

        GameManager.instance.AddDestoryable(gameObject, adSigns, newObject);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (!isSpawned && coll.tag == "Player")
        {
            isSpawned = true;
            SpawnTrackPiece();
            GameManager.instance.DestoryDestroyable();
        }
    }

    public void SpawnTrackPiece()
    {
        GameObject raceTrackPiece = Instantiate(Resources.Load("RaceTrack") as GameObject, spawnPosition.transform.position, transform.rotation);
    }
}
