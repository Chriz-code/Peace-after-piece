using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform leftBorder = null, rightBorder = null;
    [SerializeField] Transform target = null;
    float zPos = -10;
    private void Start()
    {
        zPos = transform.position.z;
    }
    void FixedUpdate()
    {
        Vector3 pos = Vector3.zero;
        if(target)
            pos = target.position;
        pos.z = zPos;
        pos.y = transform.position.y;

        if (leftBorder && rightBorder)
        {
            float horz = Camera.main.orthographicSize * Screen.width / Screen.height;


            float minX = leftBorder.position.x + horz;
            float maxX = rightBorder.position.x - horz;

            if (pos.x < minX)
                pos.x = minX;
            if (pos.x > maxX)
                pos.x = maxX;
        }

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
