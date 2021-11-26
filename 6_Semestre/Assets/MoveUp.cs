using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    [SerializeField] private float desireY;
    [SerializeField] private bool up;
    [SerializeField] private bool runAwake;

    private void Awake()
    {
        if(runAwake) StartCoroutine(MoveUpFunction());
    }

    private IEnumerator MoveUpFunction()
    {
        if (up)
        {
            float number = transform.localPosition.z;

            while (transform.localPosition.z != desireY)
            {
                number = Mathf.Lerp(number, desireY, Time.deltaTime * 0.5f);
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, number);
                yield return null;
            }
        }
        else
        {
            float number = transform.localPosition.y;

            while (transform.localPosition.y != desireY)
            {
                number = Mathf.Lerp(number, desireY, Time.deltaTime * 0.5f);
                transform.localPosition = new Vector3(transform.localPosition.x, number, transform.localPosition.z);
                yield return null;
            }
        }

        yield return null;
    }

    public void RunCoroutine()
    {
        StartCoroutine(MoveUpFunction());
    }
}
