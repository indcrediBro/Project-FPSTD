using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    [SerializeField] private Image flashImage;
    [SerializeField] private Color flashColor = Color.red;

    void Start()
    {
        if (flashImage != null)
        {
            flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0);
        }
    }

    public void TriggerFlash(float flashDuration = 0.2f)
    {
        //if (SettingsManager.Instance.screenFlashEnabled)
        {
            StartCoroutine(FlashCoroutine(flashDuration));
        }
    }

    private IEnumerator FlashCoroutine(float flashDuration)
    {
        float elapsedTime = 0f;
        flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 1);

        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / flashDuration);
            flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
            yield return null;
        }

        flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0);
    }
}
