using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CallUIEventOnCollision : MonoBehaviour
{
    GameObject targetGO;

    void Start()
    {
        targetGO = gameObject;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "PlayerAmmo") { return; }

        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(targetGO, pointer, ExecuteEvents.pointerClickHandler);
        Debug.Log("UI Hit");
    }
}
