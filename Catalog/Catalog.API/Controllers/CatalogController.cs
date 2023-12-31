using Amazon.Util;
using Catalog.API.Controllers.Common;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Catalog.API.Controllers;

public class CatalogController : ApiController
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{action}/{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductResponse>> GetProductById(string id)
    {
        var query = new GetProductByIdQuery(id);
        
        var result = await _mediator.Send(query);
        
        return Ok(result);
    }

    [HttpGet]
    [Route("{action}/{productName}", Name = "GetProductByName")]
    [ProducesResponseType(typeof(IList<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetProductByName(string productName)
    {
        var query = new GetProductByNameQuery(productName);

        var result = await _mediator.Send(query);

        return Ok(result);
    }
    
    [HttpGet]
    [Route("GetAllProducts")]
    [ProducesResponseType(typeof(IList<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<ProductResponse>>> GetAllProducts()
    {
        var query = new GetAllProductsQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }
    
    [HttpGet]
    [Route("GetAllBrands")]
    [ProducesResponseType(typeof(IList<BrandResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<BrandResponse>>> GetAllBrands()
    {
        var query = new GetAllBrandsQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }
}