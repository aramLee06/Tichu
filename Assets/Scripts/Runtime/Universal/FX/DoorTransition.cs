using UnityEngine;
using System.Collections;

public class DoorTransition : MonoBehaviour
{
    public static DoorTransition transition;

    public Transform doorLeft, doorRight;
    public AnimationCurve doorPosition;
    public float width;
    private float time = 0;
    public bool closed;
    public int nextScene;
    public string nextSceneName;

    public bool useName;

    public void Start()
    {
        if (transition)
            Destroy(gameObject);

        transition = this;

        DontDestroyOnLoad(gameObject);
    }

    public void Close(string nextScene)
    {
        nextSceneName = nextScene;
		doorLeft.GetComponent<Animator> ().SetTrigger ("Close");
		doorRight.GetComponent<Animator> ().SetTrigger ("Close");
        useName = true;
        closed = true;
    }

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