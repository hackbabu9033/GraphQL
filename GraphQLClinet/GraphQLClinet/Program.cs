// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
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
var body = JsonConvert.SerializeObject(requestModel);
var httpContent = new StringContent(body, Encoding.UTF8, "application/json");
//var uri = "http://localhost:7181/graphql";
var uri = "http://localhost:5031/graphql";
var result = await httpClinet.PostAsync(uri, httpContent);
Console.WriteLine(result);