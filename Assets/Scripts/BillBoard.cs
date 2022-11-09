using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, Camera.allCameras[0].transform.rotation.eulerAngles.y, 0f);
    }
}
