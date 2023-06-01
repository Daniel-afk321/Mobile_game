using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAbleObj : MonoBehaviour
{
    public GameObject DropBtn;

    private Rigidbody rbObj;

    private void Start()
    {
        rbObj = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (DropBtn.activeInHierarchy)
        {
            rbObj.isKinematic = true;
        }
        else
        {
            rbObj.isKinematic = false;
        }
    }
}
