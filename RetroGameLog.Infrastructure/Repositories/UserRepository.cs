﻿using Microsoft.EntityFrameworkCore;
using RetroGameLog.Domain.Users;
using RetroGameLog.Infrastructure.DatabaseContext;

namespace RetroGameLog.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(RetroGameLogDbContext context) : base(context)
    {

    }

    public async Task<bool> IsEmailOverlaped(string email)
    {
        return await DbContext
            .Set<User>()
            .AnyAsync(x => x.Email.Value == email);
    }

    public async Task<bool> IsUsernameOverlaped(string username)
    {
        return await DbContext
            .Set<User>()
            .AnyAsync(x => x.Username.Value == username);
    }
}
