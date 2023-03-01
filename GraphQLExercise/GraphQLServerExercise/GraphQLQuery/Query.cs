using GraphQLServerExercise.Model;

namespace GraphQLServerExercise.GraphQLQuery
{
    public class Query
    {
        public Book GetBook() =>
               new Book
               {
                   Title = "C# in depth.",
                   Author = new Author
                   {
                       Name = "Jon Skeet"
                   }
               };

        // query with Parameter
        public Book QueryBookWithVersion(int version)
        {
            return new Book
            {
                Title = $"C#, Version:{version}",
            };
        }
    }
}
