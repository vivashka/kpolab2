using System.Text;
using Dapper;
using KpoApi.Application.Models.Data;
using KpoApi.Domain.Entities;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;
using KpoApi.Infrastructure.PostgresEfCore.Models.ResultModels;
using Filter = KpoApi.Application.Models.Data.Filter;
using SortAttribute = KpoApi.Application.Models.Data.SortAttribute;
using SortMode = KpoApi.Application.Models.Data.SortMode;

namespace KpoApi.Infrastructure.PostgresEfCore.Repositories;

public class CardiogramsRepository : BaseRepository, ICardiogramsRepository
{
    private ICardiogramsRepository _cardiogramsRepositoryImplementation;

    public async Task<EntireCardiogramEntity?> GetCardiogram(Guid guid, CancellationToken cancellationToken)
    {
        string sqlQuery = """
                          SELECT *
                          FROM "Cardiograms" AS c
                          LEFT JOIN "Calls" AS cl ON c."CallUuid" = cl."CallUuid"
                          LEFT JOIN "ResultsCardiograms" AS rc ON c."ResultCardiogramUuid" = rc."ResultCardiogramUuid"
                          LEFT JOIN "Cardiographs" AS cs ON c."CardiographUuid" = cs."SerialNumber"
                          WHERE c."CardiogramUuid" = @CardiogramUuid
                          """;

        var param = new DynamicParameters();
        param.Add("CardiogramUuid", guid);

        return await ExecuteQuerySingleAsync<EntireCardiogramEntity>(sqlQuery, param, cancellationToken);
    }

    public Task<CardiogramEntity> SendCardiogram(Guid guid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangeCardiogramState(Guid guid, int cardiogramState,
        CancellationToken cancellationToken)
    {
        string sqlQuery = """
                          UPDATE "Cardiograms" SET "CardiogramState" = @CardiogramState
                          WHERE "CardiogramUuid" = @CustomerId
                          RETURNING "CardiogramUuid";
                          """;

        var param = new DynamicParameters();
        param.Add("CustomerId", guid);
        param.Add("CardiogramState", cardiogramState);

        var response = await ExecuteNonQueryAsync<Guid>(sqlQuery, param, cancellationToken);

        return !response.Equals(Guid.Empty);
    }

    public async Task<CardiogramEntity[]> GetCardiograms(Filter filter, CancellationToken cancellationToken)
    {
        
        var sqlQuery = new StringBuilder("""
                                             SELECT * FROM "Cardiograms" AS c
                                             LEFT JOIN "Calls" AS cl ON c."CallUuid" = cl."CallUuid"
                                             LEFT JOIN "ResultsCardiograms" AS rc ON c."ResultCardiogramUuid" = rc."ResultCardiogramUuid"
                                             WHERE c."ReceivedTime" >= @DateFrom AND c."ReceivedTime" <= @DateTo
                                         """);
        
        string? sortDirection = Enum.GetName(typeof(SortMode), filter.SortMode);
        
        switch (filter.SortAttribute)
        {
            case SortAttribute.ReceivedTime:
                sqlQuery.Append(""" ORDER BY c."ReceivedTime" """ + sortDirection);
                break;
            case SortAttribute.SsmpNumber:
                sqlQuery.Append(""" ORDER BY c."SsmpNumber" """ + sortDirection);
                break;
            default:
                sqlQuery.Append(" ORDER BY c.\"ReceivedTime\" DESC");
                break;
        }

       
        var param = new DynamicParameters();
        param.Add("DateFrom", filter.DateFrom);
        param.Add("DateTo", filter.DateTo);
        param.Add("SortDirection", sortDirection);

      
        return await ExecuteQueryAsync<CardiogramEntity>(sqlQuery.ToString(), param, cancellationToken);
    }

    public async Task<Organization[]> GetOrganizations(CancellationToken cancellationToken)
    {
        string sqlQuery = """
                          SELECT * FROM "Organizations"
                          """;
        var param = new DynamicParameters();
        return await ExecuteQueryAsync<Organization>(sqlQuery, param, cancellationToken);
    }

    public async Task<User[]> GetUsers(Guid organizationGuid, CancellationToken cancellationToken)
    {
        string sqlQuery = """
                          SELECT * FROM "Users" AS u
                          WHERE u."OrganizationUuid" = @OrganizationGuid
                          """;
        var param = new DynamicParameters();
        param.Add("OrganizationGuid", organizationGuid);
        return await ExecuteQueryAsync<User>(sqlQuery, param, cancellationToken);
    }

    public async Task<Cardiograph[]> GetCardiographs(Guid? userGuid, CancellationToken cancellationToken)
    {
        string sqlQuery = "";
        
        if (userGuid == null)
        {
            sqlQuery = """
                       SELECT *
                       FROM "Cardiographs"
                       """;
        }
        else
        {
            sqlQuery = """
                       SELECT *
                       FROM "Cardiographs" c
                       INNER JOIN "UsersCardiographs" uc
                         ON c."SerialNumber" = uc."CardiographId"
                       WHERE uc."UserUuid" = @userGuid;
                       """;
        }
        var param = new DynamicParameters();
        param.Add("userGuid", userGuid);
        return await ExecuteQueryAsync<Cardiograph>(sqlQuery, param, cancellationToken);
    }

    public async Task<Cardiogram[]> GetCardiograms(string serialNumber, CancellationToken cancellationToken)
    {
        string sqlQuery = """
                          SELECT * FROM "Cardiograms" AS c
                          WHERE c."CardiographUuid" = @serialNumber
                          """;
        var param = new DynamicParameters();
        param.Add("serialNumber", serialNumber);
        return await ExecuteQueryAsync<Cardiogram>(sqlQuery, param, cancellationToken);
    }

    public async Task<User[]> GetUsersByCardiograms(Guid cardiogramUuid, CancellationToken cancellationToken)
    {
        string sqlQuery = """
                              SELECT *
                              FROM "Users" u
                              INNER JOIN "UsersCardiographs" uc ON u."UserUuid" = uc."UserUuid"
                              INNER JOIN "Cardiographs" c ON uc."CardiographId" = c."SerialNumber"
                              INNER JOIN "Cardiograms" ca ON c."SerialNumber" = ca."CardiographUuid"
                              WHERE ca."CardiogramUuid" = @cardiogramUuid;
                          """;
        var param = new DynamicParameters();
        param.Add("cardiogramUuid", cardiogramUuid);
        return await ExecuteQueryAsync<User>(sqlQuery, param, cancellationToken);
    }

    public async Task<Call[]> GetCalls(CancellationToken cancellationToken)
    {
        string sqlQuery = """
                              SELECT *
                              FROM "Cardiograms"
                          """;
        var param = new DynamicParameters();
        return await ExecuteQueryAsync<Call>(sqlQuery, param, cancellationToken);
    }
}