//Pagination
//returns items for certain page
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Helper
{
    //T = any Type, what we use e.g. MembersDto
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            //count = number of entries in db e.g. count=10/pageSize=5) = 2
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        //Needed vars for pagination
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize{ get; set; }
        public int TotalCount { get; set; }


        //static method to be called from anywhere
        //returns page
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize){
            //get total numbers of returns
            var count = await source.CountAsync();
            //Store Objects to show into items list 
            //skip = skips the number of items. eg pageNumber=0, pageSize=5 Skip(0) = nothing will be skiped, skip(5) = the first 5 items will be skiped
            //take: Take the follwing number of objects (e.g. pageSize = 5) take 5 items
            var items = await source.Skip((pageNumber -1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

    }
}