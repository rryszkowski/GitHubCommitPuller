# GitHubCommitPuller
This is a console applications which downloads all commits for a given repository and saves them into database.

## Description
* Application leverages Postgres database with Marten library which allows to use Postgres as a document database
* As Id the SHA field is used what prevents duplicates
* Dependency injection was added in order to add Marten and HttpClient factory
* Factory for HttpClient was used to handle client's life cycle, and implement two Polly policies
* Policies for polly:
	* Terminate connection after 10 seconds
	* Retry three times with extended time period after failure
* Arguments validation was implemented
* Added sample unit tests for ArgumentsValidator

## Usage:
1. Install Docker Desktop locally.
2. Download application repository.
3. Go to directory `docker` in downloaded repository and run `docker compose up -d` in order to establish a server and create a database.
4. Build application.
5. Go to build output directory and issue `GhitHubPuller <owner> <repo>` command.

### Discalaimer:
GitHub has 60 roundtrips per hour restriction, therefore the application can only work with small repositories. In order to increase roundtrip limit, token authentication is required.