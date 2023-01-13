using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueController : MonoBehaviour
{
    public TextAsset taskText;
    public GameObject talkUI;
    public bool fristOnTrigger = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (fristOnTrigger)
        {
            PlayerUIController playerUIController = other.GetComponent<PlayerUIController>();
            playerUIController.GetTextFromFile(taskText);
            fristOnTrigger = false;
        }else
        {

        }
    }
}
