using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using HcBimUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDBIM.Model
{
	public static class Unit
	{
		public static XYZ PointMmToFoot(this XYZ point)
		{
			return new XYZ(point.X.MmToFoot(), point.Y.MmToFoot(), point.Z.MmToFoot());
		}
		public static void ChangeActiveViewToView3D(UIDocument uidoc)
		{
			Document doc = uidoc.Document;

			// Filter for all 3D views in the document
			var collector = new FilteredElementCollector(doc)
								.OfClass(typeof(View3D))
								.Cast<View3D>()
								.Where(v => !v.IsTemplate); // Ignore template views

			// Pick the first available 3D view (or choose by name if needed)
			View3D view3D = collector.FirstOrDefault();

			if (view3D != null)
			{
				// Change the active view to the selected 3D view
				uidoc.ActiveView = view3D;
			}
			else
			{
				TaskDialog.Show("Error", "No 3D view found in the project.");
			}
		}
	}
}
