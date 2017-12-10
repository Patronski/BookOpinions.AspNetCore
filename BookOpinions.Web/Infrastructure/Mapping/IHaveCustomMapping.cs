using AutoMapper;

namespace BookOpinions.Services
{
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
