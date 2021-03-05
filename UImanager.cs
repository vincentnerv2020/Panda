using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UImanager : MonoBehaviour
{
    public static UImanager Instance = null;

    [Tooltip("All menus in the game")] 
    public GameObject[] panels;
    //0 main menu panel
    //1 Gameplay panel
    //2 StartGameTimeCounter
    //3 Tips
    //4 Defeat panel
    //5 congratulation panel
    //6 settings panel



    [Tooltip("All menus in the game")]
    //MAIN MENU HUD
    public TMP_Text CrystalsCount;
    public TMP_Text CurrentLevelNumber;
    
    //GAMEPLAY HUD
    public TMP_Text ItemCount;
    public TMP_Text MotivationInfo; //Cool, Great, Super ...
    public TMP_Text SecondsToStart;

    //REWARDS
    public TMP_Text Reward; //When player finished as 2Th he get a simple reward
    public TMP_Text DailyReward;

    //TEXT that would be shown to player after he get a new item
    public GameObject pickUpPrefab; //This prefab represent text +1 when player pickUp new item

    //ACTIONS
    public TMP_Text PlayerAction; //

    //LEVEL INFORMATION
    public TMP_Text Announcements;  //Water is COMING!


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

    public  void ActivatePanel(int panelIndex)
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        panels[panelIndex].SetActive(true);
    }


}
