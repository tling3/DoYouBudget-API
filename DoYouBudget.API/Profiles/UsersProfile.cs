﻿using AutoMapper;
using DoYouBudget.API.Models.Domain;
using DoYouBudget.API.Models.Dto;

namespace DoYouBudget.API.Profiles
{
    /// <summary>
    /// Users Mapper Profile
    /// </summary>
    public class UsersProfile : Profile
    {
        /// <summary>
        /// Create Maps
        /// </summary>
        public UsersProfile()
        {
            CreateMap<UsersModel, UsersReadDto>();
        }
    }
}
