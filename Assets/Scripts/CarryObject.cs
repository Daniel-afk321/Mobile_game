using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour
{
    public GameObject pickAbleObj;
    public Transform handPlayer;

    public float range = 2f;

    public Camera Camera;

    public GameObject pickUpButton;
    public GameObject dropButton;

    private void Update()
    {
        StartPickUpObject();
    }

    public void StartPickUpObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, range))
        {
            pickUpButton.SetActive(true);
        }
        else
        {
            pickUpButton.SetActive(false);
        }
    }

    public void PickUpObject()
    {
        pickAbleObj.transform.SetParent(handPlayer);
        pickUpButton.SetActive(false);
        dropButton.SetActive(true);
    }

    public void DropButton()
    {
        handPlayer.DetachChildren();
        dropButton.SetActive(false);
    }
}
