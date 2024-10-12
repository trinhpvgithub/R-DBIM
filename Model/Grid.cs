using Autodesk.Revit.DB;
using HcBimUtils;
using RDBIM.DataExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDBIM.Model
{
    public class Grid
    {
        public string Name { get; set; }
        public XYZ StartPoint { get; set; }
        public XYZ EndPoint { get; set; }
        public XYZ Direction { get; set; }
        public Grid(GridLines gridLines,double maxX,double maxy)
        {
            Name = gridLines.Name;
            Direction = gridLines.Direction == "X" ? XYZ.BasisY : XYZ.BasisX;
            if (Direction.IsParallel(XYZ.BasisY))
            {
                StartPoint = new XYZ(gridLines.Coordinate.MmToFoot(), -1000.MmToFoot(), 0);
                EndPoint = new XYZ(gridLines.Coordinate.MmToFoot(), (maxy + 1000).MmToFoot(), 0);
            }
            else 
            {
                StartPoint = new XYZ(-1000.MmToFoot(), gridLines.Coordinate.MmToFoot(), 0);
                EndPoint = new XYZ((maxX+1000).MmToFoot(), gridLines.Coordinate.MmToFoot(), 0);
            }
        }
    }
}
