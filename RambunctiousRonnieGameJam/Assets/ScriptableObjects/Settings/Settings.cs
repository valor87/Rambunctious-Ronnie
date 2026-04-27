using ParadoxNotion.Design;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class Settings : ScriptableObject
{
    [Range(0f, 1f)]
    public float musicVolume;
    [Range(0f, 1f)]
    public float sfxVolume;
}
