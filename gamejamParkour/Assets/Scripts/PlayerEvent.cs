using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerEvent : MonoBehaviour
{
    UnityEvent m_MyEvent;
    UnityEngine.VFX.VisualEffect _vfx;
    private KeyCode SPRINT_KEY = KeyCode.LeftShift;
    void Start()
    {
        if (m_MyEvent == null)
        {
            m_MyEvent = new UnityEvent();
        }
        ///   _vfx = GetComponent<VisualEffect>();
        Debug.Log(_vfx);
        m_MyEvent.AddListener(toggleSprintParticle);
    }

    void Update()
    {
        if (Input.GetKey(SPRINT_KEY) && m_MyEvent != null)
        {
            m_MyEvent.Invoke();
        }
    }

    void toggleSprintParticle()
    {
        // if (_vfx.spawnRate == 96f)
        // {
        //     _vfx.SetFloat("SpawnRate", 0f);
        // }
        // else
        // {
        //     _vfx.SetFloat("SpawnRate", 96f);
        // }
    }
}