using System.Windows.Controls;

namespace BlueScreen.UIComponents {
	public partial class SeasonPanel : UserControl {
		public SeasonPanel() {
			InitializeComponent();
		}

		private int SNumber;
		private string ShowName;
		
		
		public SeasonPanel(string show, int season) {
			InitializeComponent();
			ShowName = show;
			SNumber = season;
			SeasonNumber.Text=season.ToString();
		}
	}
}