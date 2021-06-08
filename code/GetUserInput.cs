using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class GetUserInput : MonoBehaviour
{
    public SteamVR_Action_Vector2 m_TouchpadTouch = null;
    public SteamVR_Action_Boolean m_TriggerButton = null;
    public SteamVR_Action_Boolean m_SideGrip = null;
    public SteamVR_Action_Boolean m_GripLong = null;
    public Dataset m_DatasetInformation;
    public BuildTrajectory m_BuildTracjectory;
    public SteamVR_Input_Sources m_HandType;
    public Slider m_Slider;
    public float numerator = 100;
    public float m_SmoothingFactorFast = 1f;
    public float m_SmoothingFactorSlow = .01f;
 

    // Start is called before the first frame update
    private void Start()
    {
        m_TouchpadTouch.AddOnAxisListener(SkipSlider, m_HandType);
    }

    private void OnDestroy()
    {
        m_TouchpadTouch.RemoveOnAxisListener(SkipSlider, m_HandType);
    }

 

    private void SkipSlider(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        bool isGoingFast = m_TriggerButton.GetState(SteamVR_Input_Sources.LeftHand);
        if (!isGoingFast)
        {
            ChangeSlider(axis.x, m_SmoothingFactorSlow);
        }
        else
        {
            ChangeSlider(axis.x, m_SmoothingFactorFast);
        }
    }

    private void ChangeSlider(float x, float factor)
    {
        float normalizer = numerator / m_DatasetInformation.m_TimeRange;
        m_Slider.value += (x * factor * normalizer);
    }
}