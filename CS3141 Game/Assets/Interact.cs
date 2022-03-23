using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public bool inRange = false;
    public KeyCode interactKey;
    public UnityEvent interact;
    [SerializeField] string message;
    [SerializeField] GameObject displayedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange) 
        {
            if(Input.GetKeyDown(interactKey)) //true if interact key (space) is pressed down
            {
                interact.Invoke(); //invoke event
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            inRange = true;
            ShowMessage(message);
            Debug.Log("Player in range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            ShowMessage("");
            Debug.Log("Player not in range");
        }
    }

    public void ShowMessage(string text)
    {
        if (displayedText)
        {
            displayedText.GetComponent<TextMesh>().text = text;
        }
    }
}
