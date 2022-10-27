using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;
using UnityEngine.EventSystems;

public class CodeScript : MonoBehaviour
{
    public float timerCount;
    float timer;

    public GameObject canvasText;
    public List<GameObject> branch;
    branch currentBranch;

    public string codeString;
    public int codeSize;

    bool started = false;
    bool sucess = false;

    MajorScript majorScript;

    AudioSource audioSource;
    public AudioClip[] audioClips;

    void Start()
    {
        majorScript = GameObject.Find("MajorController").GetComponent<MajorScript>();
        audioSource = GetComponent<AudioSource>();

        switch (majorScript.difficultyLevel)
        {
            case 1:
                codeSize = Random.Range(4,6);
                timerCount = 3;
            break;
            case 2:
                codeSize = Random.Range(5,7);
                timerCount = 4f;
            break;
            case 3:
                codeSize = Random.Range(7,9);
                timerCount = 5;
            break;
            case 4:
                codeSize = Random.Range(8,11);
                timerCount = 6;
            break;
        }

        for (int i = 0; i < branch.Count; i++)
        {
            // Check if the first number of the branch name is equal to the size of the code and if the last digit of the branch's name is equal to the difficulty level
            if ( (branch[i].name[0] == codeSize.ToString()[0]) && (char.GetNumericValue(branch[i].name[branch[i].name.Length - 1]) == majorScript.difficultyLevel) )
            {
                currentBranch = branch[i].GetComponent<branch>();
                currentBranch.gameObject.SetActive(true);
                audioSource.clip = audioClips[Random.Range(0,2)];
                audioSource.Play();
            }
        }
    }

    void Update()
    {
        canvasText = GameObject.Find("Text");

        switch (currentBranch.gameObject.name[0])
        {
            case '4': 
                codeSize = 4;
            break;
            case '5':
                codeSize = 5;
            break;
            case '6': 
                codeSize = 6;
            break;
            case '7':
                codeSize = 7;
            break;
            case '8': 
                codeSize = 8;
            break;
            case '9':
                codeSize = 9;
            break;
            case '1':
                codeSize = 10;
            break;
        }

        if (started && !sucess)
        {

            KeyCode pressedKey = (KeyCode)System.Enum.Parse(typeof(KeyCode),codeString[0].ToString());
            
            if (Input.GetKeyDown(pressedKey))
            {
                if (codeString.Length > 1)
                {
                    codeString = codeString.Substring(1, codeString.Length - 1);
                    currentBranch.StartAnimation();
                    majorScript.PlayTypeSound();
                }
                else
                {
                    sucess = true;
                    currentBranch.SpawnCup();
                    currentBranch.ball.SetActive(false);
                    codeString = "";
                    majorScript.PlayTypeSound();
                    majorScript.Pointed();
                }
            }
            canvasText.GetComponent<UnityEngine.UI.Text>().text = codeString;
        }
    }

    void FixedUpdate()
    {
        if (started && !sucess)
        {
            timer -= Time.deltaTime;
            if (timer <= 2)
            {
                Destroy(currentBranch.transform.GetChild(2).gameObject.GetComponent<EventTrigger>());
                currentBranch.transform.GetChild(2).gameObject.GetComponent<Animator>().SetTrigger("Trigger");
            }
            if (timer <= 0)
            {
                Destroy(this.gameObject);
                majorScript.LostPoint();
            }
        }
    }

    public void CodeClicked()
    {
        if ( !(codeString == "") )
        {
            codeString = "";
        }

        for (int i = 0; i < codeSize; i++)
        {
            char letter = (char)('A' + Random.Range(0,26));
            codeString += letter;
        }        

        timer = timerCount;
        started = true;

        currentBranch.animator.SetTrigger("Grow");
        currentBranch.animator.speed = 1;

    }
}