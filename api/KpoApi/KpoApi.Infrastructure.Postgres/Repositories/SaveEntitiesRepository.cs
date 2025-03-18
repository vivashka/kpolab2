using Dapper;
using KpoApi.Domain.Entities;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;

namespace KpoApi.Infrastructure.PostgresEfCore.Repositories;

public class SaveEntitiesRepository : BaseRepository, ISaveEntitiesRepository
{
    public async Task<User> SaveUser(User newUser, CancellationToken cancellationToken)
    {
        string sqlQuery = """
                          INSERT INTO "Users"
                          VALUES (@UserUuid, @Login, @Password, @PhoneNumber, @FullName, @OrganizationUuid, @Appointment)
                          ON CONFLICT ("UserUuid")
                          DO UPDATE SET
                              "Login" = @Login,
                              "OrganizationUuid" = @OrganizationUuid,
                              "PhoneNumber" = @PhoneNumber,
                              "FullName" = @FullName,
                              "Appointment" = @Appointment,
                              "Password" = @Password;
                          RETURNING *;
                          """;
        var param = new DynamicParameters();
        param.Add("UserUuid", newUser.UserUuid);
        param.Add("OrganizationUuid", newUser.OrganizationUuid);
        param.Add("PhoneNumber", newUser.PhoneNumber);
        param.Add("FullName", newUser.FullName);
        param.Add("Appointment", newUser.Appointment);
        param.Add("Password", newUser.Password);
        param.Add("Login", newUser.Login);

        return await ExecuteQuerySingleAsync<User>(sqlQuery, param, cancellationToken);
    }

    public async Task<Cardiogram> SaveCardiogram(Cardiogram newCardiogram, CancellationToken cancellationToken)
    {
        string sqlQuery = """
                             INSERT INTO "Cardiograms" 
                             VALUES (
                                 @CardiogramUuid,
                                 @ReceivedTime,
                                 @MeasurementTime,
                                 @CardiographUuid,
                                 @CallUuid,
                                 @CardiogramState,
                                 @ResultCardiogramUuid,
                                 @RawCardiogram
                             )
                             ON CONFLICT ("CardiogramUuid")
                             DO UPDATE SET
                                 "ReceivedTime" = EXCLUDED."ReceivedTime",
                                 "MeasurementTime" = EXCLUDED."MeasurementTime",
                                 "CallUuid" = EXCLUDED."CallUuid",
                                 "CardiogramState" = EXCLUDED."CardiogramState",
                                 "ResultCardiogramUuid" = EXCLUDED."ResultCardiogramUuid",
                                 "RawCardiogram" = EXCLUDED."RawCardiogram"
                             RETURNING *;
                         """;

        var param = new DynamicParameters(newCardiogram);

        return await ExecuteQuerySingleAsync<Cardiogram>(sqlQuery, param, cancellationToken);
    }

