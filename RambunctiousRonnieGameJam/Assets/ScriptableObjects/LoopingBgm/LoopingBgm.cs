using UnityEngine;

[CreateAssetMenu(fileName = "LoopingBgm", menuName = "Scriptable Objects/LoopingBgm")]
public class LoopingBgm : ScriptableObject
{
    public AudioClip bgm;
    [Tooltip("Where the loop should start. In seconds (like 1.5s).")]
    public float loopStart;
    [Tooltip("Where the loop should end. In seconds (like 35.4s). Set it to a negative number to make it loop when it reaches the end of the clip (not recommended).")]
    public float loopEnd = -1;
    [Tooltip("Volume of the audio clip.")]
    [Range(0f, 1f)]
    public float volume = 1f;
}
