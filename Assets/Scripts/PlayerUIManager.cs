using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerUIManager : MonoBehaviour
{
    PlayerController playerController;
    StatusPopUps statusPopUps;
    [Header("Crosshair")]
    public GameObject crossHair;
    public GameObject gameOverPanel;

    [Header("Ammo")]
    public TextMeshProUGUI currentAmmoCountText;
    public TextMeshProUGUI reservedAmmoCountText;
    void Awake()
    {
        statusPopUps = GetComponentInChildren<StatusPopUps>();
        playerController = FindAnyObjectByType<PlayerController>();
    }
    public void DisplayHealthPopUp()
    {
        statusPopUps.DisplayHealthPopUp(playerController.playerStatManager.playerHealth);
    }


    public void RestartGame()
    {
        SceneManager.LoadScene("EscapeMaze");
    }

}
