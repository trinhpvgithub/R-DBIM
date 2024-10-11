using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using RDBIM.DataExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDBIM.Model
{
	public class Beam
	{
		public XYZ StartPoint { get; set; }
		public XYZ EndPoint { get; set; }
		public Level Level { get; set; }
		public FamilySymbol FamilySymbol { get; set; }
		public Beam(DataColumns columnex, List<DataJoints> dataJoints,Document document)
		{
			var familys = new FilteredElementCollector(document)
				.OfCategory(BuiltInCategory.OST_StructuralFraming)
				.OfClass(typeof(FamilySymbol))
				.Cast<FamilySymbol>()
				.ToList();
			StartPoint = dataJoints.FirstOrDefault(x => x.Number.Equals(columnex.Startjoint)).Joint;
			EndPoint = dataJoints.FirstOrDefault(x => x.Number.Equals(columnex.Endjoint)).Joint;
			Level = dataJoints.FirstOrDefault(x => x.Number.Equals(columnex.Startjoint)).LevelJoint;
			var nametype = columnex.Width.ToString() + columnex.ToString() + " mm";
			if (familys.FirstOrDefault(x => x.Name.Equals(nametype)) != null)
			{
				FamilySymbol = familys.FirstOrDefault(x => x.Name.Equals(nametype));
			}
			else { FamilySymbol = CreateFamilyInstance(columnex.Width, columnex.Height); }
		}
		public FamilySymbol CreateFamilyInstance(double width, double height)
		{
			FamilySymbol result = null;
			return result;
		}
	}
}
