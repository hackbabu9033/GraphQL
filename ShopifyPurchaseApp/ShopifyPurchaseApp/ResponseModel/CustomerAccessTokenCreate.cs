using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyPurchaseApp.ResponseModel
{
    internal class CustomerAccessTokenCreate
    {
       
        public Token customerAccessToken { get; set; }

        public ErrorRespone customerUserErrors { get;set; }

        public class Token
        {
            public string AccessToken { get; set; }
            public DateTimeOffset ExpiresAt { get; set; }
        }
        public class ErrorRespone
        {
            
        }
    }

   
}
