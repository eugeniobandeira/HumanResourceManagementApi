using FluentAssertions;
using HR.Management.Application.Features.Users.Create;
using HR.Management.Domain.MessageResource;

namespace Unit.Test.Command;
public class CreateCommandValidatorTest
{
    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        // Arrange
        var command = CreateUserCommandBuilder.Build();
        var validator = new CreateUserCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    public void Should_Have_Error_When_Name_Is_Empty(string name)
    {
        // Arrange
        var command = new CreateUserCommand(
            Name: name,
            Email: "teste@gmail.com",
            Role: "Admin",
            Password: "Password123");

        var validator = new CreateUserCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();

        result.Errors
            .Should()
            .ContainSingle()
            .And
            .Contain(error => error.ErrorMessage == ErrorOnValidationMessageResource.NAME_REQUIRED);
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    public void Should_Have_Error_When_Email_Is_Empty(string email)
    {
        // Arrange
        var command = new CreateUserCommand(
            Name: "Tester Test",
            Email: email,
            Role: "Admin",
            Password: "Password123");

        var validator = new CreateUserCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();

        var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
        var totalErrors = 2;

        errorMessages.Should().Contain(ErrorOnValidationMessageResource.EMAIL_REQUIRED);
        errorMessages.Should().Contain(ErrorOnValidationMessageResource.EMAIL_INVALID_FORMAT);

        result.Errors.Should().HaveCount(totalErrors);
    }

    [Theory]
    [InlineData("@gmail")]
    [InlineData("      @123.com.br")]
    [InlineData("123.com.br")]
    [InlineData("myemail.123.com.it")]
    public void Should_Have_Error_When_Email_Is_Invalid(string email)
    {
        // Arrange
        var command = new CreateUserCommand(
            Name: "Tester Test",
            Email: email,
            Role: "Admin",
            Password: "Password123");

        var validator = new CreateUserCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors
            .Should()
            .ContainSingle()
            .And
            .Contain(error => error.ErrorMessage == ErrorOnValidationMessageResource.EMAIL_INVALID_FORMAT);
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    public void Should_Have_Error_When_Role_Is_Empty(string role)
    {
        // Arrange
        var command = new CreateUserCommand(
            Name: "Tester Test",
            Email: "teste@gmail.com",
            Role: role,
            Password: "Password123");

        var validator = new CreateUserCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors
            .Should()
            .ContainSingle()
            .And
            .Contain(error => error.ErrorMessage == ErrorOnValidationMessageResource.ROLE_REQUIRED);
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    public void Should_Have_Error_When_Password_Is_Empty(string password)
    {
        // Arrange
        var command = new CreateUserCommand(
            Name: "Tester Test",
            Email: "teste@gmail.com",
            Role: "Admin",
            Password: password);

        var validator = new CreateUserCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();

        var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
        var totalErrors = 2;

        errorMessages.Should().Contain(ErrorOnValidationMessageResource.PASSWORD_REQUIRED);
        errorMessages.Should().Contain(ErrorOnValidationMessageResource.PASSWORD_MIN_LENGTH);

        result.Errors.Should().HaveCount(totalErrors);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("12345")]
    [InlineData("123456")]
    [InlineData("1234567")]
    public void Should_Have_Error_When_Password_Length_Is_Invalid(string password)
    {
        // Arrange
        var command = new CreateUserCommand(
            Name: "Tester Test",
            Email: "teste@gmail.com",
            Role: "Admin",
            Password: password);

        var validator = new CreateUserCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();

        result.Errors
            .Should()
            .ContainSingle()
            .And
            .Contain(error => error.ErrorMessage == ErrorOnValidationMessageResource.PASSWORD_MIN_LENGTH);
    }
}
