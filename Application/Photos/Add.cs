using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.Photos
{
    public class Add
    {
        public class Command : IRequest<Result<Photo>>
        {
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Photo>>
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

            public async Task<Result<Photo>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .Include(u => u.Photos)
                    .FirstOrDefaultAsync(u => u.UserName == _userAccesor.GetUsername());
                if(user == null) return null;

                var photoUploadResult = await _photoAccessor.UploadPhoto(request.File);
                if(photoUploadResult == null) return null;
                var  photo = new Photo
                {
                    Id= photoUploadResult.PublicId,
                    Url= photoUploadResult.Url
                };
                if(!user.Photos.Any(p => p.IsMain)) photo.IsMain = true;
                user.Photos.Add(photo);
                var result = await _context.SaveChangesAsync() > 0;
                if(result) return Result<Photo>.Success(photo);
                return Result<Photo>.Failure("Could not upload photo");
            }
        }
    }
}