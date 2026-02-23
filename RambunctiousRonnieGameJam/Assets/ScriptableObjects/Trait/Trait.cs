using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "Trait", menuName = "Scriptable Objects/Trait")]
public class Trait : ScriptableObject
{
    public enum Genres
    {
        None = 0,
        LateNightTalkShow = 1,
        Improv = 2,
        MysteryThriller = 3,
        PoliceShow = 4,
        ActionShow = 5,
        RealitySurvivalShow = 6,
        DatingCompetition = 7,
        Horror = 8,
        Romcom = 9,
        Drama = 10,
        Sitcom = 11,
        MonsterFeature = 12
    }

    public string traitName;
    public List<Genres> positiveGenres;
    public List<Genres> negativeGenres;
}
