using UnityEngine;
using System.Collections;

public class Team
{
    public static Team team1 = new Team() { id = 1 };
    public static Team team2 = new Team() { id = -1 };

    public int score;
    public int id;
}