using System;
using System.Collections.Generic;
using System.Text;

namespace PlaneApp
{
    public class Plane
    {
        public string Model { get; set; }
        public double MaxRange { get; set; }
        public double CruiseSpeed { get; set; }

        public override string ToString()
        {
            return $"{Model} | Дальность: {MaxRange} км | Скорость: {CruiseSpeed} км/ч";
        }
    }
}
