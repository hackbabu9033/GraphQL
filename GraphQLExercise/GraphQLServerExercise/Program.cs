using GraphQLServer.GraphQLType;
using GraphQLServerExercise.GraphQLQuery;

namespace GraphQLServerExercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                  .AddGraphQLServer()
                  .AddMutationType<Mutation>()
                  .AddQueryType<Query>();

            var app = builder.Build();
            app.MapGraphQL();
            app.Run();
        }
    }
}