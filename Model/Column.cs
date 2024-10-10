using Autodesk.Revit.DB;
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
		public FamilyInstance FamilyInstance { get; set; }
		public Column(DataColumns columnex,List<DataJoints> dataJoints, List<FamilyInstance> ColumnTypes)
		{
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
		public FamilyInstance CreateFamilyInstance(double width,double height)
		{
			FamilyInstance result=null;
			return result;
		}
	}
}
