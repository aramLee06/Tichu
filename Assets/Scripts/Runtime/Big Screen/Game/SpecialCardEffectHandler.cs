using UnityEngine;
using System.Collections;

/// <summary>
/// Handles the special cards' effects
/// </summary>
public class SpecialCardEffectHandler : MonoBehaviour
{
	/// <summary>
	/// Singleton.
	/// </summary>
    public static SpecialCardEffectHandler main;

	/// <summary>
	/// The duration of the effect.
	/// </summary>
    public float effectDuration = 1.5f;
	/// <summary>
	/// The effect prefabs
	/// </summary>
    public GameObject dogEffectPrefab, dragonEffectPrefab, phoenixEffectPrefab, mahjongEffectPrefab;

    private void Awake()
    {
        main = this;
    }

    /// <summary>
    /// Effect for the Dog Card
    /// </summary>
    /// <param name="pos">Position of the effect</param>
    public static void DogCard(Vector3 pos)
    {
        if (main)
            PerformEffect(main.dogEffectPrefab,pos);
    }

    /// <summary>
    /// Effect for the Dragon Card
    /// </summary>
    public static void DragonCard()
    {
        if (main)
            PerformEffect(main.dragonEffectPrefab);
    }


    /// <summary>
    /// Effect for the Pheonix Card
    /// </summary>
    public static void PhoenixCard()
    {
        if (main)
            PerformEffect(main.phoenixEffectPrefab);
    }

    /// <summary>
    /// Effect for the MahJong card
    /// </summary>
    public static void MahJongCard()
    {
        if (main)
            PerformEffect(main.mahjongEffectPrefab);
    }

    /// <summary>
    /// Performs a certain card's effect
    /// </summary>
    /// <param name="effect">The effect object</param>
    private static void PerformEffect(GameObject effect)
    {
        PerformEffect(effect, Vector3.zero);
    }

    /// <summary>
    /// Perform a certain card's effect
    /// </summary>
    /// <param name="effect">The effect object</param>
    /// <param name="pos">The effect's position</param>
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