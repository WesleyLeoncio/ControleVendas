using controle_vendas.modules.common.pagination;
using controle_vendas.modules.fornecedor.model.entity;

namespace controle_vendas.modules.fornecedor.model.response;

public class FornecedorPaginationResponse
{
    public IEnumerable<FornecedorResponse> Fornecedores { get; private set; }
    public MetaData<Fornecedor> MetaData { get; private set; }

    public FornecedorPaginationResponse(IEnumerable<FornecedorResponse> fornecedores, MetaData<Fornecedor> metaData)
    {
        Fornecedores = fornecedores;
        MetaData = metaData;
    }
}