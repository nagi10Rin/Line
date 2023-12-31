using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class LineController : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float cycle = 0.1f;
    [SerializeField] private int _startDelayTime = 0;
    private float previousCycle = 0f;

    private bool isIncrease = true;

    private void Update()
    {
        sinUpdater(1000, 0.005f, 5.0f, _startDelayTime);

        if (Mathf.Abs(previousCycle - cycle) > float.Epsilon)
        {
            Render(SinRenderer(1024, 30f, -15f, cycle));
            previousCycle = cycle;
        }
    }

    private Vector3[] SinRenderer(int size, float xLength, float xOffset, float cycle)
    {
        var points = new Vector3[size];
        for (var i = 0; i < points.Length; i++)
        {
            var v = Mathf.Sin((float)i / size * 2 * Mathf.PI * cycle);
            points[i] = new Vector3((float)i / size * xLength + xOffset, v, 0);
        }

        return points;
    }

    public void Render(Vector3[] points)
    {
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }

    async void sinUpdater(int delayTime, float addCycle, float maxTime, int delayStartTime)
    {
        await Task.Delay(delayStartTime); //ms
        if (cycle < maxTime && cycle > 0.0f && isIncrease == true)
        {
            cycle += addCycle;
        }
        if (cycle > maxTime)
        {
            isIncrease = false;
            cycle = maxTime;
            cycle -= addCycle;
        }
        if (cycle < maxTime && isIncrease == false)
        {
            cycle -= addCycle;
        }
        if (cycle < 0.0f)
        {
            isIncrease = true;
            cycle = 0.0f;
            await Task.Delay(delayTime); //ms
            cycle += addCycle;
        }

    }

}
