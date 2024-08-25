using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [Tooltip("Higher amount means higher movement when camera moves")]
    [Range(0f, 1f)]
    [SerializeField] float parallaxAmount;

    GameObject cam;

    private void Start()
    {
        cam = Camera.main.gameObject;
    }

    private void LateUpdate()
    {
        Vector3 camPos = cam.transform.position;

        transform.position = new Vector3(camPos.x * parallaxAmount, camPos.y * parallaxAmount, transform.position.z);
    }
}
