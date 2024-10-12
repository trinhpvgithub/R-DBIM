using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RDBIM.DataExcel
{
	public class Frames
	{
		public double FRAME {  get; set; }
		public int JOINT_I {  get; set; }
		public int JOINT_J {  get; set; }
		public double TYPE {  get; set; }
		public double LOCAL_ANG {  get; set; }
		public double LENGTH {  get; set; }
		public string SHAPE {  get; set; }
		public double b {  get; set; }
		public double h {  get; set; }
		public double bc {  get; set; }
		public double hc {  get; set; }
		public double bb {  get; set; }
		public string hb {  get; set; }
		public string d {  get; set; }
		public string MBT {  get; set; }
		public string NCT {  get; set; }
		public string NCD {  get; set; }
		public string LechTam {  get; set; }
		public string IDT {  get; set; }
	}
}
