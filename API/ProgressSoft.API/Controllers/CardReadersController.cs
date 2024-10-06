using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProgressSoft.Core;
using ProgressSoft.Core.Constant;
using ProgressSoft.Core.Dtos;
using ProgressSoft.Core.Entites;
using ProgressSoft.Core.Helper;
using ProgressSoft.Core.Helper.FileUpload;
using ProgressSoft.Core.Helper.FileUpload.Files;

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

            var result =  cardReadersQuery.ToList();

            return Ok(_mapper.Map<List<CardReaderDto>>(result));
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

            // Convert byte array to Base64 string
            cardReaderModel.Photo = Convert.ToBase64String(photoBytes); 

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

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadFile( IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            IFormUpload formUpload;

            if (fileExtension == ".csv")
            {
                formUpload = new CsvUpload();
            }
             else if (fileExtension == ".xml")
            {
                formUpload = new XmlUpload();
            }
            else if (fileExtension == ".png") 
            {

                formUpload = new QrCodeUpload();
            }

            else
            {
                return BadRequest("Unsupported file type. Please upload a CSV or XML file or png(QR CODE).");
            }

            var result = formUpload.ProcessUpload(file);

            if (result.Success)
            {
                if(result.Length == 1)
                {
                  var model =  await _unitOfWork.CardReaders.CreateAsync(_mapper.Map<CardReader>(result.Data.FirstOrDefault()));
                    await _unitOfWork.CompleteAsync();
                    return Ok(_mapper.Map<CardReaderDto>(model));
                }
                else
                {
                    var models = await _unitOfWork.CardReaders.CreateRaneAsync(_mapper.Map<List<CardReader>>(result.Data));
                    await _unitOfWork.CompleteAsync();
                    return Ok(_mapper.Map<List<CardReaderDto>>(models));
                }
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}
