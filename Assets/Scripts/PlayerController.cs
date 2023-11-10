using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputController inputController;


    float lookAmountVertical;
    float lookAmountHorizontal;
    float maxAngle = 15f;
    float minAngle = -15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputController.HandleInputs();
    }



    void CameraRotations()
    {
        lookAmountVertical = lookAmountVertical + inputController.horizontalCameraMovement;
        lookAmountHorizontal = lookAmountHorizontal - inputController.verticalCameraMovement;
        lookAmountHorizontal = Mathf.Clamp(lookAmountHorizontal, minAngle, maxAngle);
    }
}
