using AutoMapper;
using controle_vendas.infra.exceptions.custom;
using controle_vendas.modules.categoria.model.entity;
using controle_vendas.modules.categoria.model.request;
using controle_vendas.modules.categoria.model.response;
using controle_vendas.modules.categoria.service.interfaces;
using controle_vendas.modules.common.pagination;
using controle_vendas.modules.common.unit_of_work.interfaces;
using X.PagedList;

namespace controle_vendas.modules.categoria.service;

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
        Categoria entity =
            _uof.CategoriaRepository.Create(_mapper.Map<Categoria>(request));
        await _uof.Commit();
        return _mapper.Map<CategoriaResponse>(entity);
    }

    public async Task<CategoriaResponse> GetCategoriaById(int id)
    {
        return _mapper.Map<CategoriaResponse>(await CheckCategoria(id));
    }

    public async Task<CategoriaPaginationResponse> GetAllFilterCategorias(CategoriaFiltroRequest filtroRequest)
    {
        IPagedList<Categoria> categorias = 
            await _uof.CategoriaRepository.GetAllFilterPageableAsync(filtroRequest);
        CategoriaPaginationResponse categoriaPg = new CategoriaPaginationResponse(
            _mapper.Map<IEnumerable<CategoriaResponse>>(categorias),
            MetaData<Categoria>.ToValue(categorias));
        return categoriaPg;
    }

    public async Task<CategoriaResponse> UpdateCategoria(int id, CategoriaRequest request)
    {
        Categoria categoria = await CheckCategoria(id);
        _mapper.Map(request, categoria);
        Categoria update = _uof.CategoriaRepository.Update(categoria);
        await _uof.Commit();
        return _mapper.Map<CategoriaResponse>(update);
    }

    public async Task<CategoriaResponse> DeleteCategoria(int id)
    {
        Categoria categoria = await CheckCategoria(id);
        _uof.CategoriaRepository.Delete(categoria);
        await _uof.Commit();
        return _mapper.Map<CategoriaResponse>(categoria);
    }
    
    private async Task<Categoria> CheckCategoria(int id)
    {
        return await _uof.CategoriaRepository.GetAsync(c => c.Id == id) ??
               throw new NotFoundException("Categoria não encontrada!");
    }
}