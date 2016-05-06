using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class ProfileIconManager : MonoBehaviour
{
    public Character character;
    public bool topAvatar;
    private Sprite happy, neutral, sad;

    [Range(0,1)] public float flowLerp = .1f;

    public Transform upperTransform, lowerTransform;
    public AnimationCurve cardPlaceScaleCurve;
    public Vector3 initialScale = Vector3.one;

    public float animationTime = 1;
    public float animationSpeed = .1f;

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