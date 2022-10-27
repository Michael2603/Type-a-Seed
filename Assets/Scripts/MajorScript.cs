using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Health;

public class MajorScript : MonoBehaviour
{

    [SerializeField] List<GameObject> lifeIcons = new List<GameObject>();

    public int difficultyLevel = 1;
    public int levelPoints = 0;
    int currentPoint = 0;
    int health = HealthController.health;

    public GameObject warningText;

    public List<AudioSource> audioSources;
    public AudioClip victoryClip;
    public List<AudioClip> audioClips;

    void Start()
    {
        levelPoints = Random.Range(5,6);
        health = 3;
    }
    
    void Update()
    {
        if (health == 3)
        {
            lifeIcons[0].SetActive(true);
            lifeIcons[1].SetActive(true);
            lifeIcons[2].SetActive(true);
        }
        else if (health == 2)
        {
            lifeIcons[0].SetActive(true);
            lifeIcons[1].SetActive(true);
            lifeIcons[2].SetActive(false);
        }
        else if (health == 1)
        {
            lifeIcons[0].SetActive(true);
            lifeIcons[1].SetActive(false);
            lifeIcons[1].SetActive(false);
        }
        else if (health <= 0)
        {
            GameOver();
        }

        if (currentPoint >= levelPoints)
        {
            LevelUp();
        }

        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (warningText.GetComponent<Animator>().GetBool("Appear") == false)
            {
                warningText.GetComponent<Animator>().SetBool("Appear", true);
                GameObject.Find("ButtonsController").GetComponent<ButtonsController>().TriggerSound();
            }
            else
                SceneManager.LoadScene(0);
        }
    }

    public void Pointed()
    {
        currentPoint++;
        audioSources[1].clip = audioClips[0];
        audioSources[1].Play();

        if (difficultyLevel == 4 && currentPoint == levelPoints)
            Victory();
        
    }

    public void LostPoint()
    {
        audioSources[1].clip = audioClips[1];
        audioSources[1].Play();
        health--;
    }

    public void LevelUp()
    {
        difficultyLevel++;

        if (difficultyLevel == 1)
        {
            levelPoints = Random.Range(5,6);
            currentPoint = 0;
        }
        if (difficultyLevel == 2)
        {
            levelPoints = Random.Range(4,5);
            currentPoint = 0;
        }
        if (difficultyLevel == 3)
        {
            levelPoints = levelPoints = 3;
            currentPoint = 0;
        }
        if (difficultyLevel == 4)
        {
            levelPoints = Random.Range(5,6);
            currentPoint = 0;
        }
    }

    void GameOver()
    {
        difficultyLevel = 1;
        currentPoint = 0;
        levelPoints = Random.Range(5,6);
        health = 3;

        SpawnBranch spawnScript = GetComponent<SpawnBranch>();
        
        for (int i = 0; i < 5; i++)
        {
            if (spawnScript.spawnPoints1[i].GetComponent<Transform>().childCount != 0)
                Destroy(spawnScript.spawnPoints1[i].GetComponent<Transform>().GetChild(0).gameObject);
        }
        for (int i = 0; i < 6; i++)
        {
            if (spawnScript.spawnPoints2[i].childCount != 0)
                Destroy(spawnScript.spawnPoints2[i].GetChild(0).gameObject);
        }
        for (int i = 0; i < 5; i++)
        {
            if (spawnScript.spawnPoints3[i].childCount != 0)
                Destroy(spawnScript.spawnPoints3[i].GetChild(0).gameObject);
        }
        for (int i = 0; i < 3; i++)
        {
            if (spawnScript.spawnPoints4[i].childCount != 0)
                Destroy(spawnScript.spawnPoints4[i].GetChild(0).gameObject);
        }
    }

    public void Victory()
    {
        GameObject.Find("Tree").GetComponent<ParticleSystem>().Play();
        GameObject.Find("Victory").GetComponent<Text>().enabled = true;
        audioSources[0].clip = victoryClip;
        audioSources[0].Play();
    }

    public void PlayTypeSound()
    {
        audioSources[2].Play();
    }
}