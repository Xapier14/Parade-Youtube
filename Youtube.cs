using Parade;
using System.Text.RegularExpressions;

namespace Youtube
{
    public class YoutubeDownloader : IDownloader
    {
        private int _maxThreads;
        private string _location;
        private ParadeManager _parade;
        public string Handler => "Youtube";
        public YoutubeDownloader(ParadeManager parade)
        {

        }
#nullable enable
        public void Download(IDownloadable downloadable, string? destination)
        {
            Match urlData = Regex.Match(downloadable.Metadata.Source,
                                        RegexPatterns.Youtube,
                                        RegexOptions.IgnoreCase);

            // check if it has 4 groups,
            // [0] = whole regex match
            // [1] = youtube endpoint (watch | playlist)
            // [2] = id type ( v | list)
            // [3] == content id

            if (urlData.Success && urlData.Groups.Count == 4)
            {
                string endpoint = urlData.Groups[1].Value;
                string idType = urlData.Groups[2].Value;
                string id = urlData.Groups[3].Value;

                if (endpoint.ToLower() == "watch" &&
                    idType.ToLower() == "v")
                    DownloadVideo(downloadable, id);
                else
                if (endpoint.ToLower() == "playlist" &&
                    idType.ToLower() == "list")
                    DownloadPlaylist(downloadable, id);
                else
                    throw new Exception("Invalid YouTube URL data.");
            }
            else
            {
                // throw not match error
                throw new Exception("Invalid YouTube URL.");
            }
            downloadable.Metadata.Handler = this.Handler;
        }

        private void DownloadVideo(IDownloadable downloadable, string videoId)
        {

        }
        private void DownloadPlaylist(IDownloadable downloadable, string playlistId)
        {

        }

        public static bool IsDownloadable(IDownloadable downloadable)
        {
            Metadata metadata = downloadable.Metadata;
            return Regex.IsMatch(metadata.Source, RegexPatterns.Youtube, RegexOptions.IgnoreCase);
        }

        public void Start(IDownloadable downloadable)
        {
            throw new NotImplementedException();
        }

        public void Stop(IDownloadable downloadable)
        {
            throw new NotImplementedException();
        }

        public void Abort(IDownloadable downloadable)
        {
            throw new NotImplementedException();
        }
    }
}