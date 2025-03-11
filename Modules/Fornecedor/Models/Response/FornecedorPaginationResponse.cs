using ControleVendas.Modules.Common.Pagination;
using ControleVendas.Modules.Fornecedor.Models.Entity;

namespace ControleVendas.Modules.Fornecedor.Models.Response;

public class FornecedorPaginationResponse
{
    public IEnumerable<FornecedorResponse> Fornecedores { get; private set; }
    public MetaData<FornecedorEntity> MetaData { get; private set; }

    public FornecedorPaginationResponse(IEnumerable<FornecedorResponse> fornecedores, MetaData<FornecedorEntity> metaData)
    {
        Fornecedores = fornecedores;
        MetaData = metaData;
    }
}