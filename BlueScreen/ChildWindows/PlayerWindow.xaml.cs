using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace BlueScreen.ChildWindows {
	public partial class PlayerWindow : Window {

		public enum PlayerState {
			Playing,Paused,Stopped,Finished
		}

		private PlayerState ElementState = PlayerState.Stopped;
		
		public string CurrentFile { get; private set; }
		public PlayerWindow() {
			InitializeComponent();
			CurrentFile = "BlueScreen Media Player";
			Title = CurrentFile;

			PlayerElement.LoadedBehavior = MediaState.Manual;
			PlayerElement.MediaOpened += _WindowMediaOpened;
			PlayerElement.BufferingStarted += BufferStart;
			PlayerElement.BufferingEnded += BufferEnd;

		}

		private void BufferStart(object sender, RoutedEventArgs e) {
			PlayerElement.Pause();
		}
		private void BufferEnd(object sender, RoutedEventArgs e) {
			PlayerElement.Play();
		}
		
		private void _WindowMediaOpened(object sender, RoutedEventArgs e)
		{
			MediaSlider.Maximum = PlayerElement.NaturalDuration.TimeSpan.TotalMilliseconds;
		}

		public PlayerWindow(string path) {
			InitializeComponent();
			CurrentFile = path;
			Title = CurrentFile;
			PlayerElement.LoadedBehavior = MediaState.Manual;
			PlayerElement.MediaEnded += _WindowMediaEnded;
			PlayerElement.MediaOpened += _WindowMediaOpened;

		}

		private void _WindowMediaEnded(object sender,RoutedEventArgs e) {
			ElementState = PlayerState.Finished;
		}
		
		
		private void _elementStop() {
			PlayerElement.Stop();
			ElementState = PlayerState.Stopped;
		}

		private void _elementPlay() {
			try {
				PlayerElement.Play();
				ElementState = PlayerState.Playing;
				
			}
			catch (Exception e) {
				ElementState = PlayerState.Stopped;
			}
		}

		private void _elementPause() {
			PlayerElement.Pause();
			ElementState = PlayerState.Paused;
		}

		

		public bool LoadContent(string path) {
			try {
				PlayerElement.Source = new System.Uri(path);
			}
			catch (Exception e) {
				Debug.Print(e.Message);
				return false;
			}
			return true;
		}

		public void PlayContent() {
			PlayerElement.Play();
		}
		
		public bool PlayContent(string path) {
			if (!IsVisible) {
				Show();
			}

			try {
				PlayerElement.Source = new System.Uri(path);
				PlayerElement.Play();
				ElementState = PlayerState.Playing;
			}
			catch (Exception e) {
				Debug.Print(e.Message);
				return false;
			}
			return true;
		}


		protected override void OnClosing(CancelEventArgs e) {
			e.Cancel = true;
			PlayerElement.Stop();
			ElementState = PlayerState.Stopped;
			Hide();
		}

		private void StopButton_OnClick(object sender, RoutedEventArgs e) {
			_elementStop();
		}

		private void StepBackButton_OnClick(object sender, RoutedEventArgs e) {
			PlayerElement.Position=PlayerElement.Position.Subtract(TimeSpan.FromSeconds(5));
		}

		private void StepForwardButton_OnClick(object sender, RoutedEventArgs e) {
			PlayerElement.Position=PlayerElement.Position.Add(TimeSpan.FromSeconds(5));
		}

		private long _sliderUpdateTimer = 0;

		private void Slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
			var eventtime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
			if (eventtime - _sliderUpdateTimer > 150) {
				PlayerElement.Position = TimeSpan.FromMilliseconds(e.NewValue);
				_sliderUpdateTimer = eventtime;
			}

			e.Handled = true;
		}
	}
}