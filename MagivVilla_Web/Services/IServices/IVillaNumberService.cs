﻿using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTOS;
using System.Linq.Expressions;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaNumberService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> DeleteAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaNumberCreateDto dto);       
        Task<T> UpdateAsync<T>(VillaNumberUpdateDto dto);
    }
}
