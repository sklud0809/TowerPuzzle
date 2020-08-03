using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TextManagerScript : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject touchPleaseButton;
    public GameObject tutorialText;
    public GameObject tutorial2Text;
    public GameObject tutorialImage;
    public GameObject panel;
    public GameObject gameStartButton;
    public GameObject nextButton;
    public GameObject backButton;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        touchPleaseButton.SetActive(true);
        tutorialText.SetActive(false);
        tutorial2Text.SetActive(false);
        tutorialImage.SetActive(false);
        nextButton.SetActive(false);
        gameStartButton.SetActive(false);
        panel.SetActive(false);
    }

    
    void Update()
    {
        
    }
    public void TouchPlease()
    {
        tutorialImage.SetActive(true);
        tutorialText.SetActive(true);
        panel.SetActive(true);
        nextButton.SetActive(true);
        touchPleaseButton.SetActive(false);
        audioSource.Play();
        backButton.SetActive(false);
    }

    public void Next()
    {
        tutorialText.SetActive(false);
        nextButton.SetActive(false);
        tutorial2Text.SetActive(true);
        gameStartButton.SetActive(true);
        backButton.SetActive(true);
        audioSource.Play(); 
    }
    public void GameSceneClick()
    {
        audioSource.Play();
        //ここで移りたいシーンを指定します。
        SceneManager.LoadScene("GameScene");
    }
    public void Back()
    {
        tutorialImage.SetActive(true);
        tutorialText.SetActive(true);
        tutorial2Text.SetActive(false);
        panel.SetActive(true);
        nextButton.SetActive(true);
        touchPleaseButton.SetActive(false);
        audioSource.Play();
        backButton.SetActive(false);
    }
}
