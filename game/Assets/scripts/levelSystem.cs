using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class levelSystem : MonoBehaviour
{
    //level system for win, load, fail and restart
    public GameObject[] levels;
    public int level;
    public GameObject winMenu;
    public GameObject failMenu;
    public int fakeLevel;
    public TMP_Text levelText;
    public TMP_Text nextLevelText;
    public bool alreadyDone;

    public bool startBool;
    public AudioSource levelWinSound;
    public AudioSource levelfailSound;
    public GameObject currentLevel;
    public LevelEditor levelEditor;
    public MainMove mainMove;
    public ParticleSystem confetti;
    void Start()
    {
       // PlayerPrefs.SetInt("level",0);
        //PlayerPrefs.SetInt("fakeLevel", 1);
        level = PlayerPrefs.GetInt("level", 0); 
        fakeLevel = PlayerPrefs.GetInt("fakeLevel", 1);
        levelText.text = fakeLevel.ToString();
        nextLevelText.text = (fakeLevel + 1).ToString();
        LoadLevel();
        currentLevel = levels[level].gameObject;
        levelEditor.EditorEssentials();

    }


    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadLevel()

    {
        level = PlayerPrefs.GetInt("level", 0);
        foreach (var item in levels)
        {
            item.SetActive(false);
        }
        levels[level].SetActive(true);

        fakeLevel = PlayerPrefs.GetInt("fakeLevel", 1);



    }
    public void WinLevel()
    {
        if (!alreadyDone)
        {
            winMenu.SetActive(true);
            level = PlayerPrefs.GetInt("level", 0);
            level = (level + 1) % levels.Length;
            PlayerPrefs.SetInt("level", level);
            fakeLevel = PlayerPrefs.GetInt("fakeLevel", 1);
            fakeLevel++;
            PlayerPrefs.SetInt("fakeLevel", fakeLevel);
            alreadyDone = true;
            mainMove.gamePlayable = false;
            confetti.Play();
            // levelWinSound.Play();
        }

    }
    public void FailMenu()
    {
        if (!alreadyDone)
        {
            failMenu.SetActive(true);
            alreadyDone = true;
            mainMove.gamePlayable = false;
            //levelfailSound.Play();

        }

    }
}
