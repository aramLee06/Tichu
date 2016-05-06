using UnityEngine;
using System.Collections;

public class SpecialCardEffectHandler : MonoBehaviour
{
    public static SpecialCardEffectHandler main;

    public float effectDuration = 1.5f;
    public GameObject dogEffectPrefab, dragonEffectPrefab, phoenixEffectPrefab, mahjongEffectPrefab;

    private void Awake()
    {
        main = this;
    }

    public static void DogCard(Vector3 pos)
    {
        if (main)
            PerformEffect(main.dogEffectPrefab,pos);
    }

    public static void DragonCard()
    {
        if (main)
            PerformEffect(main.dragonEffectPrefab);
    }

    public static void PhoenixCard()
    {
        if (main)
            PerformEffect(main.phoenixEffectPrefab);
    }

    public static void MahJongCard()
    {
        if (main)
            PerformEffect(main.mahjongEffectPrefab);
    }

    private static void PerformEffect(GameObject effect)
    {
        PerformEffect(effect, Vector3.zero);
    }

    private static void PerformEffect(GameObject effect, Vector3 pos)
    {
        if (main)
        {
            if (pos == Vector3.zero)
                pos = main.transform.position;

            Destroy(Instantiate(effect, main.transform.position, Quaternion.identity), main.effectDuration);
        }
    }
}