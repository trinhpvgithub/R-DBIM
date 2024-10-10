using Nice3point.Revit.Toolkit.External;
using RDBIM.Commands;

namespace RDBIM
{
	[UsedImplicitly]
	public class Application : ExternalApplication
	{
		public override void OnStartup()
		{
			CreateRibbon();
		}

		private void CreateRibbon()
		{
			var panel = Application.CreatePanel("Commands", "RDBIM");

			var showButton = panel.AddPushButton<Command>("Execute");
			showButton.SetImage("/RDBIM;component/Resources/Icons/RibbonIcon16.png");
			showButton.SetLargeImage("/RDBIM;component/Resources/Icons/RibbonIcon32.png");
		}
	}
}