using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatyIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private float m_floatDuration = 1.15f;
    [SerializeField] private Transform m_floatTarget;
    [SerializeField] private Ease m_easing;

    private Vector3 m_localRotation;
    private Tween m_tweener;

    private void Awake()
    {
        m_localRotation = m_floatTarget.localRotation.eulerAngles;
    }

    private void OnDisable()
    {
        m_tweener.Complete();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShopUIManager.Instance.MoveShopItemToBottom(transform);
        m_tweener = m_floatTarget.DOBlendablePunchRotation(Vector3.forward * 5, m_floatDuration).SetEase(m_easing).OnComplete(() =>
        m_floatTarget.DOBlendablePunchRotation(Vector3.forward * -5, m_floatDuration).SetEase(m_easing));
        m_tweener.SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_tweener = m_floatTarget.DOBlendableLocalRotateBy(m_localRotation, m_floatDuration, RotateMode.Fast).SetEase(m_easing);
        m_tweener.SetUpdate(true);
    }

    public void OnSelect(BaseEventData eventData)
    {
        ShopUIManager.Instance.MoveShopItemToBottom(transform);
        m_tweener = m_floatTarget.DOBlendablePunchRotation(Vector3.forward * 2, m_floatDuration).SetEase(m_easing).OnComplete(() =>
        m_floatTarget.DOBlendablePunchRotation(Vector3.forward * -2, m_floatDuration).SetEase(m_easing));
        m_tweener.SetUpdate(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        m_tweener = m_floatTarget.DOBlendableLocalRotateBy(m_localRotation, m_floatDuration, RotateMode.Fast).SetEase(m_easing);
        m_tweener.SetUpdate(true);
    }
}
