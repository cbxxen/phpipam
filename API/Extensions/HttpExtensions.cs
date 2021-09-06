//Pagination
//returns the needed HTTP Request Header

using System.Text.Json;
using API.Helper;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        //static method to add Pagination Header!
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages){
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            //Return in CamelCase
            var options = new JsonSerializerOptions{
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            //Pagination = Header Name
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
            //expose the header, Access.. must be, Pagination = Pagination equals the one on top
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}