using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgressSoft.Core;
using ProgressSoft.Core.Constant;
using ProgressSoft.Core.Dtos;
using ProgressSoft.Core.Entites;
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
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(_mapper.Map<List<CardReaderDto>>(await _unitOfWork.CardReaders.GetAllAsync()));
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
