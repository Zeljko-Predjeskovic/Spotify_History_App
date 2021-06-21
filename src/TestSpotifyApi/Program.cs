using SpotifyAPI.Web;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace TestSpotifyApi
{
    class Program
    {
        static async Task Main()
        {
            var config = SpotifyClientConfig.CreateDefault();

            
            var request = new ClientCredentialsRequest("ClientId", "ClentSecret");
            var response = await new OAuthClient(config).RequestToken(request);

            //new client with auth token
            var spotify = new SpotifyClient(config.WithToken(response.AccessToken = "Authorization Token"));

            //For the GetRecentPlayerId
            //We want the last 4 played tracks
            var playerReq = new PlayerRecentlyPlayedRequest() { Limit = 4, After = 10 };

            //Gets the recently played tracks
            var track = await spotify.Player.GetRecentlyPlayed(playerReq);

            //A collection of music tracks with their name, artists and with which album it came out
            var erg = from a in track.Items
                      select new {artist= from b in a.Track.Artists
                                          select b.Name
                      , trackName = a.Track.Name, album = a.Track.Album.Name };

           

            erg.ToList().ForEach(f=>Console.WriteLine("Artists: " + f.artist.First() + ", Song: " + f.trackName + ", Album: " + f.album));

        }
    }
}
