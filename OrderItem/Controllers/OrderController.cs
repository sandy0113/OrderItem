using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderItem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderItem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
      
        [HttpPost("{menuitemid}")]
        public Cart CartDetails(int menuitemid)
        {

            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44306/api/");
                var responseTask = client.GetAsync(string.Format("MenuItem/{0}", menuitemid));
                responseTask.Wait();
                var result = responseTask.Result;
                var responseBody = result.Content.ReadAsStringAsync();
                Dictionary<string, string> Response = JsonConvert.DeserializeObject<Dictionary<string,string>>(responseBody.Result);

                if (result.IsSuccessStatusCode)
                {
                    Cart cart = new Cart();
                    cart.Id = 1;
                    cart.userId = 1;
                    cart.menuItemId = menuitemid;
                    cart.menuItemName = Response["name"].ToString();

                    return cart;
                }
                else
                {
                    return null;
                }


            }

        }



        
    }
}

