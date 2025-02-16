using AutoMapper;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Common.Pagination;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Fornecedor.Models.Entity;
using ControleVendas.Modules.Fornecedor.Models.Request;
using ControleVendas.Modules.Fornecedor.Models.Response;
using ControleVendas.Modules.Fornecedor.Service.Interfaces;
using X.PagedList;

namespace ControleVendas.Modules.Fornecedor.Service;

public class FornecedorService : IFornecedorService
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public FornecedorService(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    public async Task<FornecedorResponse> CreateFornecedor(FornecedorRequest request)
    {
        await CheckNameExists(request.Nome);
        FornecedorEntity entity =
            _uof.FornecedorRepository.Create(_mapper.Map<FornecedorEntity>(request));
        await _uof.Commit();
        return _mapper.Map<FornecedorResponse>(entity);
    }

    public async Task<FornecedorResponse> GetFornecedorById(int id)
    {
        return _mapper.Map<FornecedorResponse>(await CheckFornecedor(id));
    }

    public async Task<FornecedorPaginationResponse> GetAllFilterFornecedor(FornecedorFiltroRequest filtroRequest)
    {
        IPagedList<FornecedorEntity> fornecedores =
            await _uof.FornecedorRepository.GetAllFilterPageableAsync(filtroRequest);
        FornecedorPaginationResponse fornecedorPg = new FornecedorPaginationResponse(
            _mapper.Map<IEnumerable<FornecedorResponse>>(fornecedores),
            MetaData<FornecedorEntity>.ToValue(fornecedores));
        return fornecedorPg;
    }

    public async Task<FornecedorResponse> UpdateFornecedor(int id, FornecedorRequest request)
    {   
        await CheckNameExists(request.Nome);
        FornecedorEntity fornecedorEntity = await CheckFornecedor(id);
        _mapper.Map(request, fornecedorEntity);
        FornecedorEntity update = _uof.FornecedorRepository.Update(fornecedorEntity);
        await _uof.Commit();
        return _mapper.Map<FornecedorResponse>(update);
    }

    public async Task<FornecedorResponse> DeleteFornecedor(int id)
    {
        FornecedorEntity fornecedorEntity = await CheckFornecedor(id);
        _uof.FornecedorRepository.Delete(fornecedorEntity);
        await _uof.Commit();
        return _mapper.Map<FornecedorResponse>(fornecedorEntity);
    }

    private async Task<FornecedorEntity> CheckFornecedor(int id)
    {
        return await _uof.FornecedorRepository.GetAsync(c => c.Id == id) ??
               throw new NotFoundException("Fornecedor não encontrado!");
    }
    
    private async Task CheckNameExists(string nome)
    {
        FornecedorEntity? fornecedor = await _uof.FornecedorRepository.GetAsync(f => f.Nome == nome);
        if (fornecedor != null)
        {
            throw new KeyDuplicationException("Já existe um fornecedor com este nome!");
        }
    }
}