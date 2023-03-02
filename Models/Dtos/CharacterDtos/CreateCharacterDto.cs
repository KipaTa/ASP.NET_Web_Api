﻿namespace MovieCharactersAPI.Models.Dtos.CharacterDtos
{
    public class CreateCharacterDto
    {
        public string FullName { get; set; }
        public string? Alias { get; set; }

        public string? Gender { get; set; }
        public string? Picture { get; set; }
    }
}
