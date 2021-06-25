using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.Followers
{
    public class FollowToggle
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string TargetUsername { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == _userAccessor.GetUsername());
                var target = await _context.Users.FirstOrDefaultAsync(t => t.UserName == request.TargetUsername);
                if(target == null) return null;

                var following = await _context.UserFollowings.FindAsync(user.Id, target.Id);
                if(following != null)
                { 
                    _context.UserFollowings.Remove(following);  
                }else 
                {
                    following = new UserFollowing
                    {
                        Observer= user,
                        Target= target
                    };
                    _context.UserFollowings.Add(following);
                }
                var result = await _context.SaveChangesAsync() > 0;
                if(result) return Result<Unit>.Success(Unit.Value);
                return Result<Unit>.Failure("Failed to update following");
            }
        }
    }
}