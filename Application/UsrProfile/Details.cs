using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UsrProfile
{
    public class Details
    {
        public class Query : IRequest<Result<UserProfile>>
        {
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<UserProfile>>
        {
            private readonly IMapper _mapper;
            private readonly DataContext _context;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<UserProfile>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .ProjectTo<UserProfile>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(u => u.Username == request.Username);
                if (user == null) return null;

                return Result<UserProfile>.Success(user);
            }
        }
    }
}