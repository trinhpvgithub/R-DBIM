using Autodesk.Revit.DB;
using HcBimUtils;
using HcBimUtils.DocumentUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace RDBIM.Model
{
	public class CreateType
	{
		public static ElementType TypeColumn(Family family, double width, double height)
		{
			ElementType elementType = null;

			var columnTypes = family.GetFamilySymbolIds().Select(x => x.ToElement())
				.Cast<FamilySymbol>().ToList();

			foreach (var familySymbol in columnTypes)
			{
				var bParam = familySymbol.LookupParameter("b");
				var hParam = familySymbol.LookupParameter("h");
				var bInMM = Convert.ToInt32(bParam.AsDouble().FootToMm());
				var hInMM = Convert.ToInt32(hParam.AsDouble().FootToMm());

				if (width == bInMM && height == hInMM)
				{
					elementType = familySymbol;
				}
			}

			if (elementType == null)
			{
				//Duplicate Column Type
				var type = columnTypes.FirstOrDefault();

				var newTypeName = width + "x" + height + "mm";

				if (columnTypes.Select(x => x.Name).Contains(newTypeName))
				{
					newTypeName = newTypeName + " Ignore existed name";
				}

				while (true)
				{
					try
					{
						elementType = type?.Duplicate(newTypeName);
						break;
					}
					catch
					{
						newTypeName += ".";
					}
				}
				if (elementType != null)
				{
					elementType.LookupParameter("b").Set(width.MmToFoot());
					elementType.LookupParameter("h").Set(height.MmToFoot());
				}
			}

			return elementType;
		}
		public static ElementType BeamType(Family family, double width, double height)
		{
			ElementType elementType = null;
			var beamTypes = family.GetFamilySymbolIds().Select(x => x.ToElement())
				.Cast<FamilySymbol>().ToList();
			foreach (var familySymbol in beamTypes)
			{

				var bParameter = familySymbol.LookupParameter("b");

				var binMm = Convert.ToInt32(bParameter.AsDouble().FootToMm());

				var hParameter = familySymbol.LookupParameter("h");

				var hinMm = Convert.ToInt32(hParameter.AsDouble().FootToMm());

				if (height == hinMm && width == binMm)
				{
					elementType = familySymbol;
				}
			}

			if (elementType == null)
			{
				//Duplicate Beam Type
				var type = beamTypes.FirstOrDefault();

				var newTypeName = width + " x " + height + "mm";

				var i = 1;
				if (beamTypes.Select(x => x.Name).Contains(newTypeName))
				{
					newTypeName = $"{newTypeName} (1)";
				}

				while (true)
				{
					try
					{
						elementType = type?.Duplicate(newTypeName);
						break;
					}
					catch
					{
						i++;
						newTypeName = $"{newTypeName} ({i})";
					}
				}

				if (elementType != null)
				{
					elementType.LookupParameter("b").Set(width.MmToFoot());
					elementType.LookupParameter("h").Set(height.MmToFoot());
				}
			}
			return elementType;
		}
	}
}
