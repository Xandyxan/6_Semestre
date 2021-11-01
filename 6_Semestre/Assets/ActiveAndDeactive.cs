using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAndDeactive : MonoBehaviour
{
    private void Awake()
    {
        if (!this.isActiveAndEnabled) this.gameObject.SetActive(true);
    }
    private void Start()
    {
        Invoke("Deactive", 0.5f);
    }

    private void Deactive()
    {
        this.gameObject.SetActive(false);
    }
}
