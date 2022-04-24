using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject HealthBar;

    public Slider HealthBarBoss;

    [Header("UI Elements")]
    public GameObject MenuPause;

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
                    HealthBar.transform.GetChild(0).gameObject.SetActive(true);
                    HealthBar.transform.GetChild(1).gameObject.SetActive(true);
                    HealthBar.transform.GetChild(2).gameObject.SetActive(true);
                }
                break;
            case 2:
                {
                    HealthBar.transform.GetChild(0).gameObject.SetActive(true);
                    HealthBar.transform.GetChild(1).gameObject.SetActive(true);
                    HealthBar.transform.GetChild(2).gameObject.SetActive(false);
                }
                break;
            case 1:
                {
                    HealthBar.transform.GetChild(0).gameObject.SetActive(true);
                    HealthBar.transform.GetChild(1).gameObject.SetActive(false);
                    HealthBar.transform.GetChild(2).gameObject.SetActive(false);
                }
                break;
            case 0:
                {
                    HealthBar.transform.GetChild(0).gameObject.SetActive(false);
                    HealthBar.transform.GetChild(1).gameObject.SetActive(false);
                    HealthBar.transform.GetChild(2).gameObject.SetActive(false);
                }
                break;
            default:
                {
                    HealthBar.transform.GetChild(0).gameObject.SetActive(true);
                    HealthBar.transform.GetChild(1).gameObject.SetActive(true);
                    HealthBar.transform.GetChild(2).gameObject.SetActive(true);
                }
                break;
        }
    }

    public void GamePause()
    {
        MenuPause.SetActive(true);
        // 游戏暂停
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        MenuPause.SetActive(false);
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

}
