using UnityEngine;
using System.Collections;

/// <summary>
/// Door transition.
/// </summary>
public class DoorTransition : MonoBehaviour
{
	/// <summary>
	/// Singleton
	/// </summary>
    public static DoorTransition transition;

	/// <summary>
	/// The doors
	/// </summary>
    public Transform doorLeft, doorRight;
	/// <summary>
	/// The door position curve.
	/// </summary>
    public AnimationCurve doorPosition;
	/// <summary>
	/// The width of a single door.
	/// </summary>
    public float width;
	/// <summary>
	/// The time.
	/// </summary>
    private float time = 0;
	/// <summary>
	/// Is it closed?
	/// </summary>
    public bool closed;
	/// <summary>
	/// The next scene id.
	/// </summary>
    public int nextScene;
	/// <summary>
	/// The name of the next scene.
	/// </summary>
    public string nextSceneName;

	/// <summary>
	/// Does it use the sceneś name, rather than the id?.
	/// </summary>
    public bool useName;

    public void Start()
    {
        if (transition)
            Destroy(gameObject);

        transition = this;

        DontDestroyOnLoad(gameObject);
    }

	/// <summary>
	/// Close to the specified nextScene.
	/// </summary>
	/// <param name="nextScene">Next scene.</param>
    public void Close(string nextScene)
    {
        nextSceneName = nextScene;
		doorLeft.GetComponent<Animator> ().SetTrigger ("Close");
		doorRight.GetComponent<Animator> ().SetTrigger ("Close");
        useName = true;
        closed = true;
    }

	/// <summary>
	/// Close to the specified buildIndex.
	/// </summary>
	/// <param name="buildIndex">Build index.</param>
    public void Close(int buildIndex)
    {
        this.nextScene = buildIndex;
        useName = false;
        closed = true;
    }

    public void FixedUpdate()
    {
        Vector3 pL = doorLeft.localPosition, pR = doorRight.localPosition;

        float evaluatedPosition = doorPosition.Evaluate(time);
        pL.x = width * evaluatedPosition;
        pR.x = -width * evaluatedPosition;

        doorLeft.localPosition = pL;
        doorRight.localPosition = pR;

        time = Mathf.Clamp01(closed ? time - Time.deltaTime : time + Time.deltaTime);
        if (closed && time == 0)
        {
            if(useName)
                UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
            else
                UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);

            closed = false;

			doorLeft.GetComponent<Animator> ().SetTrigger ("Open");
			doorRight.GetComponent<Animator> ().SetTrigger ("Open");
        }
    }
}

/// <summary>
/// Rerouted SceneManager to activate the DoorTransition
/// </summary>
public class SceneManager
{
    public static void LoadScene(string sceneName)
    {
        DoorTransition.transition.Close(sceneName);
    }

    public static void LoadScene(int buildIndex)
    {
        DoorTransition.transition.Close(buildIndex);
    }

    public static UnityEngine.SceneManagement.Scene GetActiveScene()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene();
    }
        
}