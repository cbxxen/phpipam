using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helper;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository

    {
        //Get Data Context
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                //Get User where UserName equals username given to method
                .Where(x => x.UserName == username)
                //get Mapping Configuration to map data
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        //get members, including filtering
        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            //create Query for PagedList; AsQeryable() = allows to filter by
           var query = _context.Users.AsQueryable();
            //do not return own user query
            query = query.Where(u => u.UserName != userParams.CurrentUsername);
            //do not return same gender/gender set by user
            query = query.Where(U => U.Gender == userParams.Gender);     

            //set min age 
            var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
            //set max age
            var maxDob = DateTime.Today.AddYears(-userParams.MinAge);
            //query to filter out users not in age range
            query = query.Where(x => x.DateOfBirth >= minDob && x.DateOfBirth <= maxDob);

            //return paged List. sends query to MemberDto(Mapping it to MemberDto)
            return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking(), userParams.PageNumber, userParams.PageSize);
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
            .Include(p => p.Photos)
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            //check that the changes made are bigger than 0 (zero means no changes)
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            //mark user as modified
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}