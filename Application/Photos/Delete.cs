using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string publicId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IPhotoAccessor _photoAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .Include(usr => usr.Photos)
                    .FirstOrDefaultAsync(usr => usr.UserName == _userAccessor.GetUserName());
                if (user == null) return null;
                var userPhoto = user.Photos.FirstOrDefault(p => p.Id == request.publicId);
                if (userPhoto == null) return null;
                if (userPhoto.IsMain) return Result<Unit>.Failure("You cannot delete your main photo");

                var resultDeletingPhoto = _photoAccessor.DeletePhoto(userPhoto.Id);
                if (resultDeletingPhoto == null) return Result<Unit>.Failure("Problem deleting photo from Cloudinary");
                user.Photos.Remove(userPhoto);
                var result = await _context.SaveChangesAsync() > 0;
                if (result) return Result<Unit>.Success(Unit.Value);
                return Result<Unit>.Failure("Problem deleting photo from API");
            }
        }
    }
}