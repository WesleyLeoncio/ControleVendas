using AutoMapper;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendas.Modules.Cliente.Models.Response;
using ControleVendas.Modules.Cliente.Service.Interfaces;
using ControleVendas.Modules.Common.Pagination;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using X.PagedList;

namespace ControleVendas.Modules.Cliente.Service;

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
        ClienteEntity entity =
            _uof.ClienteRepository.Create(_mapper.Map<ClienteEntity>(request));
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
        IPagedList<ClienteEntity> clientes = 
            await _uof.ClienteRepository.GetAllFilterPageableAsync(filtroRequest);
        ClientePaginationResponse clientePg = new ClientePaginationResponse(
            _mapper.Map<IEnumerable<ClienteResponse>>(clientes),
            MetaData<ClienteEntity>.ToValue(clientes));
        return clientePg;
    }

    public async Task<ClienteResponse> UpdateCliente(int id, ClienteRequest request)
    {
        ClienteEntity clienteEntity = await CheckCliente(id);
        if (clienteEntity.Telefone != request.Telefone) await CheckTelefoneExists(request.Telefone);
        _mapper.Map(request, clienteEntity);
        ClienteEntity update = _uof.ClienteRepository.Update(clienteEntity);
        await _uof.Commit();
        return _mapper.Map<ClienteResponse>(update);
    }

    public async Task<ClienteResponse> DeleteCliente(int id)
    {
       ClienteEntity clienteEntity = await CheckCliente(id);
       _uof.ClienteRepository.Delete(clienteEntity);
       await _uof.Commit();
       return _mapper.Map<ClienteResponse>(clienteEntity);
    }

    public async Task<string> AlterStatusCliente(int id)
    {
        ClienteEntity clienteEntity = await CheckCliente(id);
        clienteEntity.Ativo = !clienteEntity.Ativo;
         _uof.ClienteRepository.Update(clienteEntity);
        await _uof.Commit();
        return clienteEntity.Ativo ? "ATIVO" : "BLOQUEADO";
    }

    private async Task<ClienteEntity> CheckCliente(int id)
    {
        return await _uof.ClienteRepository.GetAsync(c => c.Id == id) ??
               throw new NotFoundException("Cliente não encontrado!");
    }
    
    private async Task CheckTelefoneExists(string? telefone)
    {
        ClienteEntity? cliente = await _uof.ClienteRepository.GetAsync(c => c.Telefone == telefone);
        if (cliente != null)
        {
            throw new KeyDuplicationException("Já existe um telefone com estes numeros cadastrado!");
        }
    }
}