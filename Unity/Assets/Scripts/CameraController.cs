using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target = null;
    float zPos = -10;
    private void Start()
    {
        zPos = transform.position.z;
    }
    void Update()
    {
        Vector3 pos = Vector3.zero;
        if(target)
            pos = target.position;
        pos.z = zPos;
        pos.y = transform.position.y;
        transform.position = pos;
    }

    private void OnEnable()
    {
        if (TryGetComponent<Camera>(out Camera camera))
            camera.enabled = true;
    }
    private void OnDisable()
    {
        if (TryGetComponent<Camera>(out Camera camera))
            camera.enabled = false;
    }
}
