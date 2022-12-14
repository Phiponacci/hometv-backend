using Newtonsoft.Json;

namespace backend.Models.News
{
    public class Channel
    {
        public Title title { get; set; }
        public string link { get; set; }
        public Description description { get; set; }
        public string language { get; set; }
        public string lastBuildDate { get; set; }
        public Copyright copyright { get; set; }
        public Docs docs { get; set; }
        public Image image { get; set; }
        public List<Item> item { get; set; }
    }

    public class Copyright
    {
        [JsonProperty("#cdata-section")]
        public string CdataSection { get; set; }
    }

    public class Description
    {
        [JsonProperty("#cdata-section")]
        public string CdataSection { get; set; }
    }

    public class Docs
    {
        [JsonProperty("#cdata-section")]
        public string CdataSection { get; set; }
    }

    public class Guid
    {
        [JsonProperty("@isPermaLink")]
        public string IsPermaLink { get; set; }

        [JsonProperty("#text")]
        public string Text { get; set; }
    }

    public class Image
    {
        public string title { get; set; }
        public string url { get; set; }
        public string link { get; set; }
    }

    public class Item
    {
        [JsonProperty("@cbc:type")]
        public string CbcType { get; set; }

        [JsonProperty("@cbc:deptid")]
        public string CbcDeptid { get; set; }

        [JsonProperty("@cbc:syndicate")]
        public string CbcSyndicate { get; set; }
        public Title title { get; set; }
        public string link { get; set; }
        public Guid guid { get; set; }
        public string pubDate { get; set; }
        public string author { get; set; }
        public string category { get; set; }
        public Description description { get; set; }
    }

    public class Root
    {
        public Channel channel { get; set; }
    }

    public class Title
    {
        [JsonProperty("#cdata-section")]
        public string CdataSection { get; set; }
    }

}
