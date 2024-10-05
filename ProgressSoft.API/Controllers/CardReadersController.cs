using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgressSoft.Core;
using ProgressSoft.Core.Constant;
using ProgressSoft.Core.Dtos;
using ProgressSoft.Core.Entites;
using ProgressSoft.Core.Helper;
using System.Reflection.Metadata.Ecma335;

namespace ProgressSoft.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardReadersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        public CardReadersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] QueryObject query)
        {
            // Start with the base query, fetching all data from the repository
            var cardReaders= await _unitOfWork.CardReaders.GetAllAsync();

            var cardReadersQuery = cardReaders.AsQueryable();

            if (!string.IsNullOrEmpty(query.Email))
            {
                cardReadersQuery = cardReadersQuery.Where(x => x.Email.Contains(query.Email));
            }

            if (!string.IsNullOrEmpty(query.Gender))
            {
                cardReadersQuery = cardReadersQuery.Where(x => x.Gender.Contains(query.Gender));
            }

            if (!string.IsNullOrEmpty(query.Name))
            {
                cardReadersQuery = cardReadersQuery.Where(x => x.Name.Contains(query.Name));
            }

            if (!string.IsNullOrEmpty(query.Phone))
            {
                cardReadersQuery = cardReadersQuery.Where(x => x.Phone.Contains(query.Phone));
            }

            var result = cardReadersQuery.ToList();

            return Ok(_mapper.Map<CardReaderDto>(result));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]CreateCardReaderDto createCardReaderDto)
        {
            if(createCardReaderDto.Photo.Length > Constant.ONE_MB)
            {
                return BadRequest("The image size exceeds the maximum allowed size of 1MB.");
            }

            using var stream = new MemoryStream();
            await createCardReaderDto.Photo.CopyToAsync(stream);
            var photoBytes = stream.ToArray();

            var cardReaderModel = _mapper.Map<CardReader>(createCardReaderDto);
            cardReaderModel.Photo = photoBytes;

            var model =  await _unitOfWork.CardReaders.CreateAsync(cardReaderModel);
            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<CardReaderDto>(model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var model = await _unitOfWork.CardReaders.GetByIdAsync(id);
            if (model is null)
                return NotFound($"No there Card Reder with this id {id}");

            await _unitOfWork.CardReaders.DeleteAsync(model);
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<CardReaderDto>(model)); 
        }
    }
}
