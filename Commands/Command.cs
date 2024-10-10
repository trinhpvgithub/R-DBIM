using Autodesk.Revit.Attributes;
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
			var viewModel = new RDBIMViewModel();
			var view = new RDBIMView(viewModel);
			view.ShowDialog();
		}
	}
}