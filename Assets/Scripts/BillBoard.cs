using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Camera cam;

    private void Start() {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f);
    }
}
