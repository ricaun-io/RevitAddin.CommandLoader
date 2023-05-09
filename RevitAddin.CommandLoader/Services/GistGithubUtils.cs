using Autodesk.Revit.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace RevitAddin.CommandLoader.Services
{
    public class GistGithubUtils
    {
        public static bool TryGetGistString(string url, out string output)
        {
            output = "";
            if (url.IndexOf("gist.github", StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                url = url.Trim('/');
                if (url.IndexOf("/raw", StringComparison.InvariantCultureIgnoreCase) == -1)
                {
                    url += "/raw";
                }
                output = GetString(url);
                return true;
            }
            return false;
        }

        public static bool TryGetGistId(string url, out string id)
        {
            id = "";
            if (url.IndexOf("gist.github", StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                url = url.Trim('/');
                id = url.Split('/').Last();
                return true;
            }
            return false;
        }

        public static bool TryGetGistFilesContent(string url, out string[] contents)
        {
            contents = null;
            if (TryGetGistModel(url, out GistModel gistModel))
            {
                if (gistModel is not null)
                {
                    contents = gistModel.files.Values.Select(e => e.content).ToArray();
                    return contents.Length > 0;
                }
            }
            return false;
        }

        public static bool TryGetGistModel(string url, out GistModel gistModel)
        {
            gistModel = null;
            if (TryGetGistId(url, out string gistId))
            {
                var content = GetGistString(gistId);
                if (content is null)
                    return false;

                try
                {
                    var jsonService = new JsonService<GistModel>();
                    gistModel = jsonService.Deserialize(content);
                }
                catch { }

                return gistModel is not null;
            }
            return false;
        }

        private static string GetGistString(string id)
        {
            try
            {
                return GetString($"https://api.github.com/gists/{id}");
            }
            catch { }
            return null;
        }

        private static string GetString(string url)
        {
            using (var client = new System.Net.WebClient())
            {
                client.Headers.Add(System.Net.HttpRequestHeader.UserAgent, typeof(GistGithubUtils).Assembly.GetName().Name);
                return client.DownloadString(url);
            }
        }
    }

    public class GistModel
    {
        public string Id { get; set; }
        public Dictionary<string, File> files { set; get; }

        public class File
        {
            public double size { set; get; }
            public string filename { set; get; }
            public string raw_url { set; get; }
            public string language { set; get; }
            public string content { set; get; }
        }
    }
}

