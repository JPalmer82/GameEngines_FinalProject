using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject Door;
    private GameObject Boss;



    private void Start()
    {
        Boss = GameObject.FindGameObjectWithTag("Boss");
        Debug.Log(Boss.gameObject);
    }

    void Update()
    {
        if (Boss == null)
        {
            Destroy(Door);
        }
    }
}
