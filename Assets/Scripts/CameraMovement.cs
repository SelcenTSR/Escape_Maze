using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] InputController inputController;
    [SerializeField] Transform cameraPivot;
    [SerializeField] GameObject player;
    [SerializeField] Camera cameraObject;

    // Start is called before the first frame update
    [SerializeField] PlayerController playerController;
    public Transform aimedCameraPosition;

    float lookAmountVertical;
    float lookAmountHorizontal;
    float maxAngle = 15f;
    float minAngle = -15;

    Vector3 cameraFollowVelocity=Vector3.zero;
    Vector3 targetPos;
    Vector3 cameraRot;

    Quaternion targetRot;

    float cameraSmoothTime = .2f;
    float aimedCameraSmoothTime = 3f;

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        if (Input.GetMouseButton(0))
        {
            inputController.HandleInputs();
            CameraRotations();
        }
        
    }

    void FollowPlayer()
    {
        if (playerController.isAiming)
        {
            targetPos = Vector3.SmoothDamp(transform.position, aimedCameraPosition.transform.position, ref cameraFollowVelocity, cameraSmoothTime * Time.deltaTime);
            transform.position = targetPos;
        }
        else
        {
            targetPos = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraFollowVelocity, cameraSmoothTime * Time.deltaTime);
            transform.position = targetPos;
        }
        
    }

    void CameraRotations()
    {
        if (playerController.isAiming)
        {
            cameraPivot.transform.localRotation = Quaternion.Euler(0, 0, 0);
            lookAmountVertical = lookAmountVertical + inputController.horizontalCameraMovement;
            lookAmountHorizontal = lookAmountHorizontal - inputController.verticalCameraMovement;
            lookAmountHorizontal = Mathf.Clamp(lookAmountHorizontal, minAngle, maxAngle);

            cameraRot = Vector3.zero;
            cameraRot.y = lookAmountVertical;
            targetRot = Quaternion.Euler(cameraRot);
            targetRot = Quaternion.Slerp(transform.rotation, targetRot, aimedCameraSmoothTime);
            transform.rotation = targetRot;
           

            cameraRot = Vector3.zero;
            cameraRot.x = lookAmountHorizontal;
            targetRot = Quaternion.Euler(cameraRot);
            targetRot = Quaternion.Slerp(cameraPivot.transform.localRotation, targetRot, aimedCameraSmoothTime);
            cameraObject.transform.localRotation = targetRot;
        }
        else
        {
            cameraObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            lookAmountVertical = lookAmountVertical + inputController.horizontalCameraMovement;
            lookAmountHorizontal = lookAmountHorizontal - inputController.verticalCameraMovement;
            lookAmountHorizontal = Mathf.Clamp(lookAmountHorizontal, minAngle, maxAngle);
            cameraRot = Vector3.zero;
            cameraRot.y = lookAmountVertical;
            targetRot = Quaternion.Euler(cameraRot);
            targetRot = Quaternion.Slerp(transform.rotation, targetRot, cameraSmoothTime);
            transform.rotation = targetRot;


            cameraRot = Vector3.zero;
            cameraRot.x = lookAmountHorizontal;
            targetRot = Quaternion.Euler(cameraRot);
            targetRot = Quaternion.Slerp(cameraPivot.transform.localRotation, targetRot, cameraSmoothTime);
            cameraPivot.transform.localRotation = targetRot;
        }

       
    }

}
