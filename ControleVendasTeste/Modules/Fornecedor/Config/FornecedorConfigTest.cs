using AutoMapper;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Fornecedor.Models.Mapper;
using ControleVendas.Modules.Fornecedor.Repository.Interfaces;
using ControleVendas.Modules.Fornecedor.Service;
using ControleVendas.Modules.Fornecedor.Service.Interfaces;
using ControleVendasTeste.Config;
using Moq;

namespace ControleVendasTeste.Modules.Fornecedor.Config;

public class FornecedorConfigTest
{
    public IFornecedorService FornecedorService;
    public Mock<IUnitOfWork> MockUof;

    public FornecedorConfigTest()
    {
        MockUof = new Mock<IUnitOfWork>();
        Mock<IFornecedorRepository> mockFornecedorRepository = new Mock<IFornecedorRepository>();
        var mapper = AutoMapperConfig.Configure(new List<Profile>()
        {
            new FornecedorMapper()
        });

        FornecedorService = new FornecedorService(MockUof.Object, mapper);
        MockUof.Setup(u => u.FornecedorRepository).Returns(mockFornecedorRepository.Object);
    }
}