using WorkDiaryWebApp.Models.Procedure;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IProcedureService
    {
        public (bool isDone, string errors) AddNewProcedure(AddProcedureModel addProcedureModel);
        public ListFromProcedures GetAllProcedures();

        public ShowProcedureModel ProcedureInfo(string procedureId);
    }
}
