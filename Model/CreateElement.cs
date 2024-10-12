using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB;
using HcBimUtils.DocumentUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HcBimUtils;
using System.Windows.Controls;
using System.Diagnostics;

namespace RDBIM.Model
{
	public class CreateElement
	{
		public static void CreateColumn(List<Column> columns, Document document)
		{
			foreach (var columndata in columns)
			{
				var column = document.Create.NewFamilyInstance(columndata.StartPoint, columndata.FamilyInstance,
										columndata.LevelBase, StructuralType.Column);
				document.Regenerate();
				var topLevelParam = column.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM);
				topLevelParam.Set(columndata.LevelTop.Id);
				//ElementTransformUtils.RotateElement(document, column.Id, colum, columnInfo.Rotation);
				var topoffsetParam = column.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM);
				topoffsetParam.Set(0);
			}

		}
		public static void CreateBeam(List<Beam> beams, Document document)
		{
			foreach (var beamdata in beams)
			{
				var l = beamdata.StartPoint.CreateLine(beamdata.EndPoint);
				var beam = document.Create.NewFamilyInstance(l, beamdata.FamilySymbol, beamdata.Level,
								StructuralType.Beam);
				document.Regenerate();
			}
		}
		public static void CreateLevel(List<Level> levels, Document document)
		{
			var lvs = new FilteredElementCollector(document)
							.WhereElementIsNotElementType()
							.OfCategory(BuiltInCategory.OST_Levels)
							.Cast<Autodesk.Revit.DB.Level>()
							.ToList();
			var type=new FilteredElementCollector(document)
				.OfCategory(BuiltInCategory.OST_Levels)
				.Cast<Element>()
				.FirstOrDefault();
			try
			{
				document.Delete(lvs.Select(x => x.Id).ToList());
				foreach (var level in levels)
				{
					var levelmodel = Autodesk.Revit.DB.Level.Create(document, level.Elevation);
					if (null == levelmodel)
					{
						throw new Exception("Create a new level failed.");
					}

					// Change the level name
					levelmodel.Name = level.Name;
					levelmodel.ChangeTypeId(type.Id);
				}
			}
			catch
			{

			}
		}
		public static void CreateGrid(List<Model.Grid> grids, Document document)
		{
			var gridss = new FilteredElementCollector(document)
				.WhereElementIsNotElementType()
				.OfCategory(BuiltInCategory.OST_Grids)
				.Cast<Autodesk.Revit.DB.Grid>()
				.ToList();
			var type= new FilteredElementCollector(document)
				.OfCategory(BuiltInCategory.OST_Grids)
				.WhereElementIsElementType()
				.Cast<Element>()
				.FirstOrDefault();
			document.Delete(gridss.Select(x => x.Id).ToList());
			try
			{
				foreach (var grid in grids)
				{
					var l = grid.StartPoint.CreateLine(grid.EndPoint);
					var gridmodel = Autodesk.Revit.DB.Grid.Create(document, l);
					var nameparam = gridmodel.get_Parameter(BuiltInParameter.DATUM_TEXT);
					if (grid.Name != null)
					{
						nameparam.Set(grid.Name);
					}
					gridmodel.ShowBubbleInView(DatumEnds.End0, document.ActiveView);
					gridmodel.ShowBubbleInView(DatumEnds.End1, document.ActiveView);
					gridmodel.ChangeTypeId(type.Id);
					Debug.WriteLine(gridmodel.Id);
				}
			}
			catch
			{

			}
		}
		public static void CreateColumnSingle(Column columndata, Document document)
		{

			var column = document.Create.NewFamilyInstance(columndata.StartPoint, columndata.FamilyInstance,
									columndata.LevelBase, StructuralType.Column);
			document.Regenerate();
			var topLevelParam = column.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_PARAM);
			topLevelParam.Set(columndata.LevelTop.Id);
			//ElementTransformUtils.RotateElement(document, column.Id, colum, columnInfo.Rotation);
			var topoffsetParam = column.get_Parameter(BuiltInParameter.FAMILY_TOP_LEVEL_OFFSET_PARAM);
			topoffsetParam.Set(0);

		}
		public static void CreateBeamSingle(Beam beam, Document document)
		{
			var l = beam.StartPoint.CreateLine(beam.EndPoint);
			var beam1 = document.Create.NewFamilyInstance(l, beam.FamilySymbol, beam.Level,
							StructuralType.Beam);
			document.Regenerate();

		}
	}
}
