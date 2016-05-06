using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class JoinpadConnectionPanel : MonoBehaviour
{
    public ClientController clientController;
    public GameObject target, timeoutPanel, disconnectCanvas;
    public float timeoutPanelDuration;

    public void Awake()
    {
        clientController.connectingEvent += OnConnecting;
    }

    public void OnConnecting()
    {
        target.SetActive(true);
        StartCoroutine(ConnectingEnd());
    }

    public IEnumerator ConnectingEnd()
    {
        yield return new WaitUntil(clientController.IsNotConnecting);
        target.SetActive(false);
        timeoutPanel.SetActive(true);
        StartCoroutine(TimeoutPanelTimer());
    }

    public IEnumerator TimeoutPanelTimer()
    {
        yield return new WaitForSeconds(timeoutPanelDuration);
        Destroy(clientController.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}