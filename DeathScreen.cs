using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathScreen : MonoBehaviour
{
    public static DeathScreen Instance { get; private set; }
    [SerializeField] private GameObject red;
    private bool didStart = false;
    private void Start()
    {
        Instance = this;
    }
    public void Die()
    {
        red.SetActive(true);
        if (!didStart)
            StartCoroutine(Change());
    }
    private IEnumerator Change()
    {
        didStart = true;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
