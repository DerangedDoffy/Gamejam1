using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseGravity : MonoBehaviour
{
    void Start()
    {
        Physics.gravity = new Vector3(0, -20, 0);
    }
}
