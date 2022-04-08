namespace SaveAnAnimal.Modules;

public interface IModule
{
	void RegisterModule(IServiceCollection services);
	void MapEndpoints(IEndpointRouteBuilder app);
}
