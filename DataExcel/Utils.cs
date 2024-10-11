using MiniExcelLibs;
using RDBIM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDBIM.DataExcel
{
	public class Utils
	{
		public static List<Joint> GetJoints(string path)
		{
			try
			{
				return MiniExcel.Query<Joint>(path, sheetName: NameDefine.Joint).ToList();
			}
			catch (Exception)
			{
				IO.Warning("File NotFound");
				return new List<Joint>();
			}
		}
		public static List<Frames> GetFrames(string path)
		{
			try
			{
				return MiniExcel.Query<Frames>(path, sheetName: NameDefine.Joint).ToList();
			}
			catch (Exception)
			{
				IO.Warning("File NotFound");
				return new List<Frames>();
			}
		}
	}
}
