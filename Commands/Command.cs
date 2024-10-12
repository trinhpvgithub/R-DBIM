using Autodesk.Revit.Attributes;
using HcBimUtils.DocumentUtils;
using Nice3point.Revit.Toolkit.External;
using RDBIM.ViewModels;
using RDBIM.Views;

namespace RDBIM.Commands
{
	[UsedImplicitly]
	[Transaction(TransactionMode.Manual)]
	public class Command : ExternalCommand
	{
		public override void Execute()
		{
			AC.GetInformation(UiDocument);
			try
			{
				var view = new RDBIMView();
				var viewModel = new RDBIMViewModel(AC.Document,AC.UiDoc) { MainView=view};
				view.DataContext = viewModel;
				viewModel.Run();
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}