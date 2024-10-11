using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HcBimUtils.DocumentUtils;
using HcBimUtils.MoreLinq;
using RDBIM.DataExcel;
using RDBIM.Model;
using System.Windows.Forms;
using Object = RDBIM.DataExcel.Object;

namespace RDBIM.ViewModels
{
	public partial class RDBIMViewModel(Document document) : ObservableObject
	{
		private List<Level> Levels { get; set; }
		private List<Joint> Joints { get; set; }
		private List<Frames> Frames { get; set; }
		private List<DataJoints> DataJoints { get; set; }
		private List<DataColumns> DataColumns { get; set; }
		private List<DataBeams> DataBeams { get; set; }
		private List<Column> Columns { get; set; }
		private List<Beam> Beams { get; set; }
		[ObservableProperty]
		private string _path;
		[RelayCommand]
		private void OK()
		{
			Joints = Utils.GetJoints(Path);
			Frames = Utils.GetFrames(Path);
		}
		[RelayCommand]
		private void GetPath()
		{
			var open=new OpenFileDialog();
		}
		private void ProcessingDataExcel()
		{
			foreach (var joint in Joints)
			{
				var data = new DataJoints(joint.Name, joint.X, joint.Y, joint.Z, Levels);
				if (data != null)
				{
					DataJoints.Add(data);
				}
			}
			foreach (var frame in Frames)
			{
				if (frame.TYPE == (int)Object.Column)
				{
					var data = new DataColumns(frame.JOINT_I, frame.JOINT_I, frame.b, frame.h, frame.LOCAL_ANG);
					if (data != null)
					{
						DataColumns.Add(data);
					}
				}
				else
				{
					var data = new DataBeams(frame.JOINT_I, frame.JOINT_I, frame.b, frame.h);
					if (data != null)
					{
						DataBeams.Add(data);
					}
				}
			}
		}
		private void ProcessingDataRevit()
		{
			foreach (var column in DataColumns)
			{
				var cl = new Column(column, DataJoints,document);
				if(cl!=null)
				{
					Columns.Add(cl);
				}
			}
		}
		private void GetData()
		{
			Levels = new FilteredElementCollector(document)
			   .OfClass(typeof(Level))
			   .Cast<Level>()
			   .OrderBy(x => x.Elevation)
			   .ToList();
			Joints = Utils.GetJoints(Path);
			Frames = Utils.GetFrames(Path);
		}
	}
}