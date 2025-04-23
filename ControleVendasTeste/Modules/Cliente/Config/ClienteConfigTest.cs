using AutoMapper;
using ControleVendas.Modules.Cliente.Models.Mapper;
using ControleVendas.Modules.Cliente.Repository.Interfaces;
using ControleVendas.Modules.Cliente.Service;
using ControleVendas.Modules.Cliente.Service.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendasTeste.Config;
using Moq;

namespace ControleVendasTeste.Modules.Cliente.Config;

public class ClienteConfigTest
{
    public IClienteService ClienteService;
    public Mock<IUnitOfWork> MockUof;

    public ClienteConfigTest()
    {
        MockUof = new Mock<IUnitOfWork>();
        Mock<IClienteRepository> mockClienteRepository = new Mock<IClienteRepository>();
        var mapper = AutoMapperConfig.Configure(new List<Profile>()
        {
            new ClienteMapper()
        });

        ClienteService = new ClienteService(MockUof.Object, mapper);
        MockUof.Setup(u => u.ClienteRepository).Returns(mockClienteRepository.Object); 
    }
}