using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace Egharpay.Business.Services
{
    public class YouTubeBusinessService : IYouTubeBusinessService
    {
        private string part = "snippet";
        public List<YouTube> Search(string searchTerm, int maxResults)
        {
            var apiKey = WebConfigurationManager.AppSettings["YouTubeAPIKey"];
            var youTubeApiRestClient = new YouTubeApiRestClient.YouTubeApiRestClient(apiKey);
            var result = youTubeApiRestClient.Search(part, searchTerm, maxResults);
            var youtube = new List<YouTube>();
            foreach (var item in result.Items)
            {
                youtube.Add(new YouTube()
                {
                    Description = item.Snippet.Description,
                    Title = item.Snippet.Title,
                    URL = $"https://www.youtube.com/embed/{item.Id.VideoId}"
                });
            }
            return youtube;
        }

        private async Task Run()
        {
            var apiKey = WebConfigurationManager.AppSettings["YouTubeAPIKey"];
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = "Google"; // Replace with your search term.
            searchListRequest.MaxResults = 50;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                        break;

                    case "youtube#channel":
                        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        break;

                    case "youtube#playlist":
                        playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                        break;
                }
            }
        }
    }
}
