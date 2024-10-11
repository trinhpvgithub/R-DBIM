using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using RDBIM.DataExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDBIM.Model
{
	public class Column
	{
		public XYZ StartPoint { get; set; }
		public XYZ EndPoint { get; set; }
		public Level LevelBase { get; set; }
		public Level LevelTop { get; set; }
		public FamilySymbol FamilyInstance { get; set; }
		public Column(DataColumns columnex,List<DataJoints> dataJoints,Document document)
		{
			var ColumnTypes = new FilteredElementCollector(document)
				.OfCategory(BuiltInCategory.OST_StructuralColumns)
				.OfClass(typeof(FamilySymbol))
				.Cast<FamilySymbol>()
				.Where(x => x.StructuralMaterialType != StructuralMaterialType.Steel)
				.ToList();
			StartPoint = dataJoints.FirstOrDefault(x => x.Number.Equals(columnex.Startjoint)).Joint;
			EndPoint = dataJoints.FirstOrDefault(x => x.Number.Equals(columnex.Endjoint)).Joint;
			LevelBase = dataJoints.FirstOrDefault(x => x.Number.Equals(columnex.Startjoint)).LevelJoint;
			LevelBase = dataJoints.FirstOrDefault(x => x.Number.Equals(columnex.Endjoint)).LevelJoint;
			var nametype=columnex.Width.ToString()+columnex.ToString()+" mm";
			if (ColumnTypes.FirstOrDefault(x => x.Name.Equals(nametype)) != null)
			{
				FamilyInstance = ColumnTypes.FirstOrDefault(x => x.Name.Equals(nametype));
			}
			else { FamilyInstance = CreateFamilyInstance(columnex.Width, columnex.Height); }
		}
		public FamilySymbol CreateFamilyInstance(double width,double height)
		{
			FamilySymbol result=null;
			return result;
		}
	}
}
