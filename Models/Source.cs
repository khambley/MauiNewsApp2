using System;
using System.Text.Json.Serialization;

namespace MauiNewsApp2.Models
{
	public class Source
	{
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

