using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public bool Active;
    [HideInInspector]
    public Transform EndPostion;
    public bool TPed;
    public Vector3 RotationGoingIn;
    public Vector3 RelativePoint;
    public Vector3 Velocity;
    [HideInInspector]
    public GameObject ObjectIn;
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Active)
        {
            if (other.CompareTag("Player") || other.CompareTag("Prop"))
            {
                ObjectIn = other.gameObject;
                RotationGoingIn = -other.transform.eulerAngles;
                RelativePoint = transform.InverseTransformPoint(other.transform.position);
                Velocity = -transform.InverseTransformDirection(other.attachedRigidbody.velocity);
                other.transform.position = EndPostion.position;
                TPed = true;
            }
        }

    }
}

