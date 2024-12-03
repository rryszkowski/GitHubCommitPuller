using FluentAssertions;
using GitHubCommitPuller.Validators;

namespace GitHubCommitPuller.Tests.Validators;

public class ArgumentsValidatorTests
{
    [Fact]
    public void Validate_ArgumentsAmountDifferentThanTwo_ThrowArgumentException()
    {
        var args = new [] { "a", "b", "c" };

        var result = () => ArgumentsValidator.Validate(args);

        result.Should().Throw<ArgumentException>()
            .WithMessage("Invalid arguments amount.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_OwnerEmpty_ThrowArgumentException(string owner)
    {
        var args = new[] { owner, "b" };

        var result = () => ArgumentsValidator.Validate(args);

        result.Should().Throw<ArgumentException>()
            .WithMessage("Owner's name cannot be empty.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_RepoEmpty_ThrowArgumentException(string repo)
    {
        var args = new[] { "a", repo };

        var result = () => ArgumentsValidator.Validate(args);

        result.Should().Throw<ArgumentException>()
            .WithMessage("Repository name cannot be empty.");
    }

    [Fact]
    public void Validate_ArgumentsValid_ShouldNotThrow()
    {
        var args = new[] { "a", "b" };

        var result = () => ArgumentsValidator.Validate(args);

        result.Should().NotThrow();
    }
}