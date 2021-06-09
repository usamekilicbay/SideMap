using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthRotator : MonoBehaviour
{
    [Range(1f, 100f)]
    [SerializeField] private float rotationSpeed = 10f;

    private void Update()
    {
        SelfRotate();
    }

    private void SelfRotate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
}
