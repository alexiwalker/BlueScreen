using System.Windows.Controls;

namespace BlueScreen.UIComponents {
	public partial class EpisodePanel : UserControl {
		public EpisodePanel(int episodenumber, string path, MainWindow parent) {
			InitializeComponent();
			EpisodeNumber.Text = episodenumber.ToString();
		}
	}
}