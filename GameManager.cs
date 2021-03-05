using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    //GameStates
    public bool levelReady;
    public bool botsReady;
    public bool playerReady;
    public bool gameIsRunning;

    public int playerPlace;

    //HighScore
    private int crystals;
    public int Crystals
    {
        get { return crystals; }
        set
        {
            crystals = value;
            PlayerPrefs.SetInt("Crystals", crystals);
            UImanager.Instance.CrystalsCount.text = crystals.ToString();
            //Add some fx
        }
    }

    private void Start()
    {
        //Singleton 
        // “еперь, провер€ем существование экземпл€ра
        if (Instance == null)
        { // Ёкземпл€р менеджера был найден
            Instance = this; // «адаем ссылку на экземпл€р объекта
        }
        else if (Instance == this)
        { // Ёкземпл€р объекта уже существует на сцене
            Destroy(gameObject); // ”дал€ем объект
        }

    }


    #region Menu_CallBacks
    public void OnPlayerClickHome()
    {
        //SimpleGameRestart
        SceneManager.LoadScene(0);
    }
    public void OnPlayerClickSettings()
    {

    }
    #endregion Menu_CallBacks

    #region Gameplay_CallBacks

    //Before GameStarts
    public void GameInitialization()
    {
        Crystals = PlayerPrefs.GetInt("Crystals", 0);
        GetRewardForReturning(0);
        UImanager.Instance.ActivatePanel(3); //Activate tips
    }
    public void GetRewardForReturning(int dailyRewardCount)
    {
        //In future value will depends on how many time player was innactive
        Crystals += Random.Range(100, 1000);
    }
    public void PrepareToStartGame()
    {
        //Activate GamePlayHud
        UImanager.Instance.ActivatePanel(1);

        //Activate countTimer
        StartCoroutine("StartGameCounter");
    }
    public IEnumerator StartGameCounter()
    {
        //Show CounterPanel
        UImanager.Instance.panels[2].SetActive(true);
        yield return new WaitForSeconds(1f);
        UImanager.Instance.SecondsToStart.text = "3";
        yield return new WaitForSeconds(1f);
        UImanager.Instance.SecondsToStart.text = "2";
        yield return new WaitForSeconds(1f);
        UImanager.Instance.SecondsToStart.text = "1";
        yield return new WaitForSeconds(0.5f);
        UImanager.Instance.SecondsToStart.text = "RUN!";

        //Run the game
        LevelManager.Instance.LevelStart();
    }

    //Game is Running
    public void OnPlayerTouchFinish()
    {
        if (playerPlace == 1)
        {
            LevelManager.Instance.ActivateBonusZone();
        }
    }
    public void Congratulation(int place, int reward)
    {
        //Make player stop
        //Make him Dancing
        Crystals += reward;
        //Make Crystals fly


        //Show congratulation panel
        UImanager.Instance.ActivatePanel(5);

        //Show how many crystals player get
        UImanager.Instance.Reward.text = reward.ToString();


    }
    public void Defeat()
    {
        Time.timeScale = 0;
        UImanager.Instance.ActivatePanel(4);
    }
    public void RestartCurrentLevel()
    {

    }

    //LastChance give player reward to continue level
    public void Revive(int itemCount)
    {
        //Spawn player 4m higher
        LevelManager.Instance.player.position += Vector3.up * 4; 
    }

    #endregion  Gameplay_CallBacks


    //This method will be called after player complete level



}
