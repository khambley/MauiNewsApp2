using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace MauiNewsApp2.Models
{
	public partial class Article : ObservableObject
	{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FavoriteIcon))]
        private bool isFavorite;

        [ObservableProperty]
        private DateTime timestamp;

        [Ignore]
        [JsonPropertyName("source")]
        public Source? Source { get; set; }

        public int SourceId { get; set; }

        [JsonPropertyName("author")]
        public string? Author { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("urlToImage")]
        public string? UrlToImage { get; set; }

        [JsonPropertyName("publishedAt")]
        public DateTime PublishedAt { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [Ignore]
        public string FavoriteIcon => IsFavorite ? "heart_filled.png" : "heart_outline.png";



    }
}

