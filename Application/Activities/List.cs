using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.Activities
{

    //for getting all the activities
    public class List
    {
        public class Query : IRequest<Result<PagedList<ActivityDto>>> { 
            public ActivitiesParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<ActivityDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mapper = mapper;
                _context = context;
            }

            //Mediator handler
            public async Task<Result<PagedList<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Activities
                    .Where(a => a.Date >= request.Params.StartDate)
                    .OrderBy(a => a.Date)
                    .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider, 
                        new { currentUsername = _userAccessor.GetUsername() }
                    )
                    .AsQueryable();

                if(request.Params.IsGoing && !request.Params.IsHost)
                {
                    query = query.Where(
                        a => a.Attendees.Any(u => u.Username == _userAccessor.GetUsername())
                    );
                }
                
                if(!request.Params.IsGoing && request.Params.IsHost)
                {
                    query = query.Where(
                        a => a.HostUsername == _userAccessor.GetUsername()
                    );
                }

                return Result<PagedList<ActivityDto>>.Success(
                    await PagedList<ActivityDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize)
                );
            }
        }
    }
}