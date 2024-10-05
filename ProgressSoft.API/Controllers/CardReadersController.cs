using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgressSoft.Core;
using System.Reflection.Metadata.Ecma335;

namespace ProgressSoft.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardReadersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CardReadersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _unitOfWork.CardReaders.GetAllAsync());
        }
    }
}
