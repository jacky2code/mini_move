using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;

    public bool IsGameOver;

    private PlayerController playerCtrl;

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
    }

    public void Update()
    {
        IsGameOver = playerCtrl.IsDead;
        UIManager.Instance.IsShowGameOverPanel(IsGameOver);
    }
}
