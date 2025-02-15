using AutoMapper;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Categoria.Models.Request;
using ControleVendas.Modules.Categoria.Models.Response;
using ControleVendas.Modules.Categoria.Service.Interfaces;
using ControleVendas.Modules.Common.Pagination;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using X.PagedList;

namespace ControleVendas.Modules.Categoria.Service;

public class CategoriaService : ICategoriaService
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public CategoriaService(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    public async Task<CategoriaResponse> CreateCategoria(CategoriaRequest request)
    {
        await CheckNameExists(request.Nome);
        Models.Entity.Categoria entity =
            _uof.CategoriaRepository.Create(_mapper.Map<Models.Entity.Categoria>(request));
        await _uof.Commit();
        return _mapper.Map<CategoriaResponse>(entity);
    }

    public async Task<CategoriaResponse> GetCategoriaById(int id)
    {
        return _mapper.Map<CategoriaResponse>(await CheckCategoria(id));
    }

    public async Task<CategoriaPaginationResponse> GetAllFilterCategorias(CategoriaFiltroRequest filtroRequest)
    {
        IPagedList<Models.Entity.Categoria> categorias =
            await _uof.CategoriaRepository.GetAllFilterPageableAsync(filtroRequest);
        CategoriaPaginationResponse categoriaPg = new CategoriaPaginationResponse(
            _mapper.Map<IEnumerable<CategoriaResponse>>(categorias),
            MetaData<Models.Entity.Categoria>.ToValue(categorias));
        return categoriaPg;
    }

    public async Task<CategoriaPaginationProdutoResponse> GetAllIncludeProduto(CategoriaFiltroRequest filtroRequest)
    {
        IPagedList<Models.Entity.Categoria> categorias =
            await _uof.CategoriaRepository.GetAllIncludePageableAsync(filtroRequest);
        CategoriaPaginationProdutoResponse categoriaPg = new CategoriaPaginationProdutoResponse(
            _mapper.Map<IEnumerable<CategoriaProdutoResponse>>(categorias),
            MetaData<Models.Entity.Categoria>.ToValue(categorias));
        return categoriaPg;
    }


    public async Task<CategoriaResponse> UpdateCategoria(int id, CategoriaRequest request)
    {   
        Models.Entity.Categoria categoria = await CheckCategoria(id);
        if (categoria.Nome != request.Nome) await CheckNameExists(request.Nome);
        _mapper.Map(request, categoria);
        Models.Entity.Categoria update = _uof.CategoriaRepository.Update(categoria);
        await _uof.Commit();
        return _mapper.Map<CategoriaResponse>(update);
    }

    public async Task<CategoriaResponse> DeleteCategoria(int id)
    {
        Models.Entity.Categoria categoria = await CheckCategoria(id);
        _uof.CategoriaRepository.Delete(categoria);
        await _uof.Commit();
        return _mapper.Map<CategoriaResponse>(categoria);
    }

    private async Task<Models.Entity.Categoria> CheckCategoria(int id)
    {
        return await _uof.CategoriaRepository.GetAsync(c => c.Id == id) ??
               throw new NotFoundException("Categoria não encontrada!");
    }

    private async Task CheckNameExists(string? nome)
    {
        Models.Entity.Categoria? categoria = await _uof.CategoriaRepository.GetAsync(c => c.Nome == nome);
        if (categoria != null)
        {
            throw new KeyDuplicationException("Já existe uma categoria com este nome!");
        }
    }
}