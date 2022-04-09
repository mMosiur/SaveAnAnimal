# SaveAnAnimal

A mock project with an application for a fictional animal welfare organization called *SaveAnAnimal*.

It allows its users to organize pets cared for by the organization as well as volunteers helping it.
Manages who is taking care for what pet and keeps a history of cares for given pet and keep track of the volunteer's record.

Based on a very simple frontend - backend architecture with a **ASP.NET WebAPI** and **BlazorWASM** client.

Also, an awful try at applying DDD to a project that backfired and made the application even worse to maintain and extend.
As such, it is still unfinished. Lesson learned I guess.

## API

To prepare SQLite database for the api in project folder (`src/Api`) use

``` bash
dotnet ef database update
```

To run, use

``` bash
dotnet run
```

To run on specific URL and port, use eg.

``` bash
dotnet run --urls http://localhost:5236
```
