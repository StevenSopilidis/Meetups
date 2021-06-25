using System;
using System.Linq;
using Application.Dtos;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            string currentUserId = null;
            CreateMap<Post, Post>();
            CreateMap<AppUser, UserDto>();
            CreateMap<Post, PostDto>()
                .ForMember(d => d.Saved, opt => opt.MapFrom(s => s.Saves.Any(saves => 
                    saves.AppUserId == new Guid(currentUserId)
                )))
                .ForMember(d => d.Retweeted, opt => opt.MapFrom(s => s.Retweets.Any(r => 
                    r.AppUserId == new Guid(currentUserId)
                )))
                .ForMember(d => d.Liked, opt => opt.MapFrom(s => s.PostLikes.Any(r => 
                    r.AppUserId == new Guid(currentUserId)
                )));
            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.Liked, opt => opt.MapFrom(s => s.CommentLikes.Any(cl => 
                    cl.UserId == new Guid(currentUserId)
                )));
            CreateMap<AppUser, ProfileDto>();
            CreateMap<Retweet, RetweetDto>();
        }
    }
}