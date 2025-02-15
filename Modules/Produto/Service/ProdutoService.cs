using AutoMapper;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Common.Pagination;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
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
        Models.Entity.Produto entity = _uof.ProdutoRepository.Create(_mapper.Map<Models.Entity.Produto>(request));
        await _uof.Commit();
        return _mapper.Map<ProdutoResponse>(entity);
    }

    public async Task<ProdutoResponse> GetProdutoById(int id)
    {
        return _mapper.Map<ProdutoResponse>(await CheckProduto(id));
    }

    public async Task<ProdutoPaginationResponse> GetAllFilterProdutos(ProdutoFiltroRequest filtroRequest)
    {
        IPagedList<Models.Entity.Produto> produtos = await
            _uof.ProdutoRepository.GetAllFilterPageableAsync(filtroRequest);
        ProdutoPaginationResponse produtoPg = new ProdutoPaginationResponse(
            _mapper.Map<IEnumerable<ProdutoResponse>>(produtos),
            MetaData<Models.Entity.Produto>.ToValue(produtos));
        return produtoPg;
    }

    public async Task<ProdutoResponse> UpdateProduto(int id, ProdutoRequest request)
    {
        Models.Entity.Produto produto = await CheckProduto(id);
        if (produto.Nome != request.Nome) await CheckNameExists(request.Nome);
        await CheckCategoriaFornecedor(request);
        _mapper.Map(request, produto);
        Models.Entity.Produto update = _uof.ProdutoRepository.Update(produto);
        await _uof.Commit();
        return _mapper.Map<ProdutoResponse>(update);
    }

    public async Task<ProdutoResponse> DeleteProduto(int id)
    {
        Models.Entity.Produto produto = await CheckProduto(id);
        _uof.ProdutoRepository.Delete(produto);
        await _uof.Commit();
        return _mapper.Map<ProdutoResponse>(produto);
    }

    private async Task<Models.Entity.Produto> CheckProduto(int id)
    {
        return await _uof.ProdutoRepository.GetAsync(p => p.Id == id) ??
               throw new NotFoundException("Produto não encontrado!");
    }

    private async Task CheckCategoriaFornecedor(ProdutoRequest request)
    {
        Categoria.Models.Entity.Categoria? categoria = await
            _uof.CategoriaRepository.GetAsync(c => c.Id == request.CategoriaId);

        if (categoria == null)
        {
            throw new NotFoundException("Categoria não encontrada!");
        }

        Fornecedor.Models.Entity.Fornecedor? fornecedor = await
            _uof.FornecedorRepository.GetAsync(f => f.Id == request.FornecedorId);

        if (fornecedor == null)
        {
            throw new NotFoundException("Fornecedor não encontrado!");
        }
    }

    private async Task CheckNameExists(string? nome)
    {
        Models.Entity.Produto? produto = await _uof.ProdutoRepository.GetAsync(p => p.Nome == nome);
        if (produto != null)
        {
            throw new KeyDuplicationException("Já existe um produto com este nome!");
        }
    }
}