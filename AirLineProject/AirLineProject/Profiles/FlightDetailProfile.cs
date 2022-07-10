using AirLineProject.Dtos;
using AirLineProject.Models;
using AutoMapper;

namespace AirLineProject.Profiles
{
    public class FlightDetailProfile:Profile
    {
        public FlightDetailProfile()
        {
            this.CreateMap<FlightDetailCreateDTO, FlightDetail>();
            this.CreateMap<FlightDetail, FlightDetailReadDTO>();

        }
    }
}
