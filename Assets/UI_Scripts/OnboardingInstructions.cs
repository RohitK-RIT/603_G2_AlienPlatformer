using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnboardingInstructions : MonoBehaviour
{
    // Define the sequential instructions
    [SerializeField] public string[] instructions;
    private int currentInstructionIndex = 0;
    private bool instructionsActive = false;
    [SerializeField] public TMPro.TextMeshProUGUI instructionsText;
    [SerializeField] public GameObject onboardingPanel;

    // Start is called before the first frame update
    void Start()
    {
        // Start coroutine to display instructions
        StartCoroutine(DisplayInstructions());
    }

    IEnumerator DisplayInstructions()
    {
        // Display first instruction
        instructionsText.text = instructions[currentInstructionIndex];
        Debug.Log(instructions[currentInstructionIndex]);

        // Display instructions until all instructions are completed
        while (currentInstructionIndex < instructions.Length)
        {
            // Wait for the player to press the correct key for the current instruction
            yield return StartCoroutine(WaitForInput());

            // Move to the next instruction
            currentInstructionIndex++;

            // If there are more instructions, update the instruction text
            if (currentInstructionIndex < instructions.Length)
            {
                instructionsText.text = instructions[currentInstructionIndex];
                Debug.Log(instructions[currentInstructionIndex]);
            }
            else
            {
                // If all instructions have been executed, deactivate the onboarding panel
                Debug.Log("All instructions completed");
                onboardingPanel.SetActive(false);
                break;
            }
        }
    }

    IEnumerator WaitForInput()
    {
        bool keyPressed = false;
        while (!keyPressed)
        {
            if(currentInstructionIndex < instructions.Length)
            {
                if ((Input.GetKeyDown(KeyCode.A) && instructions[currentInstructionIndex].Contains("The aliens have infiltrated the human space station orbiting the planet, determined to fight back and liberate their home. Use the \"A\" Key to Move Left")))
                {
                    keyPressed = true;
                }
                else if ((Input.GetKeyDown(KeyCode.D) && instructions[currentInstructionIndex].Contains("and \"D\" Key to maneuver right through the station's intricate corridors.")))
                {
                    keyPressed = true;
                }
                else if ((Input.GetKeyDown(KeyCode.W) && instructions[currentInstructionIndex].Contains("Press \"W'' to leap over obstacles that lie in their path. Your mission is to guide a resourceful robot ally, a key figure in their resistance, navigating through a maze of challenges to reach the finish line. ")))
                {
                    keyPressed = true;
                }
                else if ((Input.GetKeyDown(KeyCode.Space) && instructions[currentInstructionIndex].Contains("Each alien possesses the extraordinary capability to manipulate gravity at will, a skill pivotal to their quest for liberation. By pressing \"Space Bar,\" players can alter the direction")))
                {
                    keyPressed = true;
                }
            }
            else
            {
                keyPressed = true;
            }
            // Check if the player presses the correct key for the current instruction
            //if ((Input.GetKeyDown(KeyCode.A) && instructions[currentInstructionIndex] == "The aliens have infiltrated the human space station orbiting the planet, determined to fight back and liberate their home. Use the \"A\" Key to Move Left") ||
            //    (Input.GetKeyDown(KeyCode.D) && instructions[currentInstructionIndex] == "and \"D\" Key to maneuver right through the station's intricate corridors.") ||
            //    (Input.GetKeyDown(KeyCode.W) && instructions[currentInstructionIndex] == "Press \"W'' to leap over obstacles that lie in their path. Your mission is to guide a resourceful robot ally, a key figure in their resistance, navigating through a maze of challenges to reach the finish line. ") ||
            //    (Input.GetKeyDown(KeyCode.Space) && instructions[currentInstructionIndex] == "Each alien possesses the extraordinary capability to manipulate gravity at will, a skill pivotal to their quest for liberation. By pressing \"Space Bar,\" players can alter the direction"))
            //{
            //    yield break;
            //}
            yield return null; // Wait for next frame
        }
    }
}
