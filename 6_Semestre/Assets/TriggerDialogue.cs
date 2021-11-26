using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{

    [SerializeField] private DialogueManager2 dialogue;
    [SerializeField] int dialogueIndex;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();

        if (playerStats)
        {
            dialogue.ExecuteDialogue(dialogueIndex);
           
        }
    }
}
