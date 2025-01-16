using AutoMapper;
using controle_vendas.infra.exceptions.custom;
using controle_vendas.modules.common.pagination;
using controle_vendas.modules.common.unit_of_work.interfaces;
using controle_vendas.modules.fornecedor.model.entity;
using controle_vendas.modules.fornecedor.model.request;
using controle_vendas.modules.fornecedor.model.response;
using controle_vendas.modules.fornecedor.service.interfaces;
using X.PagedList;

namespace controle_vendas.modules.fornecedor.service;

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
        Fornecedor entity =
            _uof.FornecedorRepository.Create(_mapper.Map<Fornecedor>(request));
        await _uof.Commit();
        return _mapper.Map<FornecedorResponse>(entity);
    }

    public async Task<FornecedorResponse> GetFornecedorById(int id)
    {
        return _mapper.Map<FornecedorResponse>(await CheckFornecedor(id));
    }

    public async Task<FornecedorPaginationResponse> GetAllFilterFornecedor(FornecedorFiltroRequest filtroRequest)
    {
        IPagedList<Fornecedor> fornecedores =
            await _uof.FornecedorRepository.GetAllFilterPageableAsync(filtroRequest);
        FornecedorPaginationResponse fornecedorPg = new FornecedorPaginationResponse(
            _mapper.Map<IEnumerable<FornecedorResponse>>(fornecedores),
            MetaData<Fornecedor>.ToValue(fornecedores));
        return fornecedorPg;
    }

    public async Task<FornecedorResponse> UpdateFornecedor(int id, FornecedorRequest request)
    {
        Fornecedor fornecedor = await CheckFornecedor(id);
        _mapper.Map(request, fornecedor);
        Fornecedor update = _uof.FornecedorRepository.Update(fornecedor);
        await _uof.Commit();
        return _mapper.Map<FornecedorResponse>(update);
    }

    public async Task<FornecedorResponse> DeleteFornecedor(int id)
    {
        Fornecedor fornecedor = await CheckFornecedor(id);
        _uof.FornecedorRepository.Delete(fornecedor);
        await _uof.Commit();
        return _mapper.Map<FornecedorResponse>(fornecedor);
    }

    private async Task<Fornecedor> CheckFornecedor(int id)
    {
        return await _uof.FornecedorRepository.GetAsync(c => c.Id == id) ??
               throw new NotFoundException("Fornecedor não encontrado!");
    }
}