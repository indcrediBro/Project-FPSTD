using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    [SerializeField] private Image m_flashImage;
    [SerializeField] private Color m_flashColor = Color.red;
    [SerializeField] private Color m_fadeColor = Color.white;
    [SerializeField] private float m_fadeDuration = 1f;

    void Start()
    {
        if (m_flashImage != null)
        {
            m_flashImage.color = m_fadeColor;
            m_flashImage.DOFade(0, m_fadeDuration);
        }
    }

    public void FadeOut()
    {
        m_flashImage.color = m_fadeColor;
        m_flashImage.DOFade(1f, m_fadeDuration);
    }

    public void TriggerFlash(float _flashDuration = 0.2f)
    {
        StartCoroutine(FlashCoroutine(_flashDuration));
    }

    private IEnumerator FlashCoroutine(float _flashDuration)
    {
        float elapsedTime = 0f;
        m_flashImage.color = new Color(m_flashColor.r, m_flashColor.g, m_flashColor.b, 1);

        while (elapsedTime < _flashDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / _flashDuration);
            m_flashImage.color = new Color(m_flashColor.r, m_flashColor.g, m_flashColor.b, alpha);
            yield return null;
        }

        m_flashImage.color = new Color(m_flashColor.r, m_flashColor.g, m_flashColor.b, 0);
    }
}
