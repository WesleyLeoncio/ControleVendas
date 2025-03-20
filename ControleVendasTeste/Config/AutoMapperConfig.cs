using AutoMapper;

namespace ControleVendasTeste.Config;

public static class AutoMapperConfig
{
    public static IMapper Configure(Profile profile)
    {
        var config = new MapperConfiguration(cfg =>
        {
              cfg.AddProfile(profile);
        });

        return config.CreateMapper();
    }
}