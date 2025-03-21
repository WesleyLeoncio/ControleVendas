﻿using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendas.Modules.Produto.Repository.Filter.Interfaces;

namespace ControleVendas.Modules.Produto.Repository.Filter.Custom;

public class FilterCategoriaProduto : IFilterProdutoResult
{
    public IQueryable<ProdutoEntity> RunFilter(IQueryable<ProdutoEntity> queryable, ProdutoFiltroRequest filtro)
    {
        if (filtro.Categoria.HasValue)
        {
            queryable = queryable
                .Where(p => p.CategoriaId == filtro.Categoria);
            return queryable;
        }
        return queryable;
    }
}