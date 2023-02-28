﻿using AutoMapper;
using MovieCharactersAPI.Models.Dtos;
using MovieCharactersAPI.Models;

namespace MovieCharactersAPI.Profiles
{
    public class MovieProfile: Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(dto => dto.Characters, options =>
                options.MapFrom(movieDomain => movieDomain.Characters.Select(character => $"{character.Id}").ToList()));
        }
    }
}