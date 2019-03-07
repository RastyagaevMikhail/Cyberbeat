using FluffyUnderware.Curvy;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ParentMover : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Vector3 direction = Vector3.forward;

    void Update ()
    {
        rb.MovePosition (transform.position + Time.deltaTime * direction);
        // rb.velocity = (direction);
    }
}
