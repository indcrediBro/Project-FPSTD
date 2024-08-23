using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Coin : Interactable
{
    [SerializeField] private Rigidbody m_rigidbody;
    private bool m_isCollected;
    private void Start()
    {
        if(!m_rigidbody) m_rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        OnObjectSpawn();
    }

    private void OnDisable()
    {
        OnObjectDespawn();
    }

    public void OnObjectSpawn()
    {
        m_isCollected = false;
        Vector3 randomDirection = new Vector3(RandomNumber.Instance.NextFloat(-1f, 1f), 1f, RandomNumber.Instance.NextFloat(-1f, 1f)).normalized;
        float randomForce = RandomNumber.Instance.NextFloat(5f, 10f);
        m_rigidbody.AddForce(randomDirection * randomForce, ForceMode.Impulse);
    }

    public void OnObjectDespawn()
    {
        m_rigidbody.velocity = Vector3.zero;
    }

    public override void Interact()
    {
        if(!m_isCollected && gameObject.activeInHierarchy) StartCoroutine(CollectCoinCO());
    }

    private IEnumerator CollectCoinCO()
    {
        m_isCollected = true;
        transform.DOMoveY(transform.position.y + 1f, 1f);
        transform.DOLocalRotate(new Vector3(0f, 360f*2, 0f), 2f);
        yield return new WaitForSeconds(1f);
        transform.DOMove(GameReferences.Instance.m_PlayerStats.transform.position, 2f);
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        EconomyManager.Instance.EarnMoney(1);
    }
}
