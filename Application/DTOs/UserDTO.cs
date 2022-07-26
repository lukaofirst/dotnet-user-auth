﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs
{
    public class UserDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? _id { get; set; }
        [Required]
        [EmailAddress]
        public string? email { get; set; }
        [Required]
        [MinLength(7, ErrorMessage = "Your password needs at least 7 characters long!")]
        public string? password { get; set; }
    }
}