    public async Task<Call> SaveCall(Call newCall, CancellationToken cancellationToken)
    {
        string sqlQuery = """
                                INSERT INTO "Calls"
                                VALUES (
                                  @CallUuid,
                                  @CallState,
                                  @PatientName,
                                  @PatientSurname,
                                  @PatientPatronymic,
                                  @PatientAge,
                                  @PatientId,
                                  @PatientSex,
                                  @BrigadeNumber,
                                  @BrigadeProfile,
                                  @MainChiefNumber,
                                  @MainChiefFullName,
                                  @YearNumber,
                                  @DayNumber,
                                  @RegionCode,
                                  @City,
                                  @SsmpNumber,
                                  @SsmpName,
                                  @Street,
                                  @StreetNumber,
                                  @ApartmentNumber,
                                  @Entrance,
                                  @Priority,
                                  @HospitalizationAddress,
                                  @HospitalizationDistance,
                                  @CallType,
                                  @Reason,
                                  @ReceiveTime,
                                  @TransferTime,
                                  @DepartureTime,
                                  @ArrivalTime,
                                  @CallDiagnosis
                              )
                              ON CONFLICT ("CallUuid")
                              DO UPDATE SET
                                  "CallState" = EXCLUDED."CallState",
                                  "PatientName" = EXCLUDED."PatientName",
                                  "PatientSurname" = EXCLUDED."PatientSurname",
                                  "PatientPatronymic" = EXCLUDED."PatientPatronymic",
                                  "PatientAge" = EXCLUDED."PatientAge",
                                  "PatientId" = EXCLUDED."PatientId",
                                  "PatientSex" = EXCLUDED."PatientSex",
                                  "BrigadeNumber" = EXCLUDED."BrigadeNumber",
                                  "BrigadeProfile" = EXCLUDED."BrigadeProfile",
                                  "MainChiefNumber" = EXCLUDED."MainChiefNumber",
                                  "MainChiefFullName" = EXCLUDED."MainChiefFullName",
                                  "YearNumber" = EXCLUDED."YearNumber",
                                  "DayNumber" = EXCLUDED."DayNumber",
                                  "RegionCode" = EXCLUDED."RegionCode",
                                  "City" = EXCLUDED."City",
                                  "SsmpNumber" = EXCLUDED."SsmpNumber",
                                  "SsmpName" = EXCLUDED."SsmpName",
                                  "Street" = EXCLUDED."Street",
                                  "StreetNumber" = EXCLUDED."StreetNumber",
                                  "ApartmentNumber" = EXCLUDED."ApartmentNumber",
                                  "Entrance" = EXCLUDED."Entrance",
                                  "Priority" = EXCLUDED."Priority",
                                  "HospitalizationAddress" = EXCLUDED."HospitalizationAddress",
                                  "HospitalizationDistance" = EXCLUDED."HospitalizationDistance",
                                  "CallType" = EXCLUDED."CallType",
                                  "Reason" = EXCLUDED."Reason",
                                  "ReceiveTime" = EXCLUDED."ReceiveTime",
                                  "TransferTime" = EXCLUDED."TransferTime",
                                  "DepartureTime" = EXCLUDED."DepartureTime",
                                  "ArrivalTime" = EXCLUDED."ArrivalTime",
                                  "CallDiagnosis" = EXCLUDED."CallDiagnosis"
                              RETURNING *;
                          """;

        var param = new DynamicParameters(newCall);


        return await ExecuteQuerySingleAsync<Call>(sqlQuery, param, cancellationToken);
    }


    public async Task<Cardiograph> SaveCardiograph(Cardiograph newCardiograph, CancellationToken cancellationToken)
    {
        string sqlQuery = """
                              INSERT INTO "Cardiographs"
                              VALUES (
                                  @SerialNumber, @CardiographName, @ManufacturerName
                              )
                              ON CONFLICT ("SerialNumber")
                              DO UPDATE SET
                                  "CardiographName" = @CardiographName,
                                  "ManufacturerName" = @ManufacturerName
                              RETURNING *;
                          """;

        var param = new DynamicParameters(newCardiograph);

        return await ExecuteQuerySingleAsync<Cardiograph>(sqlQuery, param, cancellationToken);
    }

    public async Task<ResultsCardiogram> SaveResult(ResultsCardiogram newResult, CancellationToken cancellationToken)
    {
        string sqlQuery = """
                              INSERT INTO "ResultsCardiograms"
                              VALUES (
                                  @ResultCardiogramUuid, @Description, @DiagnosisMain
                              )
                              ON CONFLICT ("ResultCardiogramUuid")
                              DO UPDATE SET
                                  "Description" = @Description,
                                  "DiagnosisMain" = @DiagnosisMain
                              RETURNING *;
                          """;

        var param = new DynamicParameters(newResult);

        return await ExecuteQuerySingleAsync<ResultsCardiogram>(sqlQuery, param, cancellationToken);
    }
}