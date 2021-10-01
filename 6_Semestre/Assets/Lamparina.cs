using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamparina : MonoBehaviour
{
    [SerializeField] private GameObject lamparina;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            lamparina.SetActive(!lamparina.activeInHierarchy);
        }
    }
}
