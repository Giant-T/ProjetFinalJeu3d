using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y + 180, 0f);
    }
}
