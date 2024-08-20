#region

using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#endregion

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform m_rectTransform;
    [SerializeField] private Button m_button;
    [SerializeField] private float m_scaleValue;
    [SerializeField] private float m_scaleDuration;
    private Tween m_tweener;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_tweener = m_rectTransform.DOScale(Vector3.one * m_scaleValue, m_scaleDuration);
        m_tweener.SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_tweener = m_rectTransform.DOScale(Vector3.one, m_scaleDuration);
        m_tweener.SetUpdate(true);
    }
}