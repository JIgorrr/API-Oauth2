using Application.DTOs;
using Application.Interfaces.Services;
using GenerateAccessToken.Model;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace GenerateAccessToken.Controllers.v1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/token")]
public class TokenController(ITokenService tokenService) : ControllerBase
{
    private readonly ITokenService _tokenService = tokenService;

    [HttpPost("generate")]
    public async Task<IActionResult> Generate([FromForm] ModelClientCredentials modelClientCredentials)
    {
        try
        {
            ClientCredentialsDTO clientCredentialsDTO = modelClientCredentials.Adapt<ClientCredentialsDTO>();

            TokenDetailsDTO tokenDetailsDTO = await _tokenService.GenerateToken(clientCredentialsDTO);

            return Ok(tokenDetailsDTO);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
