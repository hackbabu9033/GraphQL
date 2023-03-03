using GraphQLServerExercise.GraphQLQuery;
using GraphQLServerExercise.Model;

namespace GraphQLServer.GraphQLType
{
    public class Mutation
    {
        public Book AddBook(Book input)
        {
            Query._books.Add(input);
            return input;
        }
    }
}
