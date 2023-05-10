using AutoMapper;
using AutoMapper.QueryableExtensions;
using clar2.Application.Common.Interfaces;
using clar2.Application.Common.Mappings;
using clar2.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace clar2.Application.Notes.Queries.GetCollaboratedNotesWithPagination;

public sealed record GetCollaboratedNotesWithPaginationQuery
  (string UserId, int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<CollaboratedNoteBriefDto>>;

public class
  GetCollaboratedNotesWithPaginationQueryHandler : IRequestHandler<GetCollaboratedNotesWithPaginationQuery,
    PaginatedList<CollaboratedNoteBriefDto>> {
  private readonly IApplicationDbContext _context;
  private readonly IMapper _mapper;

  public GetCollaboratedNotesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper) {
    _context = context;
    _mapper = mapper;
  }
  
  public async Task<PaginatedList<CollaboratedNoteBriefDto>> Handle(GetCollaboratedNotesWithPaginationQuery request, CancellationToken cancellationToken) {
    var result = await _context.Notes
      .Where(n => n.Collaborators.Any(c => c.CollaboratorId == request.UserId))
      .OrderByDescending(n => n.LastModified)
      .ProjectTo<CollaboratedNoteBriefDto>(_mapper.ConfigurationProvider)
      .PaginatedListAsync(request.PageNumber, request.PageSize);
    // For the class to return proper user permissions,
    // the current user id is required
    result.Items.ForEach(n => n.CurrentUserId = request.UserId);
    return result;
  }
}
