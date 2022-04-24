using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject HealthBarPlayer;

    public Slider HealthBarBoss;

    [Header("UI Elements")]
    public GameObject PanelMenuPause;
    public GameObject PanelGameOver;

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

    /// <summary>
    /// 更新血条
    /// </summary>
    /// <param name="curHealth"></param>
    public void UpdateHealth(float curHealth)
    {
        switch (curHealth)
        {
            case 3:
                {
                    HealthBarPlayer.transform.GetChild(0).gameObject.SetActive(true);
                    HealthBarPlayer.transform.GetChild(1).gameObject.SetActive(true);
                    HealthBarPlayer.transform.GetChild(2).gameObject.SetActive(true);
                }
                break;
            case 2:
                {
                    HealthBarPlayer.transform.GetChild(0).gameObject.SetActive(true);
                    HealthBarPlayer.transform.GetChild(1).gameObject.SetActive(true);
                    HealthBarPlayer.transform.GetChild(2).gameObject.SetActive(false);
                }
                break;
            case 1:
                {
                    HealthBarPlayer.transform.GetChild(0).gameObject.SetActive(true);
                    HealthBarPlayer.transform.GetChild(1).gameObject.SetActive(false);
                    HealthBarPlayer.transform.GetChild(2).gameObject.SetActive(false);
                }
                break;
            case 0:
                {
                    HealthBarPlayer.transform.GetChild(0).gameObject.SetActive(false);
                    HealthBarPlayer.transform.GetChild(1).gameObject.SetActive(false);
                    HealthBarPlayer.transform.GetChild(2).gameObject.SetActive(false);
                }
                break;
            default:
                {
                    HealthBarPlayer.transform.GetChild(0).gameObject.SetActive(true);
                    HealthBarPlayer.transform.GetChild(1).gameObject.SetActive(true);
                    HealthBarPlayer.transform.GetChild(2).gameObject.SetActive(true);
                }
                break;
        }
    }

    public void GamePause()
    {
        PanelMenuPause.SetActive(true);
        // 游戏暂停
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        PanelMenuPause.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// 设置 Boss 血条最大值
    /// </summary>
    /// <param name="health"></param>
    public void SetMaxValueHealthBarBoss(float health)
    {
        HealthBarBoss.maxValue = health;
    }

    /// <summary>
    /// 更新 Boss 血条
    /// </summary>
    /// <param name="health"></param>
    public void updateValueHealthBarBoss(float health)
    {
        HealthBarBoss.value = health;
    }

    /// <summary>
    /// 显示/隐藏 GameOverPanel
    /// </summary>
    /// <param name="show"></param>
    public void IsShowGameOverPanel(bool show)
    {
        PanelGameOver.SetActive(show);
    }

    /// <summary>
    /// 游戏结束后重新开始
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
