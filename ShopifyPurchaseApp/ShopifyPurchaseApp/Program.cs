using Newtonsoft.Json;
using System.Text;

namespace ShopifyPurchaseApp
{
    internal class Program
    {
        private static string shopifyApiEndPoint = "https://eat-your-own-dog-food.myshopify.com/api/2022-10/graphql.json";
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            var accessToken = string.Empty;

            // 選擇是否登入
            // Y:打accesstokenApi取得customerAccessToken
            // N:繼續進行
            Console.WriteLine("歡迎使用eatDogFood購物app");
            Console.WriteLine("請決定是否登入(y/n)");
            var isLogin = Console.ReadKey(true);
            
            if (isLogin.ToString() == "y")
            {
                var email = Console.ReadLine();
                var pwd = Console.ReadLine();
                var httpClinet = new HttpClient();
                
            }

            // 陳列商品內容(考慮是否顯示分頁)
            

            // 將輸入選擇的商品內容放到collection裡面

            // 建立購物車連結，取得checkout url
        }

        static string GetAccessToken(string email,string pwd)
        {
            var noMail = string.IsNullOrWhiteSpace(email);
            var noPwd = string.IsNullOrWhiteSpace(pwd);
            if (noMail || noPwd)
            {
                throw new ArgumentNullException($"mail and pwd are required");
            }
            var request = new
            {
                // query給字串
                // variable給object
                query = @"mutation customerAccessTokenCreate($input: CustomerAccessTokenCreateInput!) {
                          customerAccessTokenCreate(input: $input) {
                            customerAccessToken {
                                accessToken
                                expiresAt
                            }
                            customerUserErrors {
                              code
                              field
                              message
                            }
                          }
                        }",
                // 這裡不要序列化，因為後面會做一次，這裡做的話會變成多序列化一次
                variables = new 
                { 
                    input = new{ 
                        email = email,
                        pwd = pwd
                    }
                },
            };
            var addBookBody = JsonConvert.SerializeObject(request);
            var addBookHttpContent = new StringContent(addBookBody, Encoding.UTF8, "application/json");
            var httpClinet = new HttpClient();
            httpClinet.PostAsync(shopifyApiEndPoint, addBookHttpContent);
            return string.Empty;
        }
    }
}