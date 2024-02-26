using System.Diagnostics.Metrics;
using AutoMapper;
using DayEndStatusAPI.Dtos;
using DayEndStatusAPI.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Master.Service
{
    /// <summary>
    /// Auto mapper
    /// </summary>
    public class AutoMappers : Profile
    {
        /// <summary>
        /// Consructor
        /// </summary>
        public AutoMappers()
        {
            
            CreateMap<StatusMessage, StatusMessageDTO>().ReverseMap();
           

            
        }
    }
}
