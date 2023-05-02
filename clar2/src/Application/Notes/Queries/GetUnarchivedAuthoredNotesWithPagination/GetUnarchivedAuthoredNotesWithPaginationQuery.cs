using AutoMapper;
using AutoMapper.QueryableExtensions;
using clar2.Application.Common.Interfaces;
using clar2.Application.Common.Mappings;
using clar2.Application.Common.Models;
using clar2.Domain.Notes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace clar2.Application.Notes.Queries.GetUnarchivedAuthoredNotesWithPagination;

public sealed record GetUnarchivedAuthoredNotesWithPaginationQuery
  (string UserId, int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<AuthoredNoteBriefDto>>;

public class
  GetUnarchivedAuthoredNotesWithPaginationQueryHandler : IRequestHandler<GetUnarchivedAuthoredNotesWithPaginationQuery,
    PaginatedList<AuthoredNoteBriefDto>> {
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetUnarchivedAuthoredNotesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper) {
    _context = context;
    _mapper = mapper;
  }
  
  public async Task<PaginatedList<AuthoredNoteBriefDto>> Handle(GetUnarchivedAuthoredNotesWithPaginationQuery request, CancellationToken cancellationToken) {
    return await _context.Notes
      .Where(n => n.OwnerId == request.UserId && n.IsArchived == false)
      .OrderByDescending(n => n.LastModified)
      .ProjectTo<AuthoredNoteBriefDto>(_mapper.ConfigurationProvider)
      .PaginatedListAsync(request.PageNumber, request.PageSize);
  }
}
