using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BlueScreen {
	public partial class ShowCard : UserControl {
		public string showName { get; set; }
		private MainWindow parent;
		public ShowCard(string showName, MainWindow parent) {
			InitializeComponent();

			this.showName = showName;
			DisplayName.Text = showName;
			DisplayName.Foreground = Brushes.Beige;

			this.parent = parent;
		}

		private void CardClickMouseDown(object sender, MouseButtonEventArgs e) {
			if (e.ClickCount >= 2) {
				parent.DrawerSetShow(showName);
			}
		}

		public void setSize(int w,int h) {
			Height = h;
			Width = w;
		}

		public void clickActionPlay() {
			
		}

		public void clickActionResume() {
			
		}

		public void clickActionShow() {
			
		}
	}
}