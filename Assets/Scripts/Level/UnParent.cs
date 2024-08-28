using UnityEngine;

public class UnParent : MonoBehaviour
{
    void Start()
    {
        transform.SetParent(null);
    }
}
