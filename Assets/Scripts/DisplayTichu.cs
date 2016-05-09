using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// Display tichu.
/// </summary>
public class DisplayTichu : MonoBehaviour
{
	/// <summary>
	/// The images
	/// </summary>
	public Image tichu, grand, grandGreat, great, decoration;
	/// <summary>
	/// The image transforms
	/// </summary>
	private Transform tichuTransf, grandTransf, grandGreatTransf, greatTransf, decorationTransf;
	/// <summary>
	/// The in/out curve.
	/// </summary>
	public AnimationCurve inOutCurve = new AnimationCurve(new Keyframe(0,0), new Keyframe(1,1));
	/// <summary>
	/// The begin and end x-positions
	/// </summary>
	public float beginX, endX, subBeginX, subEndX;
	/// <summary>
	/// The duration.
	/// </summary>
	public float duration;
	/// <summary>
	/// The time.
	/// </summary>
	public float time;
	/// <summary>
	/// The audio source.
	/// </summary>
	public AudioSource audioSource;
	/// <summary>
	/// The tichu VoiceOver effect audioclips.
	/// </summary>
	public AudioClip[] tichuVO;

	/// <summary>
	/// The last type.
	/// </summary>
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

	/// <summary>
	/// Display the specified Tichu type and plays the corresponding sound.
	/// </summary>
	/// <param name="type">Type.</param>
	public void Display (TichuType type = TichuType.NONE)
	{
		//Debug.Log ("I get called");
		if (type != TichuType.NONE)
			lastType = type;

		switch (type)
		{
			case TichuType.TICHU:
				tichu.enabled = true;
				if (!audioSource.isPlaying)
					audioSource.PlayOneShot (tichuVO [0]);
				break;
			case TichuType.GRAND_TICHU:
				tichu.enabled = true;
				grand.enabled = true;
				if (!audioSource.isPlaying)
					audioSource.PlayOneShot (tichuVO [1]);
				break;
			case TichuType.GRAND_GRAND_TICHU:
				tichu.enabled = true;
				grandGreat.enabled = true;
				great.enabled = true;
				decoration.enabled = true;
				if (!audioSource.isPlaying)
					audioSource.PlayOneShot (tichuVO [2]);
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