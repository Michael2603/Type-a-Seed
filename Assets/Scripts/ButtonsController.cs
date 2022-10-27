using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    public static ButtonsController Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        Screen.SetResolution(1920, 1080, true, 60);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject TutorialMenu;
    [SerializeField] GameObject CreditsMenu;

    public void EnterMain()
    {
        TutorialMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    
    public void EnterTutorial()
    {
        TutorialMenu.SetActive(true);
        CreditsMenu.SetActive(false);
        MainMenu.SetActive(false);
    }

    public void EnterCredits()
    {
        TutorialMenu.SetActive(false);
        CreditsMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void TriggerSound()
    {
        GetComponent<AudioSource>().Play();
    }
}
