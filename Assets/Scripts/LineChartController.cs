using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XCharts.Runtime{
public class LineChartController : MonoBehaviour
{


    // Update is called once per frame
    public void LineUpdate(float value)
    {
        var chart = gameObject.GetComponent<LineChart>();
        if (chart == null)
        {
            chart = gameObject.AddComponent<LineChart>();
            chart.Init();
        }
        chart.AddData(0, value);
        if(chart.GetSerie<Line>().data.Count>=1000)
        {
            chart.GetSerie<Line>().data.RemoveAt(0);
        }
    }


    void Start()
    {
        var chart = gameObject.GetComponent<LineChart>();
        if (chart == null)
        {
            chart = gameObject.AddComponent<LineChart>();
            chart.Init();
        }
        chart.ClearData();
    }
}
}