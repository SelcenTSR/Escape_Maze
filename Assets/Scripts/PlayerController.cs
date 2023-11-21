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
    public bool isAiming;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleRotation();
    }
    private void Update()
    {
        isAiming = animator.GetBool("isAiming");
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
