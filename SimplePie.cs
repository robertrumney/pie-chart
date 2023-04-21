//Simple alternative implementation of Jason Heins Pie Chart Timer
using UnityEngine;

public class SimplePie : MonoBehaviour 
{
    Material m_PieChart;
    const string PIE_FRACTION = "_Fraction";

    public bool isUI = false;

    private void Awake()
    {
        if(isUI)
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
        if (m_PieChart != null)
        {
           m_PieChart.SetFloat(PIE_FRACTION, current / total);
        }          
    }
}
