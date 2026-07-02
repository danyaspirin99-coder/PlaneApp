using System.Text.Json;
using System;
using System.IO;
using System.Xml;
using PlaneApp;


public class PlaneControl
{
    private Plane[] _planes;

    public PlaneControl(Plane[] planes)
    {
        _planes = planes;
    }


    public void SortByRangeAndSpeed()
    {
        if (_planes == null || _planes.Length <= 1) return;


        int n = _planes.Length;
        for (int i = 0; i < n - 1; i++)
        {
            bool swapped = false;
            for (int j = 0; j < n - i - 1; j++)
            {

                bool shouldSwap = false;
                if (_planes[j].MaxRange < _planes[j + 1].MaxRange)
                {
                    shouldSwap = true;
                }
                else if (_planes[j].MaxRange == _planes[j + 1].MaxRange &&
                         _planes[j].CruiseSpeed < _planes[j + 1].CruiseSpeed)
                {
                    shouldSwap = true;
                }

                if (shouldSwap)
                {
                    var temp = _planes[j];
                    _planes[j] = _planes[j + 1];
                    _planes[j + 1] = temp;
                    swapped = true;
                }
            }
            if (!swapped) break;
        }
    }

    public Plane[] GetPlanes() => _planes ?? Array.Empty<Plane>();

    public void SaveToFile(string filePath)
    {
        File.WriteAllText(filePath, GetPlanes().ToString());
    }
}