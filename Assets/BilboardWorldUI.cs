using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardWorldUI : MonoBehaviour
{
    Camera camera;
    private void Start()
    {
        camera = Camera.main;
    }
    private void LateUpdate()
    {
        if (camera != null)
        {
            transform.LookAt(camera.transform.position + camera.transform.forward);
        }
    }
}
