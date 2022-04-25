﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsGameOver;

    // 敌人列表：当列表为空时，则打开门，可以进入下一关
    public List<Enemy> ListEnemy = new List<Enemy>();

    private PlayerController playerCtrl;

    private Door doorExit;

    private string playerHealthKey = "PlayerHealth";

    public void Awake()
    {   
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerCtrl = FindObjectOfType<PlayerController>();

        doorExit = FindObjectOfType<Door>();
    }

    public void Update()
    {
        IsGameOver = playerCtrl.IsDead;
        UIManager.Instance.IsShowGameOverPanel(IsGameOver);
    }

    public void AddEnemyList(Enemy enemy)
    {
        ListEnemy.Add(enemy);
    }

    public void RemoveEnemyFromList(Enemy enemy)
    {
        ListEnemy.Remove(enemy);
        if (ListEnemy.Count == 0)
        {
            doorExit.OpenDoor();
        }
    }

    /// <summary>
    /// 游戏结束后重新开始
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.DeleteKey(playerHealthKey);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// 退出游戏，build后才可以调用
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// 读取保存的血量
    /// </summary>
    /// <returns></returns>
    public float LoadPlayerHealth()
    {
        if (!PlayerPrefs.HasKey(playerHealthKey))
        {
            PlayerPrefs.SetFloat(playerHealthKey, 3.0f);
        }
        float currentHealth = PlayerPrefs.GetFloat(playerHealthKey);
        
        return currentHealth;
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat(playerHealthKey, playerCtrl.Health);
        PlayerPrefs.Save();
    }
}
