namespace ShopifyPurchaseApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var shopifyApiEndPoint = "https://eat-your-own-dog-food.myshopify.com/api/2022-10/graphql.json";
            var accessToken = string.Empty;
            var httpClient = new HttpClient();

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
            }

            // 陳列商品內容(考慮是否顯式分頁)

            // 將輸入選擇的商品內容放到collection裡面

            // 建立購物車連結，取得checkout url
        }
    }
}