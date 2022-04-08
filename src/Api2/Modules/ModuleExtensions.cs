namespace SaveAnAnimal.Modules;

public static class ModulesExtensions
{
	public static IServiceCollection RegisterModule(this IServiceCollection services, IModule module)
	{
		module.RegisterModule(services);
		return services;
	}

	public static IEndpointRouteBuilder MapModuleEndpoints(this IEndpointRouteBuilder builder, IModule module)
	{
		module.MapEndpoints(builder);
		return builder;
	}

	// this could also be added into the DI container
	static readonly List<IModule> registeredModules = new List<IModule>();

	public static IServiceCollection AddModules(this IServiceCollection services)
	{
		var modules = DiscoverModules();
		foreach (var module in modules)
		{
			services.RegisterModule(module);
			registeredModules.Add(module);
		}
		return services;
	}

	public static WebApplication MapEndpoints(this WebApplication app)
	{
		foreach (var module in registeredModules)
		{
			module.MapEndpoints(app);
		}
		return app;
	}

	private static IEnumerable<IModule> DiscoverModules()
	{
		return typeof(IModule).Assembly
			.GetTypes()
			.Where(p => p.IsClass && p.IsAssignableTo(typeof(IModule)))
			.Select(Activator.CreateInstance)
			.Cast<IModule>();
	}
}