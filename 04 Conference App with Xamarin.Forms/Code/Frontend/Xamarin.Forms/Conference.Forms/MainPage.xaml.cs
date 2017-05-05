﻿using Conference.Frontend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Conference.Core;

namespace Conference.Forms
{
    public partial class MainPage : TabbedPage
    {
        private MainViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
			//https://github.com/robinmanuelthiel/xamarinworkshop/tree/master/04%20Conference%20App%20with%20Xamarin.Forms
			//5.7 Play the Dependency Injection game

			// An dieser Stelle: Dependency Injection ; Wird später per Tool überarbeitet.
            var httpService = new FormsHttpService();
            var conferenceService = new HttpConferenceService(httpService);
            viewModel = new MainViewModel(conferenceService);

            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
				// Immer wenn ich auf die Seite komme, wird dies aufgerufen. ALso werde hier die Daten Refreshed.
            base.OnAppearing();
            await viewModel.RefreshAsync();
        }


		// Navigation auf neue Page
		private void Session_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			// Get selected session
			var selectedSession = e.SelectedItem as Session;
			if (selectedSession != null)
			{
				// Navigate to details page and provide selected session
				Navigation.PushAsync(new SessionDetailsPage(selectedSession));
			}

			// Unselect item
			(sender as ListView).SelectedItem = null;
		}

		private void Speaker_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			// Get selected session
			var selectedSpeaker = e.SelectedItem as Speaker;
			if (selectedSpeaker != null)
			{
				// Navigate to details page and provide selected session
				Navigation.PushAsync(new SpeakerDetailsPage(selectedSpeaker));
			}

			// Unselect item
			(sender as ListView).SelectedItem = null;
		}
    }
}
