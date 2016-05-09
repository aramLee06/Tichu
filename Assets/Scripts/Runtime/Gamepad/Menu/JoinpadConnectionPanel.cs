using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Joinpad connection panel.
/// </summary>
public class JoinpadConnectionPanel : MonoBehaviour
{
	/// <summary>
	/// The client controller.
	/// </summary>
    public ClientController clientController;
	/// <summary>
	/// Various objects
	/// </summary>
    public GameObject target, timeoutPanel, disconnectCanvas;
	/// <summary>
	/// The duration of the timeout panel.
	/// </summary>
    public float timeoutPanelDuration;

    public void Awake()
    {
        clientController.connectingEvent += OnConnecting;
    }

	/// <summary>
	/// Raises the connecting event.
	/// </summary>
    public void OnConnecting()
    {
        target.SetActive(true);
        StartCoroutine(ConnectingEnd());
    }

	/// <summary>
	/// Timeout
	/// </summary>
    public IEnumerator ConnectingEnd()
    {
        yield return new WaitUntil(clientController.IsNotConnecting);
        target.SetActive(false);
        timeoutPanel.SetActive(true);
        StartCoroutine(TimeoutPanelTimer());
    }

	/// <summary>
	/// Timeout panel duration.
	/// </summary>
    public IEnumerator TimeoutPanelTimer()
    {
        yield return new WaitForSeconds(timeoutPanelDuration);
        Destroy(clientController.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}