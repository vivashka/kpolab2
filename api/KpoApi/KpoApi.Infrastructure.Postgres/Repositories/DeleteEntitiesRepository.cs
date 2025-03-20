using Dapper;
using KpoApi.Domain.Entities;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;

namespace KpoApi.Infrastructure.PostgresEfCore.Repositories;

public class DeleteEntitiesRepository : BaseRepository, IDeleteEntitiesRepository
{
    public async Task<bool> DeleteCardiogram(Guid guid, CancellationToken cancellationToken)
{
    // 1. Получаем идентификатор результата кардиограммы для данной записи
    string getResultQuery = @"
        SELECT ""ResultCardiogramUuid""
        FROM ""Cardiograms""
        WHERE ""CardiogramUuid"" = @CardiogramUuid;
    ";
    var paramGet = new DynamicParameters();
    paramGet.Add("CardiogramUuid", guid);
    Guid? resultGuid = null;
    try
    {
        resultGuid = await ExecuteQuerySingleAsync<Guid?>(getResultQuery, paramGet, cancellationToken);
    }
    catch (InvalidOperationException ex) when (ex.Message.Contains("Sequence contains no elements"))
    {
        // Если запись не найдена, resultGuid остается null
    }

    // 2. Удаляем кардиограмму
    string deleteCardiogramQuery = @"
        DELETE FROM ""Cardiograms""
        WHERE ""CardiogramUuid"" = @CardiogramUuid
        RETURNING ""CardiogramUuid"";
    ";
    var paramCardiogram = new DynamicParameters();
    paramCardiogram.Add("CardiogramUuid", guid);
    Guid? deletedCardiogramId = await ExecuteNonQueryAsync<Guid?>(deleteCardiogramQuery, paramCardiogram, cancellationToken);

    // 3. Если кардиограмма была удалена и найден связанный результат, проверяем,
    // ссылается ли на него ещё какая-либо кардиограмма.
    if (deletedCardiogramId != null && resultGuid != null)
    {
        string checkQuery = @"
            SELECT COUNT(*) 
            FROM ""Cardiograms""
            WHERE ""ResultCardiogramUuid"" = @ResultCardiogramUuid;
        ";
        var paramCheck = new DynamicParameters();
        paramCheck.Add("ResultCardiogramUuid", resultGuid);
        int count = await ExecuteQuerySingleAsync<int>(checkQuery, paramCheck, cancellationToken);

        // Если других ссылок нет, удаляем результат
        if (count == 0)
        {
            string deleteResultQuery = @"
                DELETE FROM ""ResultsCardiograms""
                WHERE ""ResultCardiogramUuid"" = @ResultCardiogramUuid
                RETURNING ""ResultCardiogramUuid"";
            ";
            var paramResult = new DynamicParameters();
            paramResult.Add("ResultCardiogramUuid", resultGuid);
            var deletedResultId = await ExecuteNonQueryAsync<Guid?>(deleteResultQuery, paramResult, cancellationToken);
            // Дополнительно можно проверить, что deletedResultId != null
        }
    }

    return deletedCardiogramId != null;
}





}