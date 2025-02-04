using AutoMapper;
using controle_vendas.infra.exceptions.custom;
using controle_vendas.modules.cliente.model.entity;
using controle_vendas.modules.cliente.model.request;
using controle_vendas.modules.cliente.model.response;
using controle_vendas.modules.cliente.service.interfaces;
using controle_vendas.modules.common.pagination;
using controle_vendas.modules.common.unit_of_work.interfaces;
using X.PagedList;

namespace controle_vendas.modules.cliente.service;

public class ClienteService : IClienteService
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ClienteService(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    public async Task<ClienteResponse> CreateCliente(ClienteRequest request)
    {   
        await CheckTelefoneExists(request.Telefone);
        Cliente entity =
            _uof.ClienteRepository.Create(_mapper.Map<Cliente>(request));
        entity.Ativo = true;
        await _uof.Commit();
        return _mapper.Map<ClienteResponse>(entity);
    }

    public async Task<ClienteResponse> GetClienteById(int id)
    {
        return _mapper.Map<ClienteResponse>(await CheckCliente(id));
    }

    public async Task<ClientePaginationResponse> GetAllFilterClientes(ClienteFiltroRequest filtroRequest)
    {   
        IPagedList<Cliente> clientes = 
            await _uof.ClienteRepository.GetAllFilterPageableAsync(filtroRequest);
        ClientePaginationResponse clientePg = new ClientePaginationResponse(
            _mapper.Map<IEnumerable<ClienteResponse>>(clientes),
            MetaData<Cliente>.ToValue(clientes));
        return clientePg;
    }

    public async Task<ClienteResponse> UpdateCliente(int id, ClienteRequest request)
    {
        Cliente cliente = await CheckCliente(id);
        if (cliente.Telefone != request.Telefone) await CheckTelefoneExists(request.Telefone);
        _mapper.Map(request, cliente);
        Cliente update = _uof.ClienteRepository.Update(cliente);
        await _uof.Commit();
        return _mapper.Map<ClienteResponse>(update);
    }

    public async Task<ClienteResponse> DeleteCliente(int id)
    {
       Cliente cliente = await CheckCliente(id);
       _uof.ClienteRepository.Delete(cliente);
       await _uof.Commit();
       return _mapper.Map<ClienteResponse>(cliente);
    }

    public async Task<string> AlterStatusCliente(int id)
    {
        Cliente cliente = await CheckCliente(id);
        cliente.Ativo = !cliente.Ativo;
         _uof.ClienteRepository.Update(cliente);
        await _uof.Commit();
        return cliente.Ativo ? "ATIVO" : "BLOQUEADO";
    }

    private async Task<Cliente> CheckCliente(int id)
    {
        return await _uof.ClienteRepository.GetAsync(c => c.Id == id) ??
               throw new NotFoundException("Cliente não encontrado!");
    }
    
    private async Task CheckTelefoneExists(string? telefone)
    {
        Cliente? cliente = await _uof.ClienteRepository.GetAsync(c => c.Telefone == telefone);
        if (cliente != null)
        {
            throw new KeyDuplicationException("Já existe um telefone com estes numeros cadastrado!");
        }
    }
}