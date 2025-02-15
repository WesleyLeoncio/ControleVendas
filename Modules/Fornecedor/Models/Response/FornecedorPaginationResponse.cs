using ControleVendas.Modules.Common.Pagination;

namespace ControleVendas.Modules.Fornecedor.Models.Response;

public class FornecedorPaginationResponse
{
    public IEnumerable<FornecedorResponse> Fornecedores { get; private set; }
    public MetaData<Entity.Fornecedor> MetaData { get; private set; }

    public FornecedorPaginationResponse(IEnumerable<FornecedorResponse> fornecedores, MetaData<Entity.Fornecedor> metaData)
    {
        Fornecedores = fornecedores;
        MetaData = metaData;
    }
}