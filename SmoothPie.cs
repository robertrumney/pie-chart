//Interpolated version of Jason Heins Pie Chart Timer
using UnityEngine;
using System.Collections;

public class SmoothPie : MonoBehaviour
{
    Material m_PieChart;
    const string PIE_FRACTION = "_Fraction";

    public bool isUI = false;

    private float m_TargetFraction = 0f;
    private float m_CurrentFraction = 0f;
    private float m_Timer = 0f;

    private void Awake()
    {
        if (isUI)
        {
            UnityEngine.UI.Image rend = GetComponent<UnityEngine.UI.Image>();

            if (rend.material)
            {
                m_PieChart = rend.material;
                m_PieChart.SetFloat(PIE_FRACTION, 1f);
            }
        }
        else
        {
            SpriteRenderer rend = GetComponent<SpriteRenderer>();
            if (rend.material)
            {
                m_PieChart = rend.material;
                m_PieChart.SetFloat(PIE_FRACTION, 0f);
            }
        }
    }

    public void SetFraction(float total, float current)
    {
        m_TargetFraction = current / total;
        m_Timer = 0f;

        // Set the duration of the interpolation based on the difference between the current and target fraction
        float lerpDuration = Mathf.Abs(m_TargetFraction - m_CurrentFraction);

        // Calculate the start fraction
        float startFraction = m_CurrentFraction;

        // Calculate the end fraction
        float endFraction = m_TargetFraction;

        // Start the interpolation
        if(this.gameObject.activeInHierarchy)
            StartCoroutine(InterpolateFraction(startFraction, endFraction, lerpDuration));
    }

    private IEnumerator InterpolateFraction(float start, float end, float duration)
    {
        // While the timer is less than the duration
        while (m_Timer < duration)
        {
            // Increment the timer
            m_Timer += Time.deltaTime;

            // Interpolate the fraction
            m_CurrentFraction = Mathf.Lerp(start, end, m_Timer / duration);

            // Set the pie chart fraction
            if (m_PieChart != null)
            {
                m_PieChart.SetFloat(PIE_FRACTION, m_CurrentFraction);
            }

            // Wait for the next frame
            yield return null;
        }

        // Set the final pie chart fraction
        if (m_PieChart != null)
        {
            m_PieChart.SetFloat(PIE_FRACTION, m_TargetFraction);
        }
    }
}
