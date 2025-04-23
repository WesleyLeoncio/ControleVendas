using AutoMapper;
using ControleVendas.Modules.Categoria.Models.Mapper;
using ControleVendas.Modules.Categoria.Repository.Interfaces;
using ControleVendas.Modules.Categoria.Service;
using ControleVendas.Modules.Categoria.Service.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Produto.Models.Mapper;
using ControleVendasTeste.Config;
using Moq;

namespace ControleVendasTeste.Modules.Categoria.Config;

public class CategoriaConfigTest
{
    public ICategoriaService CategoriaService;
    public Mock<IUnitOfWork> MockUof;

    public CategoriaConfigTest()
    {
        MockUof = new Mock<IUnitOfWork>();
        Mock<ICategoriaRepository> mockCategoriaRepository = new Mock<ICategoriaRepository>();
        var mapper = AutoMapperConfig.Configure(new List<Profile>()
        {
            new CategoriaMapper(), new ProdutoMapper()
        });

        CategoriaService = new CategoriaService(MockUof.Object, mapper);
        MockUof.Setup(u => u.CategoriaRepository).Returns(mockCategoriaRepository.Object);
    }
}