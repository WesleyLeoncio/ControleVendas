using AutoMapper;
using controle_vendas.infra.exceptions.custom;
using controle_vendas.modules.categoria.model.entity;
using controle_vendas.modules.common.unit_of_work.interfaces;
using controle_vendas.modules.fornecedor.model.entity;
using controle_vendas.modules.produto.models.entity;
using controle_vendas.modules.produto.models.request;
using controle_vendas.modules.produto.models.response;
using controle_vendas.modules.produto.service.interfaces;

namespace controle_vendas.modules.produto.service;

public class ProdutoService : IProdutoService
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutoService(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    public async Task<ProdutoResponse> CreateCategoria(ProdutoRequest request)
    {
        await CheckCategoriaFornecedor(request);
        Produto entity = _uof.ProdutoRepository.Create(_mapper.Map<Produto>(request));
        await _uof.Commit();
        return _mapper.Map<ProdutoResponse>(entity);
    }

    public async Task<ProdutoResponse> GetCategoriaById(int id)
    {
        return _mapper.Map<ProdutoResponse>(await CheckProduto(id));
    }

    private async Task<Produto> CheckProduto(int id)
    {
        return await _uof.ProdutoRepository.GetAsync(p => p.Id == id) ??
               throw new NotFoundException("Produto não encontrado!");
    }

    private async Task CheckCategoriaFornecedor(ProdutoRequest request)
    {
        Categoria? categoria = await
            _uof.CategoriaRepository.GetAsync(c => c.Id == request.CategoriaId);
        
        if (categoria == null)
        {
            throw new NotFoundException("Categoria não encontrada!");
        }

        Fornecedor? fornecedor = await
            _uof.FornecedorRepository.GetAsync(f => f.Id == request.FornecedorId);
        
        if (fornecedor == null)
        {
            throw new NotFoundException("Fornecedor não encontrado!");
        }
    }
}