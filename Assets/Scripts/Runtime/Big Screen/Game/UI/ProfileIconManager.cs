using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// A script that manages a specific character's profile icon
/// </summary>
[RequireComponent(typeof(Image))]
public class ProfileIconManager : MonoBehaviour
{
	/// <summary>
	/// The character.
	/// </summary>
    public Character character;
	/// <summary>
	/// Is the avatar on the top half of the screen?
	/// </summary>
    public bool topAvatar;
	/// <summary>
	/// Various sprites for emotions
	/// </summary>
    private Sprite happy, neutral, sad;

	/// <summary>
	/// The flow lerp speed.
	/// </summary>
    [Range(0,1)] public float flowLerp = .1f;

	/// <summary>
	/// The transforms.
	/// </summary>
    public Transform upperTransform, lowerTransform;
	/// <summary>
	/// The card place scaling curve.
	/// </summary>
    public AnimationCurve cardPlaceScaleCurve;
	/// <summary>
	/// The initial scale.
	/// </summary>
    public Vector3 initialScale = Vector3.one;

	/// <summary>
	/// The animation time.
	/// </summary>
    public float animationTime = 1;
	/// <summary>
	/// The animation speed.
	/// </summary>
    public float animationSpeed = .1f;

	/// <summary>
	/// The profile images.
	/// </summary>
    private Image upperImage, lowerImage;

    private void Awake()
    {
        lowerImage = GetComponent<Image>();

        GameObject upperObject = new GameObject("Upper Image");
        upperTransform = upperObject.AddComponent<RectTransform>();
        upperTransform.SetParent(lowerTransform, false);
        upperImage = upperObject.AddComponent<Image>();

        ((RectTransform)upperTransform).anchorMin = Vector2.zero;
        ((RectTransform)upperTransform).anchorMax = Vector2.one;

        ((RectTransform)upperTransform).sizeDelta = Vector2.zero;

        upperTransform.gameObject.SetActive(false);

        happy = ProfileIconList.GetSprite(character, Emotion.HAPPY, !topAvatar);
        neutral = ProfileIconList.GetSprite(character, Emotion.NEUTRAL, !topAvatar);
        sad = ProfileIconList.GetSprite(character, Emotion.SAD, !topAvatar);

        lowerImage.sprite = neutral;
    }

    private void Update()
    {
        CardPlaceScaling();
        FlowBetweenEmotions();

        if (Input.GetKeyDown(KeyCode.Space))
            SetEmotion(Emotion.SAD);
    }

    /// <summary>
    /// Changes the Profile's emotion
    /// </summary>
    /// <param name="emotion">The new emotion</param>
    public void SetEmotion(Emotion emotion)
    {
        upperTransform.gameObject.SetActive(true);
        upperImage.sprite = lowerImage.sprite;
        upperImage.color = new Color(1, 1, 1, 1);
        switch(emotion)
        {
            case Emotion.HAPPY:
                lowerImage.sprite = happy;
                break;
            case Emotion.NEUTRAL:
                lowerImage.sprite = neutral;
                break;
            case Emotion.SAD:
                lowerImage.sprite = sad;
                break;
        }
    }

    /// <summary>
    /// Smoothens the transition between emotions
    /// </summary>
    private void FlowBetweenEmotions()
    {
        if (upperImage.gameObject.activeSelf)
        {
            if (upperImage.color.a > 0.1f)
            {
                upperImage.color = Color.Lerp(upperImage.color, new Color(1, 1, 1, 0), flowLerp);
            }
            else
            {
                upperImage.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Increases the profile icon's scaling when collected cards get near (during the trick end phase).
    /// </summary>
    private void CardPlaceScaling()
    {
        if (animationTime < 1)
        {
            animationTime += animationSpeed;
            animationTime = Mathf.Clamp01(animationTime);
        }

        float scale = cardPlaceScaleCurve.Evaluate(animationTime);
        if (upperTransform)
            upperTransform.localScale = initialScale * scale;
        if (lowerTransform)
            lowerTransform.localScale = initialScale * scale;
    }
}