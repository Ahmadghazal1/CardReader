using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgressSoft.API.Controllers;
using ProgressSoft.Core;
using ProgressSoft.Core.Dtos;
using ProgressSoft.Core.Entites;
using ProgressSoft.Core.Helper;

namespace ProgressSoft.Test.Controller
{
    public class CardReadersControllerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CardReadersControllerTest()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async void CardReadersController_GetCardReaders_ReturnOk()
        {
            //Arrange

            var queryObject = new QueryObject { Name = "Ahmad" };
            var cardReaders = A.Fake<ICollection<CardReaderDto>>();
            var cardReadersList = A.Fake<List<CardReaderDto>>();
            A.CallTo(() => _mapper.Map<List<CardReaderDto>>(cardReaders)).Returns(cardReadersList);

            var controller = new CardReadersController(_unitOfWork,_mapper);

            //Act 

            var result =  await controller.GetAllAsync(queryObject);
            //Assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));

        }


        [Fact]
        public async Task DeleteAsync_WhenCardReaderDoesNotExist_ShouldReturnNotFound()
        {
            //Arrange
            int cardReaderId = 1;
            A.CallTo(() => _unitOfWork.CardReaders.GetByIdAsync(cardReaderId)).Returns(Task.FromResult<CardReader>(null));


            var controller = new CardReadersController(_unitOfWork, _mapper);

            //Act 

            var result = await controller.DeleteAsync(cardReaderId);

            // Assert 

            result.Should().BeOfType<NotFoundObjectResult>()
          .Which.Value.Should().Be($"No there Card Reder with this id {cardReaderId}");
            A.CallTo(() => _unitOfWork.CardReaders.DeleteAsync(A<CardReader>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _unitOfWork.CompleteAsync()).MustNotHaveHappened();

        }

        [Fact]
        public async Task DeleteAsync_WhenCardReaderExists_ShouldReturnOkWithDto()
        {
            // Arrange
            int cardReaderId = 1;
            var cardReader = new CardReader { Id = cardReaderId };
            var cardReaderDto = new CardReaderDto { Id = cardReaderId };

            A.CallTo(() => _unitOfWork.CardReaders.GetByIdAsync(cardReaderId)).Returns(Task.FromResult(cardReader));
            A.CallTo(() => _mapper.Map<CardReaderDto>(cardReader)).Returns(cardReaderDto);

            var controller = new CardReadersController(_unitOfWork, _mapper);


            // Act
            var result = await controller.DeleteAsync(cardReaderId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(cardReaderDto);

            A.CallTo(() => _unitOfWork.CardReaders.DeleteAsync(cardReader)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task CreateAsync_WhenPhotoExceedsSizeLimit_ShouldReturnProblemDetails()
        {
            // Arrange

            var file = CreateFakeFile(2 * (1 * 1024 * 1024));
            var createDto = new CreateCardReaderDto { Photo = file };
            var controller = new CardReadersController(_unitOfWork, _mapper);


            // Act
            var response = await controller.CreateAsync(createDto);

            // Assert

            response.Should().BeOfType<OkObjectResult>();
            var result = response as OkObjectResult;
            result.Value.Should().BeOfType<ProblemDetails>()
           .Which.Title.Should().Be("Image size too large");
            result.Value.Should().BeOfType<ProblemDetails>()
                .Which.Status.Should().Be(StatusCodes.Status400BadRequest);
        }



        [Fact]
        public async Task CreateAsync_WhenValidPhotoIsUploaded_ShouldCreateCardReader()
        {
            // Arrange
            var file = CreateFakeFile(500 * 1024);  // Valid file under 1MB
            var createDto = new CreateCardReaderDto { Photo = file };

            var cardReader = new CardReader();
            A.CallTo(() => _mapper.Map<CardReader>(createDto)).Returns(cardReader);

            var savedModel = new CardReader { Id = 1 };
            A.CallTo(() => _unitOfWork.CardReaders.CreateAsync(cardReader)).Returns(savedModel);
            var controller = new CardReadersController(_unitOfWork, _mapper);

            // Act
            var response = await controller.CreateAsync(createDto);

            // Assert
            response.Should().BeOfType<OkObjectResult>();
            var result = response as OkObjectResult;
            result.Value.Should().BeEquivalentTo(new { Success = true, Data = savedModel });

            A.CallTo(() => _unitOfWork.CardReaders.CreateAsync(A<CardReader>.Ignored)).MustHaveHappened();
            A.CallTo(() => _unitOfWork.CompleteAsync()).MustHaveHappenedOnceExactly();
        }


        private IFormFile CreateFakeFile(long size)
        {
            var stream = new MemoryStream(new byte[size]);
            var formFile = new FormFile(stream, 0, size, "file", "test.png")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };
            return formFile;
        }
    }
}
