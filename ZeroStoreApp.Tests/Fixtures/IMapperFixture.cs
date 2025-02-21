using AutoMapper;

namespace ZeroStoreApp.Tests.Fixtures;

internal static class IMapperFixture
{
    public static IMapper GetMapper(params Profile[] profiles)
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            profiles.ToList().ForEach(profile => cfg.AddProfile(profile));
        });
        return configuration.CreateMapper();
    }
}
