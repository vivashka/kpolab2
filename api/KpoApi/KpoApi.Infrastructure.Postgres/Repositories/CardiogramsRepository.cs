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
                          SET search_path TO cardiogram_schema;
                          select *
                          from "Cardiograms"
                          where "CardiogramUuid" = @CardiogramUuid
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
                          FROM "Cardiograms" c
                          UPDATE c SET "CardiogramState" = cardiogramState WHERE c.CardiogramUuid = guid
                          """;

        var param = new DynamicParameters();
        param.Add("CustomerId", guid);
        param.Add("CardiogramState", cardiogramState);

        return (await ExecuteNonQueryAsync<CardiogramEntity>(sqlQuery, param, cancellationToken))!
            .CardiogramState != cardiogramState;
    }

    public async Task<CardiogramEntity[]> GetCardiograms(Filter filter, CancellationToken cancellationToken)
    {
        string dateFrom = filter.DateFrom.ToString("yyyy-MM-dd hh:mm:ss");
        string dateTo = filter.DateFrom.ToString("yyyy-MM-dd hh:mm:ss");
        
        string sqlQuery = """
                           SET search_path TO cardiogram_schema;
                           SELECT * FROM "Cardiograms" as c
                           LEFT JOIN "Calls" as cl ON c."CallUuid" = cl."CallUuid"
                           LEFT JOIN "ResultsCardiograms" as rc ON c."ResultCardiogramUuid" = rc."ResultCardiogramUuid"
                           WHERE c."ReceivedTime" >= '@dateFrom' AND c."ReceivedTime" <= '@dateTo'
                           """;
        string? modeName = Enum.GetName(typeof(SortMode), filter.SortMode);
        if (filter.SortAttribute == SortAttribute.ReceivedTime)
        {
            
            sqlQuery += """
                         
                         ORDER BY c."ReceivedTime" @modeName; 
                         """;
        }
        else if (filter.SortAttribute == SortAttribute.SsmpNumber)
        {
            sqlQuery += """ ORDER BY c."SsmpNumber" @modeName; """;
        }
        else
        {
            sqlQuery += """ ORDER BY c."ReceivedTime" DESC; """;
        }
        
        var param = new DynamicParameters();
        param.Add("dateFrom", dateFrom);
        param.Add("dateTo", dateTo);
        param.Add("modeName", modeName);

        return await ExecuteQueryAsync<CardiogramEntity>(sqlQuery, param, cancellationToken);
    }
}