using GraphQL;
using GraphQL.Types;
using GraphQL.SystemTextJson;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using GraphQL.Types.Relay.DataObjects;

namespace GraphQLExercise
{
    
    internal class Program
    {
        static List<Product> Products { get; set; } = new List<Product>();
        // 練習：針對Product使用GraphQL完成以下操作
        // 1. 取得單筆Product(丟Id查找)
        // 2. 多筆查詢(取得包含分頁)
        // 3. 新增Product
        static async Task Main(string[] args)
        {
            // retreive single Product
            init();
            #region GraphType First
            var graphQuerySche = new Schema { Query = new ProductQuery() };
            var result = await graphQuerySche.ExecuteAsync(_ => _.Query = $"{{ product(pid: 50) {{ id name isAvailableForSale isGiftCard }} }}");
            // 如果正常回傳，則資料會回傳json字串，包含{data:{product:{id : xx , name : xx}}
            var product = JsonConvert.DeserializeObject<GraphQLResponse<ProductResult>>(result);
            var test = product.data.product;
            #endregion

            #region Schema First
            var schema = Schema.For(@"
                type Product{
                    Id : Int
                    Name : String
                    IsAvailableForSale : Boolean
                    isGiftCard : Boolean
                }

                type Query {
                    product(id: Int): Product
                }
            ", sb => {
                sb.Types.Include<Query>();
            });
            // note : 查詢裡面的欄位名稱似乎都只能用小寫？
            var result2 = await schema.ExecuteAsync(_ => _.Query = $"{{ product(id: 50) {{ id name isAvailableForSale isGiftCard }} }}");
            #endregion

            // retreive Product pagination


            // 新增Product
        }

        private static void init()
        {
            for (int i = 0; i < 100; i++)
            {
                Products.Add(new Product { 
                    Id = i,
                    Name = i.ToString(),
                    IsAvailableForSale = true,
                    isGiftCard= true,
                });
            }
        }

        #region 取得回傳資料

        public class GraphQLResponse<T>where T : class
        {
            public T data { get; set; }
        }

        public class ProductResult
        {
            public Product product { get; set; }

        }

        #endregion

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsAvailableForSale { get; set; }
            public bool isGiftCard { get; set; }

        }

        #region GraphType
        public class ProductType: ObjectGraphType<Product>
        {
            public ProductType()
            {
                Field<IntGraphType>("id");
                Field<StringGraphType>("name");
                Field<BooleanGraphType>("isAvailableForSale");
                Field<BooleanGraphType>("isGiftCard");
            }
        }

        public class ProductQuery : ObjectGraphType
        {
            public ProductQuery()
            {
                Field<ProductType>("Product")
                    .Argument<int>("pid")
                    .Resolve(cxt =>
                    {
                        var ProductId = cxt.GetArgument<int>("pid");
                        return Products.First(p => p.Id == ProductId); 
                    });
                //Field<ProductType>("Products")
                //    .Argument<int?>("after")
                //    .Argument<int?>("before")
                //    .Argument<int?>("first")
                //    .Argument<int?>("last")
                //    .Resolve(cxt =>
                //    {
                //        var productQuery = Products.AsQueryable();
                //        var after = cxt.GetArgument<int?>("after");
                //        var before = cxt.GetArgument<int?>("before");
                //        var first = cxt.GetArgument<int?>("first");
                //        var last = cxt.GetArgument<int?>("last");
                //        // 以id作為cursor
                //        if (after.HasValue)
                //        {
                //            productQuery.Where(x => x.Id >= after.Value);
                //        }
                //        if (before.HasValue)
                //        {
                //            productQuery.Where(x => x.Id <= before.Value);
                //        }

                //        if (first.HasValue)
                //        {
                //            productQuery.Take(first.Value);
                //        }
                //        else if (last.HasValue)
                //        {
                //            productQuery.TakeLast(last.Value);
                //        }
                //        // 回傳資料可能要以edge作範例
                //        return productQuery.ToList();
                //    });
                //Field<Product>("Products")
                //    .Argument<int>("");
            }
        }

        #endregion

        #region schema
        public class Query
        {
            [GraphQLMetadata("product")]
            public Product GetProduct(int id)
            {
                return Products.First(x => x.Id == id);
            }
        }

        #endregion 
    }



}