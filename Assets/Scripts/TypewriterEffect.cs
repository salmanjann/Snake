using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text part1; 
    public TMP_Text part2; 

    public float typingSpeed = 0.05f; // Speed of typing (seconds per character)
    public float loopDelay = 2f; // Delay in seconds before restarting the animation
    public bool shouldLoop = false;

    private string part1Text, part2Text;

    public Slider left;
    public Slider right;


    void Start()
    {
        // Store the full text from each part
        part1Text = part1.text;
        part2Text = part2.text;

        // Start the typewriter effect
        StartCoroutine(TypewriterLoop());
    }

    IEnumerator TypewriterLoop()
    {
        do
        {
            // Clear text initially
            part1.text = "";
            part2.text = "";

            // Animate part 1 (Snak)
            for (int i = 0; i < part1Text.Length; i++)
            {
                yield return new WaitForSeconds(typingSpeed);
                part1.text += part1Text[i];
                part2.text += part2Text[i];
            }

            // Wait for the loop delay
            yield return new WaitForSeconds(loopDelay);
        }while(shouldLoop);
    }

    public void buttonClick(){
        Debug.Log("Button Clicked");
    }

    public void leftSlider(){
        right.value = left.value;
    }

    public void rightSlider(){
        left.value = right.value;
    }
}
