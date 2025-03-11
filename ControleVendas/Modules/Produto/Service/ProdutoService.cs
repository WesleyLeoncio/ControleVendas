using AutoMapper;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Common.Pagination;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Fornecedor.Models.Entity;
using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendas.Modules.Produto.Models.Response;
using ControleVendas.Modules.Produto.Service.Interfaces;
using X.PagedList;

namespace ControleVendas.Modules.Produto.Service;

public class ProdutoService : IProdutoService
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutoService(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    public async Task<ProdutoResponse> CreateProduto(ProdutoRequest request)
    {
        await CheckNameExists(request.Nome);
        await CheckCategoriaFornecedor(request);
        ProdutoEntity entity = _uof.ProdutoRepository.Create(_mapper.Map<ProdutoEntity>(request));
        await _uof.Commit();
        return _mapper.Map<ProdutoResponse>(entity);
    }

    public async Task<ProdutoResponse> GetProdutoById(int id)
    {
        return _mapper.Map<ProdutoResponse>(await CheckProduto(id));
    }

    public async Task<ProdutoPaginationResponse> GetAllFilterProdutos(ProdutoFiltroRequest filtroRequest)
    {
        IPagedList<ProdutoEntity> produtos = await
            _uof.ProdutoRepository.GetAllFilterPageableAsync(filtroRequest);
        ProdutoPaginationResponse produtoPg = new ProdutoPaginationResponse(
            _mapper.Map<IEnumerable<ProdutoResponse>>(produtos),
            MetaData<ProdutoEntity>.ToValue(produtos));
        return produtoPg;
    }

    public async Task UpdateProduto(int id, ProdutoRequest request)
    {
        ProdutoEntity produtoEntity = await CheckProduto(id);
        if (produtoEntity.Nome != request.Nome) await CheckNameExists(request.Nome);
        await CheckCategoriaFornecedor(request);
        _mapper.Map(request, produtoEntity);
        _uof.ProdutoRepository.Update(produtoEntity);
        await _uof.Commit();
    }

    public async Task<ProdutoResponse> DeleteProduto(int id)
    {
        ProdutoEntity produtoEntity = await CheckProduto(id);
        _uof.ProdutoRepository.Delete(produtoEntity);
        await _uof.Commit();
        return _mapper.Map<ProdutoResponse>(produtoEntity);
    }

    private async Task<ProdutoEntity> CheckProduto(int id)
    {
        return await _uof.ProdutoRepository.GetAsync(p => p.Id == id) ??
               throw new NotFoundException("Produto não encontrado!");
    }

    private async Task CheckCategoriaFornecedor(ProdutoRequest request)
    {
        CategoriaEntity? categoria = await
            _uof.CategoriaRepository.GetAsync(c => c.Id == request.CategoriaId);

        if (categoria == null)
        {
            throw new NotFoundException("Categoria não encontrada!");
        }

        FornecedorEntity? fornecedor = await
            _uof.FornecedorRepository.GetAsync(f => f.Id == request.FornecedorId);

        if (fornecedor == null)
        {
            throw new NotFoundException("Fornecedor não encontrado!");
        }
    }

    private async Task CheckNameExists(string? nome)
    {
        ProdutoEntity? produto = await _uof.ProdutoRepository.GetAsync(p => p.Nome == nome);
        if (produto != null)
        {
            throw new KeyDuplicationException("Já existe um produto com este nome!");
        }
    }
}