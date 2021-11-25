using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    [SerializeField] private float desireY;

    private void Awake()
    {
        StartCoroutine(MoveUpFunction());
    }

    private IEnumerator MoveUpFunction()
    {
        float number = transform.localPosition.z;

        while(transform.localPosition.z != desireY)
        {
            number = Mathf.Lerp(number, desireY, Time.deltaTime * 0.5f);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, number);
            yield return null;
        }

        yield return null;
    }
}
