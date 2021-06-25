using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.Photos
{
    public class Delete
    {
        public class Command: IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccesor;
            public readonly IPhotoAccessor _photoAccessor;
            public Handler(DataContext context, IUserAccessor userAccesor, IPhotoAccessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
                _userAccesor = userAccesor;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .Include(u => u.Photos)
                    .FirstOrDefaultAsync(u => u.UserName == _userAccesor.GetUsername());

                if(user == null) return null;

                var photo = user.Photos.FirstOrDefault(p => p.Id == request.Id);
                if(photo == null) return null;
                if(photo.IsMain) return Result<Unit>.Failure("Cannot delete your main photo");
                var result = await _photoAccessor.DeletePhoto(photo.Id);
                if(result == null) return Result<Unit>.Failure("Could not delete photo");
                user.Photos.Remove(photo);
                var success = await _context.SaveChangesAsync() > 0;
                if(success) return Result<Unit>.Success(Unit.Value);
                return Result<Unit>.Failure("Could not delete photo");
            }
        }
    }
}