﻿using Application_Core.Model;
using Infrastructure.EF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Repository.ReactionCommentRepository
{
    public interface IReactionRepository
    {
        Task AddReactionAsync(Reaction reaction);
        Task DeleteAsync(Reaction reaction);
        Task<int> GetReactionsCountByPostAsync(Post post);
    }
}
