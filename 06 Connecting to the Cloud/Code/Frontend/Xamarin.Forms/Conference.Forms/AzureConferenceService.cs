using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Conference.Core;
using Conference.Frontend;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace Conference.Forms
{
	public class AzureConferenceService : IConferenceService
	{
		private MobileServiceClient client;
		private IMobileServiceSyncTable<Session> sessionTable;
		private IMobileServiceSyncTable<Speaker> speakerTable;

		public AzureConferenceService()
		{
			// Lädt die Daten aus der Azure Mobile App Seite
			//client = new MobileServiceClient("https://dotnetcolognebackend.azurewebsites.net");
			client = new MobileServiceClient("https://dotnetcolognemealmountain.azurewebsites.net");
		}

		public async Task InitAsync()
		{
			// Setup local database
			var path = Path.Combine(MobileServiceClient.DefaultDatabasePath, "syncstore.db");
			// legt die Datenbank an wenn nicht bereits vorhanden
			var store = new MobileServiceSQLiteStore(path);

			// Define local tables to sync with
			store.DefineTable<Session>();
			store.DefineTable<Speaker>();


			// Syncronisierung aktivieren
			// Initialize SyncContext
			await client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());


			// Get our sync table that will call out to azure
			sessionTable = client.GetSyncTable<Session>();
			speakerTable = client.GetSyncTable<Speaker>();
		}

		public async Task<List<Session>> GetSessionsAsync()
		{
			await InitAsync();
			await SyncAsync();
			return await sessionTable.ToListAsync();
		}

		public async Task<List<Speaker>> GetSpeakersAsync()
		{
			await InitAsync();
			await SyncAsync();
			return await speakerTable.ToListAsync();
		}

		// Synchronisiert Daten von Azure in SQLite
		public async Task SyncAsync()
		{
			try
			{
				await client.SyncContext.PushAsync();
				// "allSessions" ist meine ID, mit welcher sich der Server merkt, dass ich die Dtaen bereits habe.
				await sessionTable.PullAsync("allSessions", sessionTable.CreateQuery());
				await speakerTable.PullAsync("allSpeakers", speakerTable.CreateQuery());
			}
			catch (Exception ex)
			{
				// Unable to sync speakers, that is alright as we have offline capabilities: " + ex);
			}
		}
	}
}