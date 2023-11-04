using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private float offsetX = 0;
    [SerializeField] private float offsetY = 0;
    [SerializeField, Range(0, 1)] private float damping = 0;

    private Transform cameraTransform;
    private float cameraZ;

    private void Awake()
    {
        cameraTransform = transform;
        cameraZ = cameraTransform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        float dx = player.position.x - cameraTransform.position.x;
        float dy = player.position.y - cameraTransform.position.y;

        float x = cameraTransform.position.x + dx * (1 - damping);
        float y = cameraTransform.position.y + dy * (1 - damping);

        cameraTransform.position = new Vector3(x + offsetX, y + offsetY, cameraZ);
    }
}
