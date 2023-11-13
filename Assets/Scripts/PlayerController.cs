using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputController inputController;
    public Transform cameraHolderTransform;
    public float rotationSpeed = 3.5f;
    public Rigidbody playerRigidbody;
    Quaternion targetRotation;
    Quaternion playerRotation;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleRotation();
    }

    public void HandleRotation()
    {
        targetRotation = Quaternion.Euler(0, cameraHolderTransform.eulerAngles.y, 0);
        playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (inputController.verticalMovementInput != 0 || inputController.horizontalMovementInput != 0)
        {
            transform.rotation = playerRotation;
        }
    }


  
}
