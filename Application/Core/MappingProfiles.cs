using System.Linq;
using Application.Activities;
using Application.Comments;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        //create the automapper maps between two objects
        public MappingProfiles()
        {
            //passed as parameter
            string currentUsername = null;

            CreateMap<Activity, Activity>();
            CreateMap<Activity, ActivityDto>()
                .ForMember(d => d.HostUsername, opt => opt.MapFrom(
                    s => s.Attendees.FirstOrDefault(a => a.IsHost).AppUser.UserName
                ));
            CreateMap<ActivityAttendee, AttendeeDto>()
                .ForMember(d => d.DisplayName, opt => opt.MapFrom(s =>  s.AppUser.DisplayName))
                .ForMember(d => d.Username, opt => opt.MapFrom(s =>  s.AppUser.UserName))
                .ForMember(d => d.Bio, opt => opt.MapFrom(s =>  s.AppUser.Bio))
                .ForMember(d => d.Image, opt=> opt.MapFrom(s => s.AppUser.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(d => d.FollowingCount, opt => opt.MapFrom(s => s.AppUser.Followings.Count))
                .ForMember(d => d.FollowersCount, opt => opt.MapFrom(s => s.AppUser.Followers.Count))
                .ForMember(d => d.Following, opt => opt.MapFrom(s => s.AppUser.Followers.Any(f => f.Observer.UserName == currentUsername)));

            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(d => d.Image, opt=> opt.MapFrom(s => s.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(d => d.FollowingCount, opt => opt.MapFrom(s => s.Followings.Count))
                .ForMember(d => d.FollowersCount, opt => opt.MapFrom(s => s.Followers.Count))
                .ForMember(d => d.Following, opt => opt.MapFrom(s => s.Followers.Any(f => f.Observer.UserName == currentUsername)));

            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.Username, opt => opt.MapFrom(s => s.Author.UserName))
                .ForMember(d => d.DisplayName, opt => opt.MapFrom(s => s.Author.DisplayName))
                .ForMember(d => d.Image, opt => opt.MapFrom(s => s.Author.Photos.FirstOrDefault(p => p.IsMain).Url));

            CreateMap<ActivityAttendee, Application.Profiles.UserActivityDto>()
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Activity.Title))
                .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Activity.Category))
                .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Activity.Date))
                .ForMember(d => d.HostUsername, opt => opt.MapFrom(s => s.Activity.Attendees.FirstOrDefault(aa => 
                    aa.IsHost
                ).AppUser.UserName));
        }
    }
}