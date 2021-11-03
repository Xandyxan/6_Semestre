using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetSceneStartPos : MonoBehaviour
{
    public bool funcionar;
    // Seta a posição inicial de um objeto dinamico entre cenas. Ex: player no inicio do cemitério ou na saida de uma das portas.
    private void Start()
    {
        if (funcionar)
        {
            //PlayerPrefs.SetFloat("PlayerStartPosX", transform.position.x);
            //PlayerPrefs.SetFloat("PlayerStartPosY", transform.position.y);
            //PlayerPrefs.SetFloat("PlayerStartPosZ", transform.position.z);

            //// move player to the correct position on the scene
            float x = PlayerPrefs.GetFloat("PlayerStartPosX", transform.position.x);
            float y = PlayerPrefs.GetFloat("PlayerStartPosY", transform.position.y);
            float z = PlayerPrefs.GetFloat("PlayerStartPosZ", transform.position.z);

            transform.position = new Vector3(x, y, z);

            Debug.LogWarning("Funcionou");
        }
       
    }
}
