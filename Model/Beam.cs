using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using HcBimUtils.DocumentUtils;
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
		public Autodesk.Revit.DB.Level Level { get; set; }
		public FamilySymbol FamilySymbol { get; set; }
		public Beam(DataBeams beamex, List<DataJoints> dataJoints, Document document)
		{
			var familys = new FilteredElementCollector(document)
				.OfCategory(BuiltInCategory.OST_StructuralFraming)
				.OfClass(typeof(FamilySymbol))
				.Cast<FamilySymbol>()
				.ToList();
			StartPoint = dataJoints.FirstOrDefault(x => x.Number.Equals(beamex.Startjoint)).Joint.PointMmToFoot();
			EndPoint = dataJoints.FirstOrDefault(x => x.Number.Equals(beamex.Endjoint)).Joint.PointMmToFoot();
			Level = dataJoints.FirstOrDefault(x => x.Number.Equals(beamex.Startjoint)).LevelJoint;
			FamilySymbol = CreateFamilyInstance(beamex.Width, beamex.Height, document);
		}
		public FamilySymbol CreateFamilyInstance(double width, double height, Document document)
		{
			var family = new FilteredElementCollector(AC.Document)
				.OfCategory(BuiltInCategory.OST_StructuralFraming)
				.OfClass(typeof(FamilySymbol))
				.Cast<FamilySymbol>()
				.Select(x => x.Family)
				.Where(x => x.StructuralMaterialType != StructuralMaterialType.Steel)
				.FirstOrDefault(x => x.Name.Equals("M_Concrete-Rectangular Beam"));
			var a = new Transaction(document, "Type");
			a.Start();
			FamilySymbol result = CreateType.TypeColumn(family, width, height) as FamilySymbol;
			a.Commit();
			return result;
		}
	}
}
