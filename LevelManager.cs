using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance = null;

    [Tooltip("The list of all levels")]
    public List<GameObject> levelPrefab = new List<GameObject>();

    [Tooltip("Father of allready spawned level instances")]
    public Transform levelParent;

    [Tooltip("The position in the world where LevelInstance would be spawned")]
    public Transform levelSpawnPoint;

    [Tooltip("Curent level number displayed on gameplay HUD")]
    public int currentLevel;

    [Tooltip("Represents transform of a player object in the scene")]
    public Transform player;

    [Tooltip("Spawn position for a player to start each level on the same place")]
    public Transform playerSpawnPoint;

    [Tooltip("Bonus zone, activate this if player comes first")]
    public GameObject bonusZone;

    [Tooltip("Represent all bots in current level instance")]
    public Transform botsContainer;

    [Tooltip("Reference to Nav Mesh Agents of all bots")]
    public List<NavMeshAgent> bots = new List<NavMeshAgent>();


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
    
    public void LoadLevel(int spawnIndex)
    {
        //Destroy all levels created before 
        for (int i = 0; i < levelParent.childCount; i++)
        {
            Destroy(levelParent.GetChild(i).gameObject);
        }

        //Clear the bots from list
        bots.Clear();

        //Create new instance of level INDEX must be the same as level index in list
        GameObject LevelInstance = Instantiate(levelPrefab[spawnIndex], levelSpawnPoint.position, Quaternion.identity);
        LevelInstance.transform.SetParent(levelParent);

        //Look for bots container
        PrepareBotsToRun();
        PreparePlayerToRun();

        //GetBonusZone
        bonusZone = GameObject.FindGameObjectWithTag("BonusZone");

        //Run the Game
        GameManager.Instance.PrepareToStartGame();

        //Show what level is it? 
        currentLevel = spawnIndex;
        UImanager.Instance.CurrentLevelNumber.text = currentLevel.ToString();
        GameManager.Instance.levelReady = true;
    }
    public void PrepareBotsToRun()
    {

        botsContainer = GameObject.FindGameObjectWithTag("BotsContainer").transform;
        for (int i = 0; i < botsContainer.childCount; i++)
        {
            bots.Add(botsContainer.GetChild(i).gameObject.GetComponent<NavMeshAgent>());
        }
        foreach (NavMeshAgent agent in bots)
        {
            agent.speed = 0;
        }

        GameManager.Instance.botsReady = true;
    }
    public void PreparePlayerToRun()
    {
        player.transform.position = playerSpawnPoint.position;
        //ResetCamera

        GameManager.Instance.playerReady = true;
    }
    public void LevelStart()
    {
        //VOODOO ON GAME STARTS
        foreach (NavMeshAgent agent in bots)
        {
            agent.speed = 12;
        }
        // Start player DOTween 
    }
    public void ActivateBonusZone()
    {
        bonusZone.SetActive(true);
    }

}



