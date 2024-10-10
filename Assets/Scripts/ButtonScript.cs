using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonScript: MonoBehaviour
{
    public Button uiButton; 
    public LoadSprites imageLoader;
    private Image buttonImage;

    private float fadeDuration = 1.0f; 
    private bool isFadingIn = true, buttonIsPressed;

    private void Start()
    {
        buttonImage = uiButton.GetComponent<Image>(); 
    }

    public void ButtonPressed()
    {
        if (!buttonIsPressed)
        {
            imageLoader.Load();
            StartCoroutine(FadeLoop());
            buttonIsPressed = true;
        } 
    }

    IEnumerator FadeLoop()
    {
        while (true)
        {
            float startAlpha = isFadingIn ? 0f : 1f;
            float endAlpha = isFadingIn ? 1f : 0f;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);

                Color color = buttonImage.color;
                color.a = newAlpha;
                buttonImage.color = color;

                yield return null;
            }

            isFadingIn = !isFadingIn;
        }
    }
}
