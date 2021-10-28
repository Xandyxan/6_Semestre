using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterTime : MonoBehaviour
{
    [SerializeField] float timeToDeactivate;

    private void OnEnable()
    {
        StartCoroutine(Deactivate(timeToDeactivate));
    }

    private IEnumerator Deactivate(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}
