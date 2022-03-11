using WorkDiaryWebApp.Models.Procedure;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IProcedureService
    {
        public (bool, string?) AddNewProcedure(AddProcedureModel addProcedureModel);
        public ListFromProcedures GetAllProcedures();

        public ShowProcedureModel ProcedureInfo(string procedureId);

        public (bool, string?) EditProcedure(ShowProcedureModel model);
    }
}
