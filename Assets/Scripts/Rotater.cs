using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private int rotateSpeed = 30;
    [SerializeField] private int upDownSpeed = 30;
    [SerializeField] private float upDownAmount = 1;

    private void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
        transform.position += Vector3.up * Mathf.Sin(Time.time * upDownSpeed) * upDownAmount * Time.deltaTime;
    }
}
