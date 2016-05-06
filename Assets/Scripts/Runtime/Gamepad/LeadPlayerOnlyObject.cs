using UnityEngine;
using System.Collections;

public class LeadPlayerOnlyObject : MonoBehaviour
{
    private void Awake()
    {
        if (ClientController.main.userIndex != 0)
            Destroy(gameObject);
    }
}