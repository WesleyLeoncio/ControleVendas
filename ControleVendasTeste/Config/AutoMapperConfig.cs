using AutoMapper;

namespace ControleVendasTeste.Config;

public static class AutoMapperConfig
{
    public static IMapper Configure(List<Profile> profiles)
    {
        var config = new MapperConfiguration(cfg =>
        {
            foreach (var profile in profiles)
            {
                cfg.AddProfile(profile);
            }
        });

        return config.CreateMapper();
    }
}