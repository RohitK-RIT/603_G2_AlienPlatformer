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
                        .Contains(
                            "The aliens have infiltrated the human space station orbiting the planet, determined to fight back and liberate their home. Use the \"A\" Key to Move Left"))
                {
                    keyPressed = true;
                }
                else if (xAxis > 0 &&
                         instructions[currentInstructionIndex].Contains("and \"D\" Key to maneuver right through the station's intricate corridors."))
                {
                    keyPressed = true;
                }
                else if (yAxis > 0 && instructions[currentInstructionIndex]
                             .Contains(
                                 "Press \"W'' to leap over obstacles that lie in their path. Your mission is to guide a resourceful robot ally, a key figure in their resistance, navigating through a maze of challenges to reach the finish line. "))
                {
                    keyPressed = true;
                }
                else if (Input.GetKeyDown(KeyCode.Space) && instructions[currentInstructionIndex]
                             .Contains(
                                 "Each alien can manipulate gravity at will, a crucial skill for their mission of liberation. By pressing \"Space Bar\" players can change gravity's direction, allowing aliens to traverse ceilings and vertical surfaces, bypassing obstacles and evading security measures."))
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