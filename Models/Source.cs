using System;
using System.Text.Json.Serialization;
using SQLite;

namespace MauiNewsApp2.Models
{
	public class Source
	{
        [PrimaryKey, AutoIncrement]
        public int SourceId { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

