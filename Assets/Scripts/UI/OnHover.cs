#region

using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

#endregion

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    //private RectTransform m_rectTransform;
    //private Button m_button;
    [SerializeField] private float m_scaleValue = 1.15f;
    [SerializeField] private float m_scaleDuration = 0.25f;
    [SerializeField] private Ease m_easing;
    private Tween m_tweener;

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound("SFX_UIHover");
        m_tweener = transform.DOScale(Vector3.one * m_scaleValue, m_scaleDuration).SetEase(m_easing);
        m_tweener.SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_tweener = transform.DOScale(Vector3.one, m_scaleDuration);
        m_tweener.SetUpdate(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound("SFX_UIClick");
    }

    public void OnSelect(BaseEventData eventData)
    {
        m_tweener = transform.DOScale(Vector3.one * m_scaleValue, m_scaleDuration).SetEase(m_easing);
        m_tweener.SetUpdate(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        m_tweener = transform.DOScale(Vector3.one, m_scaleDuration);
        m_tweener.SetUpdate(true);
    }
}