using System.Text;
using Dapper;
using KpoApi.Contracts.Repositories;
using KpoApi.Models.Enums;
using KpoApi.Models.ResultModels;
using Filter = KpoApi.Application.Models.Data.Filter;
using SortAttribute = KpoApi.Application.Models.Data.SortAttribute;
using SortMode = KpoApi.Application.Models.Data.SortMode;

namespace KpoApi.Repositories;

public class CardiogramsRepository : BaseRepository, ICardiogramsRepository
{
    public async Task<CardiogramEntity?> GetCardiogram(Guid guid, CancellationToken cancellationToken)
    {
        string sqlQuery = """
                          SELECT *
                          FROM "Cardiograms"
                          WHERE "CardiogramUuid" = @CardiogramUuid
                          """;

        var param = new DynamicParameters();
        param.Add("CardiogramUuid", guid);

        return await ExecuteNonQueryAsync<CardiogramEntity>(sqlQuery, param, cancellationToken);
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

        return !response.Equals(Guid.Empty) ;
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
}