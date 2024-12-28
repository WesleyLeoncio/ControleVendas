using AutoMapper;
using controle_vendas.infra.exceptions.custom;
using controle_vendas.modules.categoria.model.entity;
using controle_vendas.modules.categoria.model.request;
using controle_vendas.modules.categoria.model.response;
using controle_vendas.modules.categoria.service.interfaces;
using controle_vendas.modules.common.unit_of_work.interfaces;

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
    
    public async Task<CategoriaResponse> CreateCategoria(CategoriaRequest categoria)
    {
       Categoria entity = 
           _uof.CategoriaRepository.Create(_mapper.Map<Categoria>(categoria));
       await _uof.Commit();
       return _mapper.Map<CategoriaResponse>(entity);
    }

    public async Task<CategoriaResponse> GetCategoriaById(int id)
    {
        return _mapper.Map<CategoriaResponse>( await CheckCategoria(id));
    }

    public Task<CategoriaResponse> GetAllFilterCategorias(CategoriaFiltroRequest filtroRequest)
    {
        throw new NotImplementedException();
    }

    public Task<CategoriaResponse> UpdateCategoria(int id, CategoriaRequest categoria)
    {
        throw new NotImplementedException();
    }

    public Task<CategoriaResponse> DeleteCategoria(int id)
    {
        throw new NotImplementedException();
    }

    private async Task<Categoria> CheckCategoria(int id)
    {
        return await _uof.CategoriaRepository.GetAsync(c => c.Id == id)??
               throw new NotFoundException("Categoria não encontrada!");
    }
}