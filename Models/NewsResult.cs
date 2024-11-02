using System;
using System.Text.Json.Serialization;

namespace MauiNewsApp2.Models
{
	public class NewsResult
	{
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("totalResults")]
        public int TotalResults { get; set; }

        [JsonPropertyName("articles")]
        public List<Article> Articles { get; set; }
    }
}

