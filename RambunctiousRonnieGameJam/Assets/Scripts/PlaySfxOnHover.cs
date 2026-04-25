using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySfxOnHover : MonoBehaviour, IPointerEnterHandler
{
    EventCore eventCore;
    public AudioClip hoverSfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventCore.playOneShotEV.Invoke(hoverSfx);
    }
}
