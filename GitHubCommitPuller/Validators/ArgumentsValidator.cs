namespace GitHubCommitPuller.Validators;

public static class ArgumentsValidator
{
    public static void Validate(string[] args)
    {
        if (args.Length != 2)
        {
            throw new ArgumentException("Invalid arguments amount.");
        }

        if (string.IsNullOrWhiteSpace(args[0]))
        {
            throw new ArgumentException("Owner's name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(args[1]))
        {
            throw new ArgumentException("Repository name cannot be empty.");
        }
    }
}