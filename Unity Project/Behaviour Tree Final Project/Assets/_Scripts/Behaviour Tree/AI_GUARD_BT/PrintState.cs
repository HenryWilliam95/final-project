using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintState : MonoBehaviour
{
    public Text textToDisplay;
    public Guardblackboard guardblackboard;

    private void Update()
    {
        // For convinence print the state of the guards to the screen
        textToDisplay.text = guardblackboard.name + " state " + guardblackboard.GuardStateOutput();
    }
}
