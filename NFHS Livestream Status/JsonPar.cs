using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace NFHS_Livestream_Status
{
    internal class JsonPar
    {
        private string eventId = null;
        private string eventType = null;

        protected string _Status = null;
        protected string _Headline = null;
        protected string _SubHeadline = null;
        protected string _Description = null;
        protected string _Key = null;
        protected bool _HD = false;
        protected string _URL = null;

        public JsonPar(string id = null, string type = null)
        {
            eventId = id;
            eventType = type;
            Load();
        }

        public void Load()
        {
            WebClient WebC = new WebClient();
            var RawJson = WebC.DownloadString("http://cfunity.nfhsnetwork.com/v1/" + eventType + "/" + eventId);
            JObject parsedObject = JObject.Parse(RawJson);

            parsedObject = JObject.Parse(RawJson);

            _Status = parsedObject["publishers"][0]["broadcasts"][0]["status"].ToString();
            _Headline = parsedObject["publishers"][0]["broadcasts"][0]["headline"].ToString();
            _SubHeadline = parsedObject["publishers"][0]["broadcasts"][0]["subheadline"].ToString();
            _Description = parsedObject["publishers"][0]["broadcasts"][0]["description"].ToString();
            _Key = parsedObject["publishers"][0]["broadcasts"][0]["key"].ToString();
            _HD = Convert.ToBoolean(parsedObject["publishers"][0]["broadcasts"][0]["is_hd"]);

            RawJson = WebC.DownloadString("http://cfunity.nfhsnetwork.com/v1/broadcasts/" + _Key + "/url.json");
            parsedObject = JObject.Parse(RawJson);
            _URL = parsedObject["video_url"].ToString();
        }

        public string Status
        {
            set { _Status = value; }
            get { return _Status; }
        }

        public string Headline
        {
            set { _Headline = value; }
            get { return _Headline; }
        }

        public string SubHeadline
        {
            set { _SubHeadline = value; }
            get { return _SubHeadline; }
        }

        public string Description
        {
            set { _Description = value; }
            get { return _Description; }
        }

        public string Key
        {
            set { _Key = value; }
            get { return _Key; }
        }

        public bool HD
        {
            set { _HD = value; }
            get { return _HD; }
        }

        public string URL
        {
            set { _URL = value; }
            get { return _URL; }
        }
    }
}