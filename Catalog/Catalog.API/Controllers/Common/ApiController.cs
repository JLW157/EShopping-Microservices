using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.Common;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ApiController : ControllerBase
{
    
}