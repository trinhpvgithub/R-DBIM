using RDBIM.ViewModels;

namespace RDBIM.Views
{
	public partial class RDBIMView
	{
		public RDBIMView(RDBIMViewModel viewModel)
		{
			InitializeComponent();
			DataContext = viewModel;
		}
	}
}