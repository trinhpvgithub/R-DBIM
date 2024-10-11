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
			var viewModel = new RDBIMViewModel(AC.Document);
			var view = new RDBIMView(viewModel);
			view.ShowDialog();
		}
	}
}