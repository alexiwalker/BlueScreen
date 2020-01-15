using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BlueScreen.ChildWindows;
using BlueScreen.UIComponents;
using BlueScreenCategoriser;
using BlueScreenIO.IOInterface;
using BlueScreenIO.Storage;

namespace BlueScreen {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow {
		private SettingsWindow _settingsWindow;
		private ToolsWindow _toolsWindow;
		private PlayerWindow _playerWindow;

		public MainWindow() {
			InitializeComponent();

			//create but hide the extra windows so that their state is always persistent if opened
			//they can be toggled on by clicking their button and close will be overridden to hide and not delete
			_settingsWindow = new SettingsWindow();
			_toolsWindow = new ToolsWindow();
			_playerWindow = new PlayerWindow();
			_settingsWindow.Hide();
			_toolsWindow.Hide();
			_playerWindow.Hide();


			// ReSharper disable once InconsistentNaming
			bool validDB = false;

			try {
				validDB = DatabaseStorage.InitDatabase();
			}
			catch (Exception e) {
				Debug.Print(e.Message);
				Close();

			}

			var names = DatabaseStorage.GetAllShowNames();


			foreach (var name in names) {
				try {
					var showCard = new ShowCard(name, this);
					Wrapper.Children.Add(showCard);
					showCard.Width = 75;
					showCard.Height = 75;
					showCard.Background = Brushes.Green;
					showCard.Margin = new Thickness(10, 10, 10, 10);
				}
				catch (Exception e) {
					Debug.Print(e.Message);
				}
			}
		}

//		private void TestButtonClick(object sender, RoutedEventArgs e) {
//			const string source = @"D:\library";
//
//			var allFiles = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories);
//
//			foreach (var f in allFiles) {
//				var episodeInfo = FileCategorizer.GetFileInfo(f);
//
//				if (!episodeInfo) {
//					Console.WriteLine($@"Unable to extract episode details from path provided, or invalid extension for: {f}");
//					continue;
//				}
//
//
//				var success = DatabaseStorage.AddFileToDatabase(episodeInfo);
//
//				if (success) {
//					//todo
//				}
//			}
//		}

		private string CurrentClickedShow { get; set; }

		public void DrawerSetShow(string showname) {
			CurrentClickedShow = showname;
			

			BottomDrawer.Children.RemoveRange(0, BottomDrawer.Children.Count);

			var seasons = DatabaseStorage.GetSeasonsForShow(showname);
			SeasonSelector.Items.Clear();
			foreach (var season in seasons) {
				var tb = new TextBlock();
				tb.Text = $"Season {season.ToString()}";
				SeasonSelector.Items.Add(tb);
			}
				
			SeasonSelector.SelectedIndex = 0;
			SeasonSelector.Visibility = Visibility.Visible;
		}

		private void SeasonSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
			ComboBox cb = (ComboBox) sender;

			int ind = cb.SelectedIndex;
			if (ind < 0) return;
			var seasontext = ((TextBlock)cb.Items[ind]).Text;
			int sn = Int32.Parse(seasontext.Replace("Season ", ""));
			Dictionary<int, string> episodes = DatabaseStorage.GetEpisodesForSeason(CurrentClickedShow, sn);
			
		}

		private void DrawerBack_OnClick(object sender, RoutedEventArgs e) {
		}
	}
}