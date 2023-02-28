using AutoMapper;
using MovieCharactersAPI.Models;
using MovieCharactersAPI.Models.Dtos.FranchiseDtos;

namespace MovieCharactersAPI.Profiles
{
    public class FranchiseProfile: Profile
    {
        public FranchiseProfile() 
        { 
            CreateMap<CreateFranchiseDto, Franchise> ();
            CreateMap<Franchise, FranchiseDto>()
                .ForMember(dto => dto.Movies, options =>
                options.MapFrom(franchiseDomain => franchiseDomain.Movies.Select(movie => $"{movie.Title}").ToList()));
            CreateMap<EditFranchiseDto, Franchise>();
        }
    }
}
