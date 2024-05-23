using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerUIManager : MonoBehaviour
{
    [Header("Crosshair")]
    public GameObject crossHair;


    [Header("Ammo")]
    public TextMeshProUGUI currentAmmoCountText;
    public TextMeshProUGUI reservedAmmoCountText;
}
