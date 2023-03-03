// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

var httpClinet = new HttpClient();
var requestModel = new
{
    query = @"query {
  queryBookWithVersion(version:1){
    title
  }
  book{
    title
    author {
      name
    }
    
  }
  
}"
};

var insertBook = new
{
    input = new Book()
    {
        title = "Title",
        author = new Author()
        {
            name = "Author",
        }
    }
};
var request = new
{
    // query給字串
    // variable給object
    query = @"mutation addBook($input: BookInput!){ 
        addBook(input: $input)
            { 
                title
                author {
                    name
                } 
            }
    }",
    // 這裡不要序列化，因為後面會做一次，這裡做的話會變成多序列化一次
    variables = insertBook,
};
// query
var body = JsonConvert.SerializeObject(requestModel);
var httpContent = new StringContent(body, Encoding.UTF8, "application/json");

// mutation
var addBookBody = JsonConvert.SerializeObject(request);
var addBookHttpContent = new StringContent(addBookBody, Encoding.UTF8, "application/json");
addBookHttpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
var uri = "https://localhost:7181/graphql";
var result = await httpClinet.PostAsync(uri, addBookHttpContent);
var msg = result.Content.ReadAsStringAsync().Result;
Console.WriteLine(result);


public class Book
{
    public string title { get; set; }

    public Author author { get; set; }
}

public class Author
{
    public string name { get; set; }
}