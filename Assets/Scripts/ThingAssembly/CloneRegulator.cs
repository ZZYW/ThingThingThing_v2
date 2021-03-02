using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneRegulator : MonoBehaviour
{
    public float cloneMinimalInterval = 1f;
    public bool canClone { get; private set; }

    public static CloneRegulator instance = null;
    public void Cloned()
    {
        canClone = false;
        StartCoroutine(ReTrue());
    }

    IEnumerator ReTrue()
    {
        yield return new WaitForSeconds(cloneMinimalInterval);
        canClone = true;
    }

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        canClone = true;
    }
}
