using AutoMapper;

namespace BookOpinions.Common.Mapping
{
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
