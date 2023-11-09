using neatbook.Application.Common.Interfaces;
using neatbook.Application.Common.Mappings;
using neatbook.Application.Common.Models;

namespace neatbook.Application.Notes.Queries.GetArchivedAuthoredNotesWithPagination; 

public sealed record GetArchivedAuthoredNotesWithPaginationQuery
  (string UserId, int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<AuthoredNoteBriefDto>>;

public class
  GetArchivedAuthoredNotesWithPaginationQueryHandler : IRequestHandler<GetArchivedAuthoredNotesWithPaginationQuery,
    PaginatedList<AuthoredNoteBriefDto>> {
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetArchivedAuthoredNotesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper) {
    _context = context;
    _mapper = mapper;
  }
  
  public async Task<PaginatedList<AuthoredNoteBriefDto>> Handle(GetArchivedAuthoredNotesWithPaginationQuery request, CancellationToken cancellationToken) {
    return await _context.Notes
      .Where(n => n.OwnerId == request.UserId && n.IsArchived == true)
      .OrderByDescending(n => n.LastModified)
      .ProjectTo<AuthoredNoteBriefDto>(_mapper.ConfigurationProvider)
      .PaginatedListAsync(request.PageNumber, request.PageSize);
  }
}
