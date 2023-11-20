using System.Reflection;

namespace neatbook.Web.Infrastructure;

public static class WebApplicationExtensions {
  public static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group) {
    var groupName = group.GetType().Name;
    // Change the names used in routes
    // from PascalCase class names to dash-case
    var groupNameDashCase = string
      .Concat(groupName
        .Select((x, i) => char.IsUpper(x) && i != 0 ? $"-{x}" : x.ToString()))
      .ToLower();

    return app
      .MapGroup($"/api/{groupNameDashCase}")
      .WithGroupName(groupName)
      .WithTags(groupName)
      .WithOpenApi();
  }

  public static WebApplication MapEndpoints(this WebApplication app) {
    var endpointGroupType = typeof(EndpointGroupBase);

    var assembly = Assembly.GetExecutingAssembly();

    var endpointGroupTypes = assembly.GetExportedTypes()
      .Where(t => t.IsSubclassOf(endpointGroupType));

    foreach (var type in endpointGroupTypes) {
      if (Activator.CreateInstance(type) is EndpointGroupBase instance) {
        instance.Map(app);
      }
    }

    return app;
  }
}
