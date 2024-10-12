using Autodesk.Revit.DB;
using HcBimUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDBIM.Model
{
	public class Level
	{
		public string Name { get; set; }
		public double Elevation {  get; set; }
		public Level(string name,double elevation)
		{
			Name = name;
			Elevation = elevation.MmToFoot();
		}

    }
}
