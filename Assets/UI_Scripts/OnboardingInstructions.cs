using System.Collections;
using UnityEngine;

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
        var keyPressed = false;
        while (!keyPressed)
        {
            if (currentInstructionIndex < instructions.Length)
            {
                var xAxis = Input.GetAxis("Horizontal");
                var yAxis = Input.GetAxis("Vertical");

                if (xAxis < 0 && instructions[currentInstructionIndex]
                        .Contains("Use the \"A & D\" Key for Movement."))
                {
                    keyPressed = true;
                }
                else if (yAxis > 0 && instructions[currentInstructionIndex]
                             .Contains("Press \"W'' to Jump."))
                {
                    keyPressed = true;
                }
                else if (Input.GetKeyDown(KeyCode.Space) && instructions[currentInstructionIndex]
                             .Contains("Use \"Space Bar\" to Switch Gravity."))
                {
                    keyPressed = true;
                }
            }
            else
            {
                keyPressed = true;
            }

            yield return null; // Wait for next frame
        }
    }
}