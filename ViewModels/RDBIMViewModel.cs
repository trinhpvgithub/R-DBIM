using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HcBimUtils.DocumentUtils;
using HcBimUtils.MoreLinq;
using RDBIM.DataExcel;
using RDBIM.Model;
using RDBIM.Views;
using System.Windows.Forms;
using Object = RDBIM.DataExcel.Object;

namespace RDBIM.ViewModels
{
	public partial class RDBIMViewModel(Document document, UIDocument uIDocument) : ObservableObject
	{
		public RDBIMView MainView { get; set; }
		private List<Autodesk.Revit.DB.Level> Levels { get; set; } = [];
		private List<Joint> Joints { get; set; } = [];
		private List<Frames> Frames { get; set; } = [];
		private List<Stories> Stories { get; set; } = [];
		private List<GridLines> GridLines { get; set; } = [];
		private List<DataJoints> DataJoints { get; set; } = [];
		private List<DataColumns> DataColumns { get; set; } = [];
		private List<DataBeams> DataBeams { get; set; } = [];
		private List<Column> Columns { get; set; } = [];
		private List<Beam> Beams { get; set; } = [];
		private List<Model.Level> LevelDatas { get; set; } = [];
		private List<Model.Grid> Grids { get; set; } = [];
		[ObservableProperty]
		private string _path;
		[RelayCommand]
		private void OK()
		{
			GetData();
			ProcessingDataExcel();
			ProcessingDataRevit();
			var tran = new TransactionGroup(document);
			tran.Start("Column");
			//Unit.ChangeActiveViewToView3D(uIDocument);
			//CreateElement.CreateColumn(Columns, document);
			//         CreateElement.CreateBeam(Beams, document);
			//CreateElement.CreateLevel(LevelDatas, document);
			//CreateElement.CreateGrid(Grids,document);			
			//CreateElement.CreateGrid(Grids, document);
			RunWithoutProcess();
			tran.Assimilate();
			MainView.Close();
		}
		[RelayCommand]
		private void Cancel() { MainView.Close(); }
		[RelayCommand]
		private void GetPath()
		{
			MainView.Hide();
			var open = new OpenFileDialog();
			open.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
			if (open.ShowDialog() == DialogResult.OK)
			{
				Path = open.FileName;
			}
			else { Path = ""; }
			MainView.ShowDialog();
		}
		public void Run()
		{
			MainView.ShowDialog();
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
					var data = new DataColumns(frame.JOINT_I, frame.JOINT_J, frame.b, frame.h, frame.LOCAL_ANG);
					if (data != null)
					{
						DataColumns.Add(data);
					}
				}
				else
				{
					var data = new DataBeams(frame.JOINT_I, frame.JOINT_J, frame.b, frame.h);
					if (data != null)
					{
						DataBeams.Add(data);
					}
				}
			}
			foreach (var level in Stories)
			{
				var data = new Model.Level(level.Name, level.Elevation);
				if (data != null)
				{
					LevelDatas.Add(data);
				}
			}
			var gX = GridLines.Where(x => x.Direction == "X");
			var gY = GridLines.Where(x => x.Direction == "Y");
			foreach (var grid in GridLines)
			{
				var data = new Model.Grid(grid, gX.Max(x => x.Coordinate), gY.Max(x => x.Coordinate));
				if (data != null)
				{
					Grids.Add(data);
				}
			}
		}
		private void ProcessingDataRevit()
		{
			foreach (var column in DataColumns)
			{
				var cl = new Column(column, DataJoints, document);
				if (cl != null)
				{
					Columns.Add(cl);
				}
			}
			foreach (var beam in DataBeams)
			{
				var b = new Beam(beam, DataJoints, document);
				if (b != null)
				{
					Beams.Add(b);
				}
			}
		}
		private void GetData()
		{
			//using var tg = new Transaction(AC.Document, "Model");
			//tg.Start();
			//CreateElement.CreateLevel(LevelDatas, document);
			//CreateElement.CreateGrid(Grids, document);
			//document.Regenerate();
			//tg.Commit();
			Levels = new FilteredElementCollector(document)
			   .OfClass(typeof(Autodesk.Revit.DB.Level))
			   .Cast<Autodesk.Revit.DB.Level>()
			   .OrderBy(x => x.Elevation)
			   .ToList();
			Joints = Utils.GetJoints(Path);
			Frames = Utils.GetFrames(Path);
			Stories = Utils.GetStories(Path);
			GridLines = Utils.GetGrids(Path);
		}
		private void RunWithProcess()
		{
			var process = new Process();
			process.Show();
			using var tg = new TransactionGroup(AC.Document, "Model");
			tg.Start();
			foreach (var beam in Beams)
			{
				using var tx = new Transaction(AC.Document, "Modeling Beam");
				tx.Start();
				if (process.Flag == false)
				{
					break;
				}
				CreateElement.CreateBeamSingle(beam, document);
				process.Create(Beams.Count, "CreateBeams");
				tx.Commit();
			}
			process.Close();
			tg.Assimilate();
			var process1 = new Process();
			process1.Show();
			using var tg1 = new TransactionGroup(AC.Document, "Model");
			tg1.Start();
			foreach (var column in Columns)
			{
				using var tx = new Transaction(AC.Document, "Modeling Column ");
				tx.Start();
				if (process.Flag == false)
				{
					break;
				}
				CreateElement.CreateColumnSingle(column, document);
				process1.Create(Columns.Count, "CreateColumns");
				tx.Commit();
			}
			process1.Close();
			tg1.Assimilate();
		}
		private void RunWithoutProcess()
		{
			using var tg = new Transaction(AC.Document, "Model");
			tg.Start();
			CreateElement.CreateColumn(Columns, document);
			CreateElement.CreateBeam(Beams, document);
			//CreateElement.CreateLevel(LevelDatas, document);
			CreateElement.CreateGrid(Grids, document);
			tg.Commit();
		}
	}
}