using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public int textOption;
    [SerializeField] TextMeshProUGUI TutText;


    public string returnText()
    {
        if (textOption == 0)
        {
            return "WASD to Move";
        }
        else if (textOption == 1)
        {
            return "Hold Shift to Sprint";
        }
        else if (textOption == 2)
        {
            return "Hold C to Crouch";
        }
        else if (textOption == 3)
        {
            return "Hold Ctrl to slide";
        }
        else if (textOption == 4)
        {
            return "Run at the wall to climb it. Beware you cant climb Ice.";
        }
        else if (textOption == 5)
        {
            return "Press Space to jump";
        }
        else if (textOption == 6)
        {
            return "Click to shoot and freeze the enemy. Then jump on him";
        }
        else if (textOption == 7)
        {
            return "Good! Now make the next jump";
        }
        else if (textOption == 8)
        {
            return "Jump off the jumppad to go higher";
        }
        else if (textOption == 9)
        {
            return "Ice is slippery. Run to the finish line!";
        }
        else return " ";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") {
            TutText.text = returnText();
        }

    }
    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Leaving Collider");
        TutText.text = " ";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
