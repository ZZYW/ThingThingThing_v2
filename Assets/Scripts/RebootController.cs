using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RebootController : MonoBehaviour
{
    public int minuteTillReboot = 2;
    public bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cycle());
    }

    IEnumerator Cycle()
    {
        while (active)
        {
            yield return new WaitForSeconds(minuteTillReboot * 60);
            SceneManager.LoadScene(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            SceneManager.LoadScene(0);
        }
    }
}
