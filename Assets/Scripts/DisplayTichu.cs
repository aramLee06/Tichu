using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayTichu : MonoBehaviour
{
	public Image tichu, grand, grandGreat, great, decoration;
    private Transform tichuTransf, grandTransf, grandGreatTransf, greatTransf, decorationTransf;
    public AnimationCurve inOutCurve = new AnimationCurve(new Keyframe(0,0), new Keyframe(1,1));
    public float beginX, endX, subBeginX, subEndX;
    public float duration;
    public float time;

    public TichuType lastType;

    private void Start()
    {
        tichuTransf = tichu.transform;
        grandTransf = grand.transform;
        grandGreatTransf = grandGreat.transform;
        greatTransf = great.transform;
        decorationTransf = decoration.transform;
    }

    private void Update()
    {
        time = Mathf.Clamp01(time + Time.deltaTime/duration * (lastType == TichuType.NONE ? -1 : 1));
        float lerpPos = inOutCurve.Evaluate(time);

        switch (lastType)
        {
            case TichuType.TICHU:
                tichuTransf.localPosition = new Vector2(Mathf.Lerp(beginX, endX, lerpPos), tichuTransf.localPosition.y);
                break;
            case TichuType.GRAND_TICHU:
                grandTransf.localPosition = new Vector2(Mathf.Lerp(subBeginX,subEndX,lerpPos), grandTransf.localPosition.y);
                goto case TichuType.TICHU;
            default:
                greatTransf.localPosition = new Vector2(Mathf.Lerp(subBeginX, subEndX, lerpPos), greatTransf.localPosition.y);
                grandGreatTransf.localPosition = new Vector2(Mathf.Lerp(subBeginX, subEndX, lerpPos), grandGreatTransf.localPosition.y);
                decorationTransf.localPosition = new Vector2(Mathf.Lerp(subBeginX, subEndX, lerpPos), decorationTransf.localPosition.y);
                goto case TichuType.GRAND_TICHU;
        }
    }

	public void Display (TichuType type = TichuType.NONE)
	{
		//Debug.Log ("I get called");
        if (type != TichuType.NONE)
            lastType = type;

		switch (type)
		{
			case TichuType.TICHU:
				tichu.enabled = true;
				break;
			case TichuType.GRAND_TICHU:
				tichu.enabled = true;
				grand.enabled = true;
				break;
			case TichuType.GRAND_GRAND_TICHU:
				tichu.enabled = true;
				grandGreat.enabled = true;
				great.enabled = true;
				decoration.enabled = true;
				break;
			default:
				tichu.enabled = false;
				great.enabled = false;
				grand.enabled = false;
				grandGreat.enabled = false;
				decoration.enabled = false;
				break;
		}
	}
}