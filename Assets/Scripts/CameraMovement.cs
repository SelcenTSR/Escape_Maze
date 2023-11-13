using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] InputController inputController;
    [SerializeField] GameObject cameraPivot;
    [SerializeField] GameObject player;
    // Start is called before the first frame update

    float lookAmountVertical;
    float lookAmountHorizontal;
    float maxAngle = 15f;
    float minAngle = -15;
    Vector3 cameraFollowVelocity=Vector3.zero;
    Vector3 targetPos;
    Vector3 cameraRot;
    Quaternion targetRot;
    float cameraSmoothTime = .2f;

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
        targetPos = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraFollowVelocity, cameraSmoothTime * Time.deltaTime);
        transform.position = targetPos;
    }

    void CameraRotations()
    {
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
