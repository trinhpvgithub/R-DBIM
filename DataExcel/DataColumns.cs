using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDBIM.DataExcel
{
	public class DataColumns
	{
		public int Startjoint {  get; set; }
		public int Endjoint { get; set; }
		public double Width {  get; set; }
		public double Height { get; set; }
		public double Axis {  get; set; }
		public DataColumns(int start,int end,double width,double height,double axis) 
		{
			Startjoint = start;
			Endjoint = end;
			Width = width;
			Height = height;
			Axis = axis;
		}
	}
}
