using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject HealthBar;

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
}
