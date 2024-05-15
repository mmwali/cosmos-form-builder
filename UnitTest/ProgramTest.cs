using API.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace UnitTest;

public class ProgramTest
{
    private readonly IProgramService _programService;
    private IServiceProvider services = ServicesMock.GetCollection().BuildServiceProvider();
    public ProgramTest()
    {
        _programService = services.GetService<IProgramService>()!;
    }
    [Fact]
    public async Task CreateProgram_ShouldNotBeNull()
    {
        var response = await _programService.CreateProgramAsync(new()
        {
            Country = new() { IsHidden = true, IsInternal = false },
            DateOfBirth = new() { IsHidden = true, IsInternal = false },
            Description = "Description",
            Gender = new() { IsHidden = true, IsInternal = false },
            IdNumber = new() { IsHidden = false, IsInternal = false },
            Nationality= new() { IsHidden = true, IsInternal = false },
            Phone = new() { IsHidden = true, IsInternal = false },
            Title = "test program"
        });
        Assert.NotNull(response);
    }
    [Fact]
    public async Task ViewProgram_ShouldNotBeNull()
    {
        var response = await _programService.CreateProgramAsync(new()
        {
            Country = new() { IsHidden = true, IsInternal = false },
            DateOfBirth = new() { IsHidden = true, IsInternal = false },
            Description = "Description",
            Gender = new() { IsHidden = true, IsInternal = false },
            IdNumber = new() { IsHidden = false, IsInternal = false },
            Nationality= new() { IsHidden = true, IsInternal = false },
            Phone = new() { IsHidden = true, IsInternal = false },
            Title = "test program"
        });
        Assert.NotNull(response);

        var response2 = await _programService.ViewProgramAsync(response.Data);
        Assert.NotNull(response2);
    }

    [Fact]
    public async Task ViewProgram_ShouldBeNull()
    {

        var response2 = await _programService.ViewProgramAsync(Guid.Empty);
        Assert.Null(response2.Data);
    }
    [Fact]
    public async Task CreateQuestion_ShouldBeSuccessful()
    {
        var response = await _programService.CreateProgramAsync(new()
        {
            Country = new() { IsHidden = true, IsInternal = false },
            DateOfBirth = new() { IsHidden = true, IsInternal = false },
            Description = "Description",
            Gender = new() { IsHidden = true, IsInternal = false },
            IdNumber = new() { IsHidden = false, IsInternal = false },
            Nationality= new() { IsHidden = true, IsInternal = false },
            Phone = new() { IsHidden = true, IsInternal = false },
            Title = "test program"
        });
        Assert.NotNull(response);

        var response2 = await _programService.CreateQuestionAsync(response.Data, new()
        {
            Label = "test lbl",
            QuestionType = API.Enums.InputType.Paragraph,
        });
        Assert.True(response2.Status);
    }
    [Fact]
    public async Task CreateQuestion_ShouldBeNoData()
    {
        var response2 = await _programService.CreateQuestionAsync(Guid.Empty, new()
        {
            Label = "test lbl",
            QuestionType = API.Enums.InputType.Paragraph,
        });
        Assert.True(response2.Status);
    }
    [Fact]
    public async Task UpdateQuestion_ShouldBeSuccessful()
    {
        var response = await _programService.CreateProgramAsync(new()
        {
            Country = new() { IsHidden = true, IsInternal = false },
            DateOfBirth = new() { IsHidden = true, IsInternal = false },
            Description = "Description",
            Gender = new() { IsHidden = true, IsInternal = false },
            IdNumber = new() { IsHidden = false, IsInternal = false },
            Nationality= new() { IsHidden = true, IsInternal = false },
            Phone = new() { IsHidden = true, IsInternal = false },
            Title = "test program"
        });
        Assert.NotNull(response);

        var response2 = await _programService.UpdateQuestionAsync(response.Data, new()
        {
            Label = "test label",
            QuestionType = API.Enums.InputType.Paragraph,
        });
        Assert.True(response2.Status);
    }
    [Fact]
    public async Task DeleteQuestion_ShouldBeSuccessful()
    {
        var response = await _programService.CreateProgramAsync(new()
        {
            Country = new() { IsHidden = true, IsInternal = false },
            DateOfBirth = new() { IsHidden = true, IsInternal = false },
            Description = "Description",
            Gender = new() { IsHidden = true, IsInternal = false },
            IdNumber = new() { IsHidden = false, IsInternal = false },
            Nationality= new() { IsHidden = true, IsInternal = false },
            Phone = new() { IsHidden = true, IsInternal = false },
            Title = "test program"
        });
        Assert.NotNull(response);

        var response2 = await _programService.DeleteQuestionAsync(response.Data);
        Assert.True(response2.Status);
    }
}