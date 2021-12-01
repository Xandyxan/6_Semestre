using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TestBossPuzzleConclusion : MonoBehaviour
{
    [Header("Camera focus on Boss")]
    [SerializeField] CinemachineVirtualCamera santoDeathCam;

    [SerializeField] private RotationMechanismBase rotationMechanism;

    [Header("Santos")]
    [SerializeField] private Animator corpoPrincipalSanto;

    [SerializeField] private Animator duplicataSanto;

    void Start()
    {
        rotationMechanism.puzzleSolvedDelegate -= MataVeio;
        rotationMechanism.puzzleSolvedDelegate += MataVeio;
    }

    private void MataVeio()
    {
        GameManager.instance.removePlayerControlEvent();
        StartCoroutine(JoaquimDeath());
    }

    private IEnumerator JoaquimDeath()
    {
        santoDeathCam.Priority = 20;
        corpoPrincipalSanto.SetTrigger("Death");
        yield return new WaitForSeconds(0.8f);
        duplicataSanto.SetTrigger("Death");
        yield return new WaitForSeconds(2f);
        santoDeathCam.Priority = 5;
        GameManager.instance.returnPlayerControlEvent();
    }
}
