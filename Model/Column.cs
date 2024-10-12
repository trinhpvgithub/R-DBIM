using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using HcBimUtils.DocumentUtils;
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
        public double Rotation {  get; set; }
        public Autodesk.Revit.DB.Level LevelBase { get; set; }
        public Autodesk.Revit.DB.Level LevelTop { get; set; }
        public FamilySymbol FamilyInstance { get; set; }
        public Column(DataColumns columnex, List<DataJoints> dataJoints, Document document)
        {
            var ColumnTypes = new FilteredElementCollector(document)
                .OfCategory(BuiltInCategory.OST_StructuralColumns)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .Where(x => x.StructuralMaterialType != StructuralMaterialType.Steel)
                .ToList();
            StartPoint = dataJoints.FirstOrDefault(x => x.Number.Equals(columnex.Startjoint)).Joint.PointMmToFoot();
            EndPoint = dataJoints.FirstOrDefault(x => x.Number.Equals(columnex.Endjoint)).Joint.PointMmToFoot();
            LevelBase = dataJoints.FirstOrDefault(x => x.Number.Equals(columnex.Startjoint)).LevelJoint;
            LevelTop = dataJoints.FirstOrDefault(x => x.Number.Equals(columnex.Endjoint)).LevelJoint;
            Rotation=columnex.Axis;
            FamilyInstance = CreateFamilyInstance(columnex.Width, columnex.Height, document);
        }
        public FamilySymbol CreateFamilyInstance(double width, double height, Document document)
        {
            var family = new FilteredElementCollector(AC.Document)
                .OfCategory(BuiltInCategory.OST_StructuralColumns)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .Select(x => x.Family)
                .Where(x => x.StructuralMaterialType != StructuralMaterialType.Steel)
                .FirstOrDefault(x => x.Name.Equals("M_Concrete-Rectangular-Column"));
            var a = new Transaction(document, "Type");
            a.Start();
            FamilySymbol result = CreateType.TypeColumn(family, width, height) as FamilySymbol;
            a.Commit();
            return result;
        }
    }
}
