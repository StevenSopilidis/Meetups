using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.Activities
{
    public class UpdateAttendance
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;

            }
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities
                    .Include(a => a.Attendees)
                    .ThenInclude(aa => aa.AppUser)
                    .SingleOrDefaultAsync(a => a.Id == request.Id);
                
                if(activity == null) return null;

                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == _userAccessor.GetUsername());
                if(user == null) return null;

                var hostUsername = activity.Attendees.FirstOrDefault(a => a.IsHost)?.AppUser?.UserName;
                var attendance = activity.Attendees.FirstOrDefault(a => a.AppUser.UserName == user.UserName);

                if(attendance != null && user.UserName == hostUsername)
                    activity.IsCancelled = !activity.IsCancelled;

                if(attendance != null && user.UserName != hostUsername)
                    activity.Attendees.Remove(attendance);

                if(attendance == null)
                {
                    attendance = new ActivityAttendee
                    {
                        AppUser = user,
                        Activity = activity,
                        IsHost= false
                    };
                    activity.Attendees.Add(attendance);
                }
                if(await _context.SaveChangesAsync() > 0)
                    return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Problem updating attendance");
            }
        }
    }
}