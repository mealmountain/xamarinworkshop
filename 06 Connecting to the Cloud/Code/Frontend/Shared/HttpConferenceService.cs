﻿using Conference.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Frontend
{
	public class HttpConferenceService : IConferenceService
    {
        private IHttpService httpService;

        public HttpConferenceService(IHttpService httpServiceImpl)
        {
            httpService = httpServiceImpl;
        }

        public async Task<List<Session>> GetSessionsAsync()
        {
			//var json = await httpService.GetStringAsync("https://raw.githubusercontent.com/robinmanuelthiel/xamarinworkshop/master/04%20Conference%20App%20with%20Xamarin.Forms/Mock/mocksessions.json");
			var json = await httpService.GetStringAsync("http://mealmountain.de/json/config.txt");
			var sessions = JsonConvert.DeserializeObject<List<Session>>(json);
            return sessions;
        }

        public async Task<List<Speaker>> GetSpeakersAsync()
        {
			var json = await httpService.GetStringAsync("https://raw.githubusercontent.com/robinmanuelthiel/xamarinworkshop/master/04%20Conference%20App%20with%20Xamarin.Forms/Mock/mockspeakers.json");
            var speakers = JsonConvert.DeserializeObject<List<Speaker>>(json);
            return speakers;
        }
    }
}