using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBranch : MonoBehaviour
{
    public GameObject branch;

    public GameObject trunk1;
    public GameObject trunk2;
    public GameObject trunk3;
    public GameObject trunk4;

    public List<Transform> spawnPoints1 = new List<Transform>();
    public List<Transform> spawnPoints2 = new List<Transform>();
    public List<Transform> spawnPoints3 = new List<Transform>();
    public List<Transform> spawnPoints4 = new List<Transform>();

    MajorScript majorScript;

    public float spawnTimer;
    float timer;
    bool clocked = false;
    bool treeFull = false;
    
    // Start is called before the first frame update
    void Start()
    {
        SetSpawns();
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            clocked = true;
    }

    void Update()
    {
        Transform selectedSpawn = null;
        int tries = 0;

        if (!clocked || treeFull) return;

        switch (majorScript.difficultyLevel)
        {
            case 1:
                selectedSpawn = spawnPoints1[Random.Range(0,spawnPoints1.Count)].GetComponent<Transform>();
                while(true)
                {
                    tries++;

                    if (selectedSpawn.childCount == 0)
                        break;
                    else if (tries == spawnPoints1.Count)
                        return;
                }
            break;
            case 2:
                selectedSpawn = spawnPoints2[Random.Range(0,spawnPoints2.Count)].GetComponent<Transform>();
                while(true)
                {
                    tries++;

                    if (selectedSpawn.childCount == 0)
                        break;
                    else if (tries == spawnPoints2.Count)
                        return;
                }
            break;
            case 3:
                selectedSpawn = spawnPoints3[Random.Range(0,spawnPoints3.Count)].GetComponent<Transform>();
                while(true)
                {
                    tries++;

                    if (selectedSpawn.childCount == 0)
                        break;
                    else if (tries == spawnPoints3.Count)
                        return;
                }
            break;
            case 4:
                selectedSpawn = spawnPoints4[Random.Range(0,spawnPoints4.Count)].GetComponent<Transform>();
                while(true)
                {
                    tries++;

                    if (selectedSpawn.childCount == 0)
                        break;
                    else if (tries == spawnPoints4.Count)
                    {
                        // The Tree is already full of branches
                        treeFull = true;
                        return;
                    }
                }
            break;
        }

        GameObject newBranch = Instantiate(branch, selectedSpawn.position, selectedSpawn.rotation, selectedSpawn);
        
        if (selectedSpawn.name.Contains("Right"))
        {
            Transform branchTrans = newBranch.GetComponent<Transform>();
            branchTrans.localScale = new Vector3( - Mathf.Abs(branchTrans.localScale.x), branchTrans.localScale.y, 0);
        }
        timer = spawnTimer;
        clocked = false;
        
    }

    public void SetSpawns()
    {
        majorScript = GetComponent<MajorScript>();
        Transform[] spawnTransforms = trunk1.transform.GetComponentsInChildren<Transform>();
        foreach(Transform t in spawnTransforms)
        {
            if (t.gameObject.name.Contains("SpawnPoint"))
            {
                spawnPoints1.Add(t);
            }
        }

        Transform[] spawnTransforms2 = trunk2.transform.GetComponentsInChildren<Transform>();
        foreach(Transform t in spawnTransforms2)
        {
            if (t.gameObject.name.Contains("SpawnPoint"))
            {
                spawnPoints2.Add(t);
            }
        }

        Transform[] spawnTransforms3 = trunk3.transform.GetComponentsInChildren<Transform>();
        foreach(Transform t in spawnTransforms3)
        {
            if (t.gameObject.name.Contains("SpawnPoint"))
            {
                spawnPoints3.Add(t);
            }
        }

        Transform[] spawnTransforms4 = trunk4.transform.GetComponentsInChildren<Transform>();
        foreach(Transform t in spawnTransforms4)
        {
            if (t.gameObject.name.Contains("SpawnPoint"))
            {
                spawnPoints4.Add(t);
            }
        }

        timer = spawnTimer;
    }
}