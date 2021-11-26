using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDesactive : MonoBehaviour
{
    [SerializeField] private float time;

    private void Awake()
    {
        Invoke("Desactive", time);
    }

    private void Desactive()
    {
        this.gameObject.SetActive(false);
    }
}
