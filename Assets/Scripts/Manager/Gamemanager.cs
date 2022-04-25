using System.Collections;
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
    private string sceneIndex = "SceneIndex";

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
    }

    public void Update()
    {
        IsGameOver = playerCtrl.IsDead;
        UIManager.Instance.IsShowGameOverPanel(IsGameOver);
    }

    public void IsEnemy(Enemy enemy)
    {
        ListEnemy.Add(enemy);
    }

    public void EnemyDead(Enemy enemy)
    {
        ListEnemy.Remove(enemy);
        if (ListEnemy.Count == 0)
        {
            doorExit.OpenDoor();
            GameManager.Instance.SaveData();
        }
    }

    public void IsPlayer(PlayerController controller)
    {
        playerCtrl = controller;
    }

    public void IsDoor(Door door)
    {
        doorExit = door;
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

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        if(PlayerPrefs.HasKey(sceneIndex))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt(sceneIndex));
        }
        else
        {
            NewGame();
        }        
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
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
        PlayerPrefs.SetInt(sceneIndex, SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.Save();
    }

    
}
