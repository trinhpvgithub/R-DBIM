using Autodesk.Revit.DB;
using HcBimUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDBIM.DataExcel
{
	public class DataJoints
	{
		public int Number {  get; set; }
		public double X {  get; set; }
		public double Y {  get; set; }
		public double Z {  get; set; }
		public XYZ Joint { get; set; }
		public Level LevelJoint { get; set; }
		public DataJoints(int number,double x,double y,double z,List<Level> levels) 
		{
			Number = number;
			X = x;
			Y = y;
			Z = z;
			Joint=new XYZ(x,y,z);
			LevelJoint=levels.FirstOrDefault(x=>x.Elevation.Equals(Z.MmToFoot()));
		}
	}
	
}
