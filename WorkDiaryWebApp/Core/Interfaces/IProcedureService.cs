using WorkDiaryWebApp.Models.Procedure;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IProcedureService
    {
        public Task<(bool, string?)> AddNewProcedure(AddProcedureModel addProcedureModel);
        public Task<ListFromProcedures> GetAllProcedures();
        public Task<ListFromProcedures> GetAllProceduresAdmin();

        public Task<ShowProcedureModel> ProcedureInfo(string procedureId);

        public Task<(bool, string?)> EditProcedure(ShowProcedureModel model);
    }
}
